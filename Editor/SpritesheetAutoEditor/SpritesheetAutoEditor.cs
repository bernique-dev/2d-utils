using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritesheetAutoEditor : Editor {
    [MenuItem("Assets/2D Bernique Utils/Autoslice")]
    private static void DoSomething() {
        string path = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        SpritesheetAutoEditorWindow saeWindow = EditorWindow.GetWindow<SpritesheetAutoEditorWindow>("Spritesheet Editor");
        saeWindow.maxSize = new Vector2(500, 310);
        //TextureImporter textureImporter = TextureImporter.GetAtPath(path) as TextureImporter;
        //saeWindow.textureImporter = textureImporter;
        //saeWindow.textureName = Selection.activeObject.name;
        //saeWindow.texturePath = path;

        saeWindow.Show();
    }

    [MenuItem("Assets/T00ls/Autoslice", true)]
    private static bool DoSomethingValidation() {
        return Selection.activeObject.GetType() == typeof(Texture2D);
    }

}