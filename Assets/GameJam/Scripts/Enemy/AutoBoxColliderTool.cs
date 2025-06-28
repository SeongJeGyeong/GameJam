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

    // Order in Layer ฐüทร
    private bool setOrderInLayer = false;
    private int orderInLayerValue = 0;

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

            Component collider = null;

            if (selectedColliderType == ColliderType.Box)
            {
                BoxCollider2D box = sr.GetComponent<BoxCollider2D>();
                if (!box)
                    box = Undo.AddComponent<BoxCollider2D>(sr.gameObject);
                if (useComposite)
                    box.usedByComposite = true;
                collider = box;
            }
            else if (selectedColliderType == ColliderType.Polygon)
            {
                PolygonCollider2D poly = sr.GetComponent<PolygonCollider2D>();
                if (!poly)
                    poly = Undo.AddComponent<PolygonCollider2D>(sr.gameObject);
                if (useComposite)
                    poly.usedByComposite = true;
                collider = poly;
            }

            if (setOrderInLayer)
            {
                Undo.RecordObject(sr, "Set Order in Layer");
                sr.sortingOrder = orderInLayerValue;
                EditorUtility.SetDirty(sr);
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
