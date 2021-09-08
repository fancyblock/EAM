using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public class GameMenus : EditorWindow
{
    [MenuItem("EAM/Open Game Scene")]
    static public void OnOpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Game/Scenes/Game.unity");
    }

    [MenuItem("EAM/Open Config Folder")]
    static public void OnOpenConfigFolder()
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/GameSettings~");
    }
}
