using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D Bernique Utils/Better Tiles/Instance Tile")]
public class InstanceTile : Tile {

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go) {
        //Debug.Log(go);
        return base.StartUp(position, tilemap, go);
    }

}