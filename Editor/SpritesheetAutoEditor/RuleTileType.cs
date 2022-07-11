using System.Collections.Generic;
using UnityEngine;
using System;

public enum RuleTileConfiguration {
    TL, T, TR, TLR, BRC, BLC, TL_BRC, TR_BLC, L_BRC, T_BLC, B_TLC_TRC, TRC_BLC_BRC, TLC_BLC_BRC,
    L, N, R, LR, TRC, TLC, BL_TRC, BR_TLC, B_TRC, R_TLC, TLC_TRC_BRC, TLC_TRC_BLC,
    BL, B, BR, BLR, T_BRC, R_BLC, TRC_BRC, TLC_TRC, TLC_BRC, TRC_BLC, L_TRC_BRC, T_BLC_BRC,
    TBL, TB, TBR, TBLR, L_TRC, B_TLC, BLC_BRC, TLC_BLC, TLC_TRC_BLC_BRC, E, R_TLC_BLC
}

public static class RuleTileConfigurationExtensions {


    public static Dictionary<Vector3Int, int> GetRuleTileConfiguration(this RuleTileConfiguration rtc, RuleTileDirection dir) {
        Vector3Int[] neighboursPositions = new Vector3Int[8] {
            new Vector3Int(-1,1,0), new Vector3Int(0,1,0), new Vector3Int(1,1,0),
            new Vector3Int(-1,0,0),                         new Vector3Int(1,0,0),
            new Vector3Int(-1,-1,0), new Vector3Int(0,-1,0), new Vector3Int(1,-1,0)
        };
        Dictionary<Vector3Int, int> dict = new Dictionary<Vector3Int, int>();
        int[] neighbours = rtc.GetNeighbours(dir);
        if (neighbours != null) {
            for (int i = 0; i < neighboursPositions.Length; i++) {
                if (neighbours[i] != 0) {
                    dict.Add(neighboursPositions[i], neighbours[i]);
                }
            }
        }
        return dict;
    }

