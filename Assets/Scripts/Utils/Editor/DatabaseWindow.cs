using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class DatabaseWindow : EditorWindow
{
    public Team[] teams = new Team[0];
    public GameTile[] gameTiles = new GameTile[0];
    public AbilityBase[] abilities = new AbilityBase[0];

    ReorderableList reorderableListTeams;
    ReorderableList reorderableListGameTiles;
    ReorderableList reorderableListAbilities;

    const string databasePath = "Assets/Database/";

    SerializedObject serializedObject;

    [MenuItem("Window/Database")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(DatabaseWindow), false, "Database").Show();
    }

    private ReorderableList CreateReorderableList(string property, string header)
    {
        ReorderableList list = new ReorderableList(serializedObject, serializedObject.FindProperty(property), true, true, false, false);
        list.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, header + " : " + list.count + " elements found");
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => DrawElement(rect, index, isActive, isFocused, list);
        return list;
    }

    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused, ReorderableList list)
    {
        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(rect, list.serializedProperty.GetArrayElementAtIndex(index), true);
    }

    private void OnFocus()
    {
        teams = new Team[0];
        serializedObject = new SerializedObject(this);
        if (serializedObject != null)
        {
            LoadGameData("team", "Teams", ref teams);
            LoadGameData("gametile", "GameTiles", ref gameTiles);
            LoadGameData("abilitybase", "Abilities", ref abilities);
        }
    }

    int selectedTab;

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Force Refresh data"))
        {
            LoadGameData("team", "Teams", ref teams);
            LoadGameData("gametile", "GameTiles", ref gameTiles);
            LoadGameData("abilitybase", "Abilities", ref abilities);
        }

        selectedTab = GUILayout.Toolbar(selectedTab, new string[] { "All", "Teams", "GameTiles", "Abilities" });

        DisplayAllLists();
    }

    private void DisplayAllLists()
    {
        if (selectedTab == 1 || selectedTab == 0)
        {
            if(reorderableListTeams == null)
            {
                reorderableListTeams = CreateReorderableList("teams", "Teams");
            }
            DisplayList(reorderableListTeams);
            EditorGUILayout.Space();
        }
        if (selectedTab == 2 || selectedTab == 0)
        {
            if (reorderableListGameTiles == null)
            {
                reorderableListGameTiles = CreateReorderableList("gameTiles", "GameTiles");
            }
            DisplayList(reorderableListGameTiles);
            EditorGUILayout.Space();
        }
        if (selectedTab == 3 || selectedTab == 0)
        {
            if (reorderableListAbilities == null)
            {
                reorderableListAbilities = CreateReorderableList("abilities", "Abilities");
            }
            DisplayList(reorderableListAbilities);
        }
    }

    private void DisplayList(ReorderableList list)
    {
        serializedObject.Update();
        list.DoLayoutList();
    }

    private void LoadGameData<T>(string type, string typePath, ref T[] output) where T : Object
    {
        List<T> objects = new List<T>();
        string[] guids = AssetDatabase.FindAssets("t:" + type, new[] { databasePath + typePath });
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            objects.Add((T)AssetDatabase.LoadAssetAtPath(path, typeof(T)));
        }
        
        output = objects.ToArray();
    }
}