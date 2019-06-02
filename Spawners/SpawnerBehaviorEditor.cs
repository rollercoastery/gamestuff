using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpawnerBehavior))]
public class SpawnerBehaviorEditor : Editor {

    private SpawnerBehavior sb;

    SerializedProperty numberOfEnemiesToSpawn;
    SerializedProperty spawnType;

    void OnEnable()
    {
        sb = (SpawnerBehavior)target;

        numberOfEnemiesToSpawn = serializedObject.FindProperty("numberOfEnemiesToSpawn");
        spawnType = serializedObject.FindProperty("spawnType");
    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();

    #region SPAWN SETTINGS
        EditorGUILayout.LabelField("Spawning Options", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(new GUIContent("Number of Enemies to Spawn",
                                                  "-1 to 100. -1 for infinite."), GUILayout.Width(205));
        sb.numberOfEnemiesToSpawn = EditorGUILayout.IntSlider(sb.numberOfEnemiesToSpawn, -1, 100);
        GUILayout.EndHorizontal();
    #endregion

    #region SPAWNING OPTIONS
        EditorGUILayout.LabelField("Spawning Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(spawnType);

        // Using the enum to show/hide various variables
        if (sb.spawnType == SpawnerBehavior.SpawnType.Static)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spawn Timer", GUILayout.Width(205));
            sb.spawnTimer = EditorGUILayout.FloatField(sb.spawnTimer);
            GUILayout.EndHorizontal();
        }
        else if (sb.spawnType == SpawnerBehavior.SpawnType.RandomRange)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("From Spawn Timer", GUILayout.Width(205));
            sb.fromSpawnTimer = EditorGUILayout.FloatField(sb.fromSpawnTimer);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("To Spawn Timer", GUILayout.Width(205));
            sb.toSpawnTimer = EditorGUILayout.FloatField(sb.toSpawnTimer);
            GUILayout.EndHorizontal();
        }
    #endregion

        serializedObject.ApplyModifiedProperties();
    }
}
