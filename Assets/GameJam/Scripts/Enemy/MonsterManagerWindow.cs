using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class MonsterManagerWindow : EditorWindow
{
    private Vector2 scrollPos;
    private string searchKeyword = "";

    private Dictionary<string, bool> componentFoldouts = new Dictionary<string, bool>();

    private List<string> commonComponentTypes = new List<string> { "MonsterWalker", "MonsterAttack" };
    private List<string> subFeatureTypes = new List<string> { "ProjectileShooter", "BreathController" };
    private Dictionary<string, bool> filterCommon = new Dictionary<string, bool>();
    private Dictionary<string, bool> filterSub = new Dictionary<string, bool>();

    private List<GameObject> monsterPrefabs = new List<GameObject>();

    [MenuItem("Tools/Monster Manager")]
    public static void ShowWindow()
    {
        GetWindow<MonsterManagerWindow>("Monster Manager");
    }

    private void OnEnable()
    {
        foreach (var comp in commonComponentTypes)
            if (!filterCommon.ContainsKey(comp)) filterCommon[comp] = true;
        foreach (var sub in subFeatureTypes)
            if (!filterSub.ContainsKey(sub)) filterSub[sub] = true;

        LoadAllMonsters();
    }

    private void LoadAllMonsters()
    {
        monsterPrefabs.Clear();

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Monsters" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null && prefab.GetComponent<Monster>())
            {
                monsterPrefabs.Add(prefab);
            }
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("\uD83D\uDD04 Refresh Monster List"))
        {
            LoadAllMonsters();
        }

        searchKeyword = EditorGUILayout.TextField("\uD83D\uDD0D Search by Name", searchKeyword);

        GUILayout.Label("\uD83D\uDCCC Filter Options", EditorStyles.boldLabel);

        GUILayout.Label("Common Components:");
        foreach (var comp in commonComponentTypes.ToList())
            filterCommon[comp] = EditorGUILayout.ToggleLeft($"  {comp}", filterCommon[comp]);

        GUILayout.Label("Sub Features:");
        foreach (var sub in subFeatureTypes.ToList())
            filterSub[sub] = EditorGUILayout.ToggleLeft($"  {sub}", filterSub[sub]);

        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (var prefab in monsterPrefabs)
        {
            if (!string.IsNullOrEmpty(searchKeyword) && !prefab.name.ToLower().Contains(searchKeyword.ToLower()))
                continue;

            Monster m = prefab.GetComponent<Monster>();
            if (m == null) continue;

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();

            // 썸네일
            Texture2D previewTex = AssetPreview.GetAssetPreview(prefab);
            GUILayout.Label(previewTex, GUILayout.Width(50), GUILayout.Height(50));

            // 이름 + 클릭 시 핑
            if (GUILayout.Button(prefab.name, EditorStyles.boldLabel))
            {
                EditorGUIUtility.PingObject(prefab);
                Selection.activeObject = prefab;
            }

            if (GUILayout.Button("\u2192 Scene", GUILayout.Width(70)))
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                Undo.RegisterCreatedObjectUndo(instance, "Place Monster Prefab");
            }

            EditorGUILayout.EndHorizontal();

            // Stats 수정
            EditorGUI.BeginChangeCheck();
            m.currentHp = EditorGUILayout.IntField("HP", m.currentHp);
            m.moveSpeed = EditorGUILayout.FloatField("Move Speed", m.moveSpeed);
            m.jumpPower = EditorGUILayout.FloatField("Jump Power", m.jumpPower);
            m.atkCoolTime = EditorGUILayout.FloatField("Attack Cooltime", m.atkCoolTime);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(m);
                PrefabUtility.RecordPrefabInstancePropertyModifications(m);
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

        DrawFilteredFoldouts();
    }

    private void DrawFilteredFoldouts()
    {
        foreach (var comp in commonComponentTypes)
        {
            if (!filterCommon[comp]) continue;
            if (!componentFoldouts.ContainsKey(comp)) componentFoldouts[comp] = false;

            componentFoldouts[comp] = EditorGUILayout.Foldout(componentFoldouts[comp], $"🔹 {comp} 포함 몬스터");
            if (componentFoldouts[comp])
            {
                foreach (var prefab in monsterPrefabs)
                {
                    if (prefab.GetComponent(comp) != null)
                        EditorGUILayout.ObjectField(prefab, typeof(GameObject), false);
                }
            }
        }

        foreach (var sub in subFeatureTypes)
        {
            if (!filterSub[sub]) continue;
            if (!componentFoldouts.ContainsKey(sub)) componentFoldouts[sub] = false;

            componentFoldouts[sub] = EditorGUILayout.Foldout(componentFoldouts[sub], $"🔹 {sub} 포함 몬스터");
            if (componentFoldouts[sub])
            {
                foreach (var prefab in monsterPrefabs)
                {
                    if (prefab.GetComponent(sub) != null)
                        EditorGUILayout.ObjectField(prefab, typeof(GameObject), false);
                }
            }
        }
    }
}
