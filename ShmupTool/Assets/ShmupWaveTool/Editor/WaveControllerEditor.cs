using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

[CustomEditor(typeof(WaveController))]
public class WaveControllerEditor : Editor {
    private ReorderableList list;

    private void OnEnable(){
        CostumWaveInspector();
    }

    private void CostumWaveInspector()
    {
        //draws the thing
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("Waves"), true, true, true, true);
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {

            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Type"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("enemyFormation"), GUIContent.none);

            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.IntSlider(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("formationEnemyCount"), 1, 10);

            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.Slider(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("spawnValueYPos"), -20, 20);

            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.Slider(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("spawnWaitTime"), 0, 20);
        };
        //header name
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Enemy Waves");
        };
        list.elementHeightCallback = (int index) =>
        {
            return EditorGUIUtility.singleLineHeight * 2 + 40;
        };
        list.onSelectCallback = (ReorderableList l) => {
            var prefab = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("enemyFormation").objectReferenceValue as GameObject;
            if(prefab) EditorGUIUtility.PingObject(prefab.gameObject);
        };

        list.onCanRemoveCallback = (ReorderableList l) => {
            return l.count > 1;
        };
        //remove wave
        list.onRemoveCallback = (ReorderableList l) => {
            if(EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the wave?", "Yes", "No"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            }
        };
        //add wave
        list.onAddCallback = (ReorderableList l) => {
            var index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = index;
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Type").enumValueIndex = 0;
            element.FindPropertyRelative("formationEnemyCount").intValue = 20;
            element.FindPropertyRelative("enemyFormation").objectReferenceValue = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Formations/E_CircleFormation.prefab", typeof(GameObject)) as GameObject;
        };

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {
            var menu = new GenericMenu();
            //create the submenus for dropdown

            //Circle DataPath
            var guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Circle" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Circle/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            //Hexagon DataPath
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Hexagon" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Hexagon/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            //Pentagon DataPath
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Pentagon" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Pentagon/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            // Square DataPath
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Square" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Square/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            //Triangle DataPath
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Triangle" });
            foreach(var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Triangle/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            //Boss DataPath
            guids = AssetDatabase.FindAssets("", new[] { "Assets/Prefabs/Formations/Boss" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Boss/" + Path.GetFileNameWithoutExtension(path)), false, clickHandler, new WaveCreationParams() { Type = MobWave.WaveType.Enemy, Path = path });
            }

            menu.ShowAsContext();
        };
    }

    public override void OnInspectorGUI(){
        //base.OnInspectorGUI();
        
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        WaveController waveControl = (WaveController)target;

        GUILayout.Label("amount of time to start spawning waves:");
        waveControl.startWaitTime = EditorGUILayout.Slider(waveControl.startWaitTime, 1, 10);

        // For saving the stuff
        if(GUILayout.Button("Save Waves"))
        {
            Debug.Log("Saving...");
            SaveSystem.SaveWaves(waveControl);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Saving Complete!");
        }

        // For loading he stuff
        if(GUILayout.Button("Load Waves"))
        {
            Debug.Log("Loading...");
            int tempListCount = list.count;

            for(int i = 0; i < tempListCount; i++){
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }

            //while(list.count > 0)
            //{
            //    ReorderableList.defaultBehaviours.DoRemoveButton(list);
            //}
            CostumWaveInspector();
            LoadWaves();
        }
    }

    private void LoadWaves()
    {
        WaveData data = SaveSystem.LoadWaves();
        for(int i = 0; i < data.waveSize; i++)
        {
            var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            element.FindPropertyRelative("Type").enumValueIndex = (int)data.type[i];
            element.FindPropertyRelative("formationEnemyCount").intValue = data.formationEnemyCount[i];
            element.FindPropertyRelative("spawnValueYPos").floatValue = data.spawnValueYPos[i];
            element.FindPropertyRelative("spawnWaitTime").floatValue = data.spawnWaitTime[i];
            Debug.Log("Load Path: " + data.waveTypes[i]);

            element.FindPropertyRelative("enemyFormation").objectReferenceValue = AssetDatabase.LoadAssetAtPath(data.waveTypes[i], typeof(GameObject));

            serializedObject.ApplyModifiedProperties();
        }
        serializedObject.Update();
        list.DoLayoutList();
        Debug.Log("Loading Complete!");
    }
    private void clickHandler(object target){
        var data = (WaveCreationParams)target;
        var index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        element.FindPropertyRelative("Type").enumValueIndex = (int)data.Type;
        element.FindPropertyRelative("formationEnemyCount").intValue = data.Type == MobWave.WaveType.Enemy ? 1 : 20;
        element.FindPropertyRelative("enemyFormation").objectReferenceValue = AssetDatabase.LoadAssetAtPath(data.Path, typeof(GameObject)) as GameObject;
        serializedObject.ApplyModifiedProperties();
    }

    private struct WaveCreationParams{
        public MobWave.WaveType Type;
        public string Path;
    }

}
