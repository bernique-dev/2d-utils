using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TextureAtlas;
using System.Linq;
using System.IO;
using UnityEditor.U2D.Sprites;
using UnityEngine.UIElements;

public class SlicerEditorWindow : EditorWindow {

    private string[] texturePaths;
    private string[] textureNames {
        get {
            return texturePaths.Select(path => GetTextureName(path)).ToArray();
        }
    }
    private int pixelsPerUnit = 16;

    //private Vector2Int spriteSize = new Vector2Int(16, 16);
    private Vector2 pivot = new Vector2(.5f, .5f);
    private Vector2Int spriteNumber = new Vector2Int(1,1);

    [MenuItem("Assets/2D Bernique Utils/Slicer")]
    private static void DoSomething() {
        SlicerEditorWindow slicerWindow = GetWindow<SlicerEditorWindow>("Slicer");
        slicerWindow.maxSize = new Vector2(500, 310);
        slicerWindow.Show();
    }

    [MenuItem("Assets/2D Bernique Utils/Slicer", true)]
    private static bool DoSomethingValidation() {
        return Selection.activeObject.GetType() == typeof(Texture2D);
    }


    public void OnGUI() {

        spriteNumber = EditorGUILayout.Vector2IntField("Sprite number", spriteNumber);
        //spriteSize = EditorGUILayout.Vector2IntField("Sprite size", spriteSize);
        pivot = EditorGUILayout.Vector2Field("Pivot", pivot);
        pixelsPerUnit = EditorGUILayout.IntField("Pixels per Unit", pixelsPerUnit);

        texturePaths = Selection.assetGUIDs.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();

        GUILayout.Label($"({texturePaths.Length} texture{(texturePaths.Length > 1 ? 's' : "")} selected.)");

        if (GUILayout.Button("Slice")) {
            Slice();
        }

        Repaint();
    }

    private string GetTextureName(string texturePath) {
        string[] splitPath = texturePath.Split('/');
        string[] splitFileName = splitPath[splitPath.Length - 1].Split('.');
        return splitFileName[0];
    }

    private void Slice() {
        var factory = new SpriteDataProviderFactories();
        factory.Init();
        foreach (var texturePath in texturePaths) {
            var textureImporter = TextureImporter.GetAtPath(texturePath) as TextureImporter;

            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
            textureImporter.isReadable = true;
            textureImporter.spritePixelsPerUnit = pixelsPerUnit;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.filterMode = FilterMode.Point;

            int width;
            int height;
            textureImporter.GetSourceTextureWidthAndHeight(out width, out height);

            var dataProvider = factory.GetSpriteEditorDataProviderFromObject(textureImporter);
            dataProvider.InitSpriteEditorDataProvider();
            var spriteRects = new List<SpriteRect>();
            //for (int x = 0; x < width; x += spriteSize.x) {
            //    for (int y = 0; y < height; y += spriteSize.y) {
            //        var spriteRect = new SpriteRect();
            //        spriteRect.pivot = pivot;
            //        spriteRect.rect = new Rect(x, y, spriteSize.x, spriteSize.y);
            //        spriteRects.Add(spriteRect);
            //    }
            //}
            var spriteSize = CalculateSpriteSize(spriteNumber, new Vector2Int(width, height));
            for (int x = 0; x < width; x += spriteSize.x) {
                for (int y = 0; y < height; y += spriteSize.y) {
                    var spriteRect = new SpriteRect();
                    spriteRect.name = $"{GetTextureName(texturePath)}_{x / spriteSize.x}_{y / spriteSize.y}";
                    spriteRect.alignment = SpriteAlignment.Custom;
                    spriteRect.pivot = pivot;
                    spriteRect.rect = new Rect(x, y, spriteSize.x, spriteSize.y);
                    spriteRects.Add(spriteRect);
                }
            }
            dataProvider.SetSpriteRects(spriteRects.ToArray());
            dataProvider.Apply();
            textureImporter.SaveAndReimport();
        }
        Debug.Log("Sliced!");

    }

    private Vector2Int CalculateSpriteNumber(Vector2Int spriteSize, Vector2Int textureSize) {
        return new Vector2Int(textureSize.x / spriteSize.x, textureSize.y / spriteSize.y);
    }

    private Vector2Int CalculateSpriteSize(Vector2Int spriteNumber, Vector2Int textureSize) {
        return new Vector2Int(textureSize.x / spriteNumber.x, textureSize.y / spriteNumber.y);
    }
}
