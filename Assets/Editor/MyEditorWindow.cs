using UnityEditor;
using UnityEngine;

public class MyEditorWindow : EditorWindow
{
    ObjectPreview myPreview;

    [MenuItem("Window/My Custom Preview")]
    public static void ShowWindow()
    {
        GetWindow<MyEditorWindow>("My Preview");
    }

    private void OnEnable()
    {
        // preview initialize करने का code अगर हो तो
        myPreview = new ObjectPreview();
    }

    private void OnDisable()
    {
        // यही है FIX 👇
        if (myPreview != null)
        {
            myPreview.Cleanup();
            myPreview = null;
        }
    }

    private void OnGUI()
    {
        // कुछ भी draw करना हो तो यहां करो
        GUILayout.Label("Custom Preview Window");
    }
}
