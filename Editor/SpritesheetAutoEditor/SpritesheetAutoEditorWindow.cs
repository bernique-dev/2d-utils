using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class SpritesheetAutoEditorWindow : EditorWindow {

    public string textureName;
    public string texturePath;
    public TextureImporter textureImporter;

    private RuleTileDirection direction;
    private string ruleTilePath = "Assets/Tiles/";
    public RuleTile.TilingRuleOutput.OutputSprite output;

    private GameObject prefab;
    private bool needsPreview;

    public float minAnimSpeed = 1;
    public float maxAnimSpeed = 1;

    private Vector2 tileSize = new Vector2(16, 16);
    private Vector2 pivot = new Vector2(.5f, .5f);
    private int framesNumber = 1;
    private int tilesNumber = 48;
    private Vector2Int tileSheetDimensions = new Vector2Int(12,4);

    private string[] suffixes = {
        "TL",   "T",    "TR",   "TLR",  "BRC",  "BLC",  "TL-BRC",   "TR-BLC",   "L-BRC",    "T-BLC",    "TRC-BLC-BRC",  "TLC-BLC-BRC",
        "L",    "N",    "R",    "LR",   "TRC",  "TLC",  "BL-TRC",   "BR-TLC",   "B-TRC",    "R-TLC",    "TLC-TRC-BRC",  "TLC-TRC-BLC",
        "BL",   "B",   "BR",   "BLR",  "T-BRC","R-BLC","TRC-BRC",  "TLC-TRC",  "TLC-BRC",  "TRC-BLC",  "L-TRC-BRC",    "T-BLC-BRC",
        "TBL",  "TB",   "TBR",  "TBLR", "L-TRC","B-TLC","BLC-BRC",  "TLC-BLC",  "TLC-TRC-BLC-BRC","E",  "B-TLC-TRC",    "R-TLC-BLC"
    };

    void OnGUI() {
        if (Selection.activeObject) {
            if (Selection.activeObject.GetType() == typeof(Texture2D)) {
                EditorGUIUtility.wideMode = true;

                texturePath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
                string[] splitPath = texturePath.Split('/');
                string[] splitFileName = splitPath[splitPath.Length - 1].Split('.');
                textureName = splitFileName[0];
                textureImporter = TextureImporter.GetAtPath(texturePath) as TextureImporter;

                GUILayout.Label("Spritesheet Editor (" + textureName + ")", EditorStyles.boldLabel);

                tileSize = EditorGUILayout.Vector2Field("Tile size", tileSize);
                pivot = EditorGUILayout.Vector2Field("Pivot", pivot);
                framesNumber = EditorGUILayout.IntField("Number of frames", framesNumber);

                if (GUILayout.Button("Slice")) {

                    SpriteMetaData[] spriteSheet = new SpriteMetaData[48 * framesNumber];
                    for (int frame = 0; frame < framesNumber; frame++) {
                        for (int idx = 0; idx < suffixes.Length; idx++) {
                            SpriteMetaData spriteMetaData = new SpriteMetaData();
                            spriteMetaData.name = textureName + "_" + suffixes[idx] + (framesNumber > 1 ? "_" + frame : "");
                            spriteMetaData.alignment = 9;
                            spriteMetaData.pivot = pivot;
                            spriteMetaData.rect = new Rect {
                                x = ((idx % tileSheetDimensions.x) + tileSheetDimensions.x * frame) * tileSize.x,
                                y = tileSize.y * (3 - (idx / tileSheetDimensions.x)),
                                width = tileSize.x,
                                height = tileSize.y,
                            };
                            spriteSheet[idx + tilesNumber * frame] = spriteMetaData;
                        }
                    }
                    textureImporter.spritePivot = pivot;


                    TextureImporterSettings texSettings = new TextureImporterSettings();
                    textureImporter.ReadTextureSettings(texSettings);
                    texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
                    textureImporter.SetTextureSettings(texSettings);

                    textureImporter.isReadable = true;
                    textureImporter.spriteImportMode = SpriteImportMode.Multiple;
                    textureImporter.spritePixelsPerUnit = tileSize.x;
                    textureImporter.filterMode = FilterMode.Point;
                    textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                    textureImporter.spritesheet = spriteSheet;
                    textureImporter.SaveAndReimport();
                    AssetDatabase.SaveAssets();
                }
                ruleTilePath = EditorGUILayout.TextField(ruleTilePath);
                if (GUILayout.Button("Create RuleTile")) {
                    RuleTile activeRuleTile = CreateRuleTile();
                    AssetDatabase.CreateAsset(activeRuleTile, ruleTilePath + textureName + ".asset");
                    if (needsPreview) {
                        RuleTile passiveRuleTile = CreateRuleTile();
                        AssetDatabase.CreateAsset(passiveRuleTile, ruleTilePath + textureName + "_preview.asset");
                    }
                }
            } else {
                GUILayout.Label("No spritesheet selected");
            }
        } else {
            GUILayout.Label("No spritesheet selected");
        }
    }

    private RuleTile CreateRuleTile() {
        RuleTile ruleTile = (RuleTile)ScriptableObject.CreateInstance(typeof(RuleTile));

        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(texturePath).OfType<Sprite>().ToArray();

        if (framesNumber > 1) {
            ruleTile.m_DefaultSprite = FindSprite(sprites, "TBLR", 0);
        } else {
            ruleTile.m_DefaultSprite = FindSprite(sprites, "TBLR");
        }
        foreach(RuleTileConfiguration rtc in Enum.GetValues(typeof(RuleTileConfiguration))) {
            if (rtc == RuleTileConfiguration.E) continue;
            RuleTile.TilingRule rule = new RuleTile.TilingRule();
            rule.ApplyNeighbors(rtc.GetRuleTileConfiguration(direction));
            ruleTile.m_TilingRules.Add(rule);

            List<Sprite> frames = new List<Sprite>();
            if (framesNumber > 1) {
                for (int f = 0; f < framesNumber; f++) {
                    frames.Add(FindSprite(sprites, rtc.ToString(), f));
                }
            } else {
                frames.Add(FindSprite(sprites, rtc.ToString()));
            }
            rule.m_Sprites = frames.ToArray();
            rule.m_Output = output;
            rule.m_MinAnimationSpeed = minAnimSpeed;
            rule.m_MaxAnimationSpeed = maxAnimSpeed;
        }
        return ruleTile;
    }


    private Sprite FindSprite(Sprite[] sprites, string suffix) {
        foreach (Sprite sprite in sprites) {
            string[] splitString = sprite.name.Split('_');
            if (splitString[splitString.Length-1].Replace('-','_') == suffix) return sprite;
        }
        return null;
    }

        private Sprite FindSprite(Sprite[] sprites,string suffix, int frame) {
        foreach (Sprite sprite in sprites) {
            string[] splitString = sprite.name.Split('_');
            if (splitString[splitString.Length - 2].Replace('-', '_') == suffix && sprite.name.Split('_')[2] == frame.ToString()) return sprite;
        }
        return null;
    }
}