    private static int[] GetNeighbours(this RuleTileConfiguration rtc, RuleTileDirection dir) {
        int up = 1;
        int down = 1;
        int left = 1;
        int right = 1;
        int corner = 1;
        int empty = 2;

        int[] neighbours = null;
        switch (rtc) {
            case RuleTileConfiguration.TL:
                neighbours = new int[8]{
                    0, empty, 0,
                    empty,    right,
                    0, down, corner
                };
                break;
            case RuleTileConfiguration.T:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    right,
                    corner, down, corner
                };
                break;
            case RuleTileConfiguration.TR:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    empty,
                    corner, down, 0
                };
                break;
            case RuleTileConfiguration.TLR:
                neighbours = new int[8]{
                    0, empty, 0,
                    empty,    empty,
                    0, down, 0
                };
                break;
            case RuleTileConfiguration.BRC:
                neighbours = new int[8]{
                    corner, up, corner,
                    left,    right,
                    corner, down, empty
                };
                break;
            case RuleTileConfiguration.BLC:
                neighbours = new int[8]{
                    corner, up, corner,
                    left,    right,
                    empty, down, corner
                };
                break;
            case RuleTileConfiguration.TL_BRC:
                neighbours = new int[8]{
                    0, empty, 0,
                    empty,    right,
                    0, down, empty
                };
                break;
            case RuleTileConfiguration.TR_BLC:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    empty,
                    empty, down, 0
                };
                break;
            case RuleTileConfiguration.L_BRC:
                neighbours = new int[8]{
                    0, up, corner,
                    empty,    right,
                    0, down, empty
                };
                break;
            case RuleTileConfiguration.T_BLC:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    right,
                    empty, down, corner
                };
                break;
            case RuleTileConfiguration.TRC_BLC_BRC:
                neighbours = new int[8]{
                    corner, up, empty,
                    left,    right,
                    empty, down, empty
                };
                break;
            case RuleTileConfiguration.TLC_BLC_BRC:
                neighbours = new int[8]{
                    empty, up, corner,
                    left,    right,
                    empty, down, empty
                };
                break;
            case RuleTileConfiguration.L:
                neighbours = new int[8]{
                    0, up, corner,
                    empty,    right,
                    0, down, corner
                };
                break;
            case RuleTileConfiguration.N:
                neighbours = new int[8]{
                    corner, up, corner,
                    left,    right,
                    corner, down, corner
                };
                break;
            case RuleTileConfiguration.R:
                neighbours = new int[8]{
                    corner, up, 0,
                    left,    empty,
                    corner, down, 0
                };
                break;
            case RuleTileConfiguration.LR:
                neighbours = new int[8]{
                    0, up, 0,
                    empty,    empty,
                    0, down, 0
                };
                break;
            case RuleTileConfiguration.TRC:
                neighbours = new int[8]{
                    corner, up, empty,
                    left,    right,
                    corner, down, corner
                };
                break;
            case RuleTileConfiguration.TLC:
                neighbours = new int[8]{
                    empty, up, corner,
                    left,    right,
                    corner, down, corner
                };
                break;
            case RuleTileConfiguration.BL_TRC:
                neighbours = new int[8]{
                    0, up, empty,
                    empty,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.BR_TLC:
                neighbours = new int[8]{
                    empty, up, 0,
                    left,    empty,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.B_TRC:
                neighbours = new int[8]{
                    0, up, empty,
                    left,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.R_TLC:
                neighbours = new int[8]{
                    empty, up, 0,
                    left,    empty,
                    corner, down, 0
                };
                break;
            case RuleTileConfiguration.TLC_TRC_BRC:
                neighbours = new int[8]{
                    empty, up, empty,
                    left,    right,
                    corner, down, empty
                };
                break;
            case RuleTileConfiguration.TLC_TRC_BLC:
                neighbours = new int[8]{
                    empty, up, empty,
                    left,    right,
                    empty, down, corner
                };
                break;
            case RuleTileConfiguration.BL:
                neighbours = new int[8]{
                    0, up, corner,
                    empty,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.B:
                neighbours = new int[8]{
                    corner, up, corner,
                    left,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.BR:
                neighbours = new int[8]{
                    corner, up, 0,
                    left,    empty,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.BLR:
                neighbours = new int[8]{
                    0, up, 0,
                    empty,    empty,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.T_BRC:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    right,
                    corner, down, empty
                };
                break;
            case RuleTileConfiguration.R_BLC:
                neighbours = new int[8]{
                    corner, up, 0,
                    left,    empty,
                    empty, down, 0
                };
                break;
            case RuleTileConfiguration.TRC_BRC:
                neighbours = new int[8]{
                    corner, up, empty,
                    left,    right,
                    corner, down, empty
                };
                break;
            case RuleTileConfiguration.TLC_TRC:
                neighbours = new int[8]{
                    empty, up, empty,
                    left,    right,
                    corner, down, corner
                };
                break;
            case RuleTileConfiguration.TLC_BRC:
                neighbours = new int[8]{
                    empty, up, corner,
                    left,    right,
                    corner, down, empty
                };
                break;
            case RuleTileConfiguration.TRC_BLC:
                neighbours = new int[8]{
                    corner, up, empty,
                    left,    right,
                    empty, down, corner
                };
                break;
            case RuleTileConfiguration.L_TRC_BRC:
                neighbours = new int[8]{
                    0, up, empty,
                    empty,    right,
                    0, down, empty
                };
                break;
            case RuleTileConfiguration.T_BLC_BRC:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    right,
                    empty, down, empty
                };
                break;
            case RuleTileConfiguration.TBL:
                neighbours = new int[8]{
                    0, empty, 0,
                    empty,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.TB:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.TBR:
                neighbours = new int[8]{
                    0, empty, 0,
                    left,    empty,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.TBLR:
                neighbours = new int[8]{
                    0, empty, 0,
                    empty,    empty,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.L_TRC:
                neighbours = new int[8]{
                    0, up, empty,
                    empty,    right,
                    0, down, corner
                };
                break;
            case RuleTileConfiguration.B_TLC:
                neighbours = new int[8]{
                    empty, up, corner,
                    left,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.BLC_BRC:
                neighbours = new int[8]{
                    corner, up, corner,
                    left,    right,
                    empty, down, empty
                };
                break;
            case RuleTileConfiguration.TLC_BLC:
                neighbours = new int[8]{
                    empty, up, corner,
                    left,    right,
                    empty, down, corner
                };
                break;
            case RuleTileConfiguration.TLC_TRC_BLC_BRC:
                neighbours = new int[8]{
                    empty, up, empty,
                    left,    right,
                    empty, down, empty
                };
                break;
            case RuleTileConfiguration.E:
                break;
            case RuleTileConfiguration.B_TLC_TRC:
                neighbours = new int[8]{
                    empty, up, empty,
                    left,    right,
                    0, empty, 0
                };
                break;
            case RuleTileConfiguration.R_TLC_BLC:
                neighbours = new int[8]{
                    empty, up, 0,
                    left,    empty,
                    empty, down, 0
                };
                break;
            default:

                break;
        }
        return neighbours;
    }

}