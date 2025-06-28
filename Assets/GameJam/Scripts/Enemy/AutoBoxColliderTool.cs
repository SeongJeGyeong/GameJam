using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class AutoBoxColliderTool : EditorWindow
{
    private enum ColliderType
    {
        Box,
        Polygon
    }

    private ColliderType selectedColliderType = ColliderType.Box;
    private bool useComposite = false;

    // Order in Layer
    private bool setOrderInLayer = false;
    private int orderInLayerValue = 0;

    // Layer
    private bool setLayer = false;
    private int layerIndex = 0;

    // Tag
    private bool setTag = false;
    private string selectedTag = "Untagged";

    // IsTrigger
    private bool setIsTrigger = false;
    private bool isTriggerValue = false;

    private List<SpriteRenderer> cachedSpriteRenderers = new List<SpriteRenderer>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Auto Box Collider 2D")]
    public static void ShowWindow()
    {
        GetWindow<AutoBoxColliderTool>("Auto Box Collider 2D");
    }

    private void OnGUI()
    {
        GUILayout.Label("Auto Collider Generator", EditorStyles.boldLabel);

        selectedColliderType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", selectedColliderType);
        useComposite = EditorGUILayout.Toggle("Use Composite Collider", useComposite);

        EditorGUILayout.Space();

        setOrderInLayer = EditorGUILayout.Toggle("Set Order in Layer", setOrderInLayer);
        if (setOrderInLayer)
        {
            orderInLayerValue = EditorGUILayout.IntField("Order in Layer Value", orderInLayerValue);
        }

        setLayer = EditorGUILayout.Toggle("Set Unity Layer", setLayer);
        if (setLayer)
        {
            layerIndex = EditorGUILayout.LayerField("Layer", layerIndex);
        }

        setTag = EditorGUILayout.Toggle("Set Tag", setTag);
        if (setTag)
        {
            selectedTag = EditorGUILayout.TagField("Tag", selectedTag);
        }

        setIsTrigger = EditorGUILayout.Toggle("Set Is Trigger", setIsTrigger);
        if (setIsTrigger)
        {
            isTriggerValue = EditorGUILayout.Toggle("Is Trigger", isTriggerValue);
        }

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Selected to SpriteRenderers"))
        {
            AddSelectedToCache();
        }
        if (GUILayout.Button("Clear SpriteRenderers"))
        {
            cachedSpriteRenderers.Clear();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < cachedSpriteRenderers.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.ObjectField(cachedSpriteRenderers[i], typeof(SpriteRenderer), true);
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                cachedSpriteRenderers.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Colliders"))
        {
            GenerateColliders();
        }
    }

    private void AddSelectedToCache()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (GameObject obj in selectedObjects)
        {
            SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer renderer in renderers)
            {
                if (!cachedSpriteRenderers.Contains(renderer))
                {
                    cachedSpriteRenderers.Add(renderer);
                }
            }
        }
    }

    private void GenerateColliders()
    {
        if (cachedSpriteRenderers.Count == 0)
        {
            EditorUtility.DisplayDialog("Warning", "No SpriteRenderers to process.", "OK");
            return;
        }

        Transform commonParent = cachedSpriteRenderers[0].transform.parent;
        if (useComposite)
        {
            foreach (var sr in cachedSpriteRenderers)
            {
                if (sr.transform.parent != commonParent)
                {
                    EditorUtility.DisplayDialog("Error", "To use CompositeCollider2D, all SpriteRenderers must share the same parent.", "OK");
                    return;
                }
            }

            Rigidbody2D rb = commonParent.GetComponent<Rigidbody2D>();
            if (!rb)
                rb = Undo.AddComponent<Rigidbody2D>(commonParent.gameObject);
            rb.bodyType = RigidbodyType2D.Static;

            if (!commonParent.GetComponent<CompositeCollider2D>())
                Undo.AddComponent<CompositeCollider2D>(commonParent.gameObject);
        }

        int processed = 0;
        foreach (var sr in cachedSpriteRenderers)
        {
            if (!sr || !sr.sprite) continue;

            GameObject go = sr.gameObject;
            Component collider = null;

            if (selectedColliderType == ColliderType.Box)
            {
                BoxCollider2D box = go.GetComponent<BoxCollider2D>();
                if (!box)
                    box = Undo.AddComponent<BoxCollider2D>(go);
                if (useComposite)
                    box.usedByComposite = true;
                if (setIsTrigger)
                    box.isTrigger = isTriggerValue;
                collider = box;
            }
            else if (selectedColliderType == ColliderType.Polygon)
            {
                PolygonCollider2D poly = go.GetComponent<PolygonCollider2D>();
                if (!poly)
                    poly = Undo.AddComponent<PolygonCollider2D>(go);
                if (useComposite)
                    poly.usedByComposite = true;
                if (setIsTrigger)
                    poly.isTrigger = isTriggerValue;
                collider = poly;
            }

            if (setOrderInLayer)
            {
                Undo.RecordObject(sr, "Set Order in Layer");
                sr.sortingOrder = orderInLayerValue;
                EditorUtility.SetDirty(sr);
            }

            if (setLayer)
            {
                Undo.RecordObject(go, "Set Layer");
                go.layer = layerIndex;
                EditorUtility.SetDirty(go);
            }

            if (setTag)
            {
                Undo.RecordObject(go, "Set Tag");
                go.tag = selectedTag;
                EditorUtility.SetDirty(go);
            }

            if (collider != null)
            {
                Undo.RecordObject(collider, "Configure Collider");
                EditorUtility.SetDirty(collider);
                processed++;
            }
        }

        EditorUtility.DisplayDialog("Complete", $"Generated {processed} {selectedColliderType} colliders.", "OK");
    }
}
