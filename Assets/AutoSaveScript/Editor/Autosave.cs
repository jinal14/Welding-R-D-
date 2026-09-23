using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class AutoSave
{
    private static float saveInterval = 120f; // 300 seconds = 5 minutes
    private static double lastSaveTime;

    static AutoSave()
    {
        EditorApplication.playModeStateChanged += SaveOnPlay;
        EditorApplication.update += Update;
        lastSaveTime = EditorApplication.timeSinceStartup;
    }

    private static void Update()
    {
        if (EditorApplication.timeSinceStartup - lastSaveTime > saveInterval)
        {
            SaveScene();
            lastSaveTime = EditorApplication.timeSinceStartup;
        }
    }

    private static void SaveOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveScene();
        }
    }

    private static void SaveScene()
    {
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            Debug.Log("Auto-saving scene...");
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
    }
}
