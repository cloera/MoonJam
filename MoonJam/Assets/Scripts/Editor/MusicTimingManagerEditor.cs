using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MusicTimingManager))]
public class MusicTimingManagerEditor : Editor
{
    private SerializedObject musicTimingManagerSO;
    private MusicTimingManager manager;

    private SerializedProperty bpm;
    private SerializedProperty prefab;
    private SerializedProperty lanePrefabs;

    // private void OnEnable()
    // {
    //     musicTimingManagerSO = new SerializedObject(target);
    //     manager = (MusicTimingManager)target;

    //     bpm = musicTimingManagerSO.FindProperty("beatsPerMinute");
    //     prefab = musicTimingManagerSO.FindProperty("musicObjectPrefab");
    //     lanePrefabs = musicTimingManagerSO.FindProperty("lanePrefabs");
    // }

    // public override void OnInspectorGUI()
    // {
    //     musicTimingManagerSO.Update();

    //     EditorGUILayout.PropertyField(bpm);
    //     EditorGUILayout.PropertyField(prefab);
    //     EditorGUILayout.PropertyField(lanePrefabs);

    //     EditorGUILayout.BeginVertical();

    //     int count = 0;
    //     foreach(MusicCommand command in manager.musicCommands)
    //     {
    //         SerializedObject commandSO = new SerializedObject(command);
    //         SerializedProperty musicClip = commandSO.FindProperty("musicClip");
    //         SerializedProperty note = commandSO.FindProperty("note");

    //         EditorGUILayout.Foldout(true, "Music Command " + count);
    //         EditorGUILayout.PropertyField(musicClip);
    //         EditorGUILayout.PropertyField(note);
    //         count++;
    //     }

    //     EditorGUILayout.EndVertical();

    //     if(GUILayout.Button("Add Music Command"))
    //     {
    //         GameObject tempGO = Instantiate(manager.musicObjectPrefab);
    //         manager.musicCommands.Add(tempGO.GetComponent<MusicCommand>());
    //     }

    //     if(GUILayout.Button("Remove Music Command"))
    //     {
    //         int lastIndex = manager.musicCommands.Count - 1;
    //         GameObject tempGO = manager.musicCommands[lastIndex].gameObject;
    //         manager.musicCommands.RemoveAt(lastIndex);
    //         DestroyImmediate(tempGO);
    //     }

    //     musicTimingManagerSO.ApplyModifiedProperties();
    // }
}
