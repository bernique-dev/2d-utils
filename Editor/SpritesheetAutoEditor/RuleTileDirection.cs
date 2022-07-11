    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RuleTileDirection {
    Up, Down, Left, Right
}

public static class DirectionExtensions {

    public static Vector2 GetVector2(this RuleTileDirection direction) {
        Vector2 result;
        switch (direction) {
            case RuleTileDirection.Up:
                result = Vector2.up;
                break;
            case RuleTileDirection.Down:
                result = Vector2.down;
                break;
            case RuleTileDirection.Left:
                result = Vector2.left;
                break;
            case RuleTileDirection.Right:
                result = Vector2.right;
                break;
            default:
                result = Vector2.zero;
                break;
        }
        return result;
    }

    public static Vector3 GetVector3(this RuleTileDirection direction) {
        Vector2 result;
        switch (direction) {
            case RuleTileDirection.Up:
                result = Vector3.up;
                break;
            case RuleTileDirection.Down:
                result = Vector3.down;
                break;
            case RuleTileDirection.Left:
                result = Vector3.left;
                break;
            case RuleTileDirection.Right:
                result = Vector2.right;
                break;
            default:
                result = Vector3.zero;
                break;
        }
        return result;
    }

    public static Quaternion GetRotation(this RuleTileDirection direction) {
        Quaternion result;
        switch (direction) {
            case RuleTileDirection.Up:
                result = Quaternion.Euler(0, 0, 0);
                break;
            case RuleTileDirection.Down:
                result = Quaternion.Euler(0, 0, 180);
                break;
            case RuleTileDirection.Left:
                result = Quaternion.Euler(0, 0, 90);
                break;
            case RuleTileDirection.Right:
                result = Quaternion.Euler(0, 0, 270);
                break;
            default:
                result = Quaternion.Euler(0, 0, 0);
                break;
        }
        return result;
    }

    public static RuleTileDirection RotateClockwise(this RuleTileDirection direction) {
        RuleTileDirection result;
        switch (direction) {
            case RuleTileDirection.Up:
                result = RuleTileDirection.Right;
                break;
            case RuleTileDirection.Down:
                result = RuleTileDirection.Left;
                break;
            case RuleTileDirection.Left:
                result = RuleTileDirection.Up;
                break;
            case RuleTileDirection.Right:
                result = RuleTileDirection.Down;
                break;
            default:
                result = RuleTileDirection.Up;
                break;
        }
        return result;
    }

    public static RuleTileDirection RotateAntiClockwise(this RuleTileDirection direction) {
        RuleTileDirection result;
        switch (direction) {
            case RuleTileDirection.Up:
                result = RuleTileDirection.Left;
                break;
            case RuleTileDirection.Down:
                result = RuleTileDirection.Right;
                break;
            case RuleTileDirection.Left:
                result = RuleTileDirection.Down;
                break;
            case RuleTileDirection.Right:
                result = RuleTileDirection.Up;
                break;
            default:
                result = RuleTileDirection.Up;
                break;
        }
        return result;
    }

    public static bool IsHorizontal(this RuleTileDirection direction) {
        return direction == RuleTileDirection.Left || direction == RuleTileDirection.Right;
    }

    public static bool IsVertical(this RuleTileDirection direction) {
        return direction == RuleTileDirection.Up || direction == RuleTileDirection.Down;
    }

    public static bool IsDirectedTo(this Vector2 vector, RuleTileDirection direction) {
        bool result = false;
        switch (direction) {
            case RuleTileDirection.Up:
                result = vector.y > 0;
                break;
            case RuleTileDirection.Down:
                result = vector.y < 0;
                break;
            case RuleTileDirection.Left:
                result = vector.x < 0;
                break;
            case RuleTileDirection.Right:
                result = vector.x > 0;
                break;
        }
        return result;
    }

    public static bool IsDirectedTo(this Vector3 vector, RuleTileDirection direction) {
        bool result = false;
        switch (direction) {
            case RuleTileDirection.Up:
                result = vector.y > 0;
                break;
            case RuleTileDirection.Down:
                result = vector.y < 0;
                break;
            case RuleTileDirection.Left:
                result = vector.x < 0;
                break;
            case RuleTileDirection.Right:
                result = vector.x > 0;
                break;
        }
        return result;
    }

    public static RuleTileDirection GetDirection(this Vector3Int vector) {
        RuleTileDirection result = RuleTileDirection.Up;
        if (vector == Vector3Int.left) {
            result = RuleTileDirection.Left;
        }
        else if (vector == Vector3Int.right) {
            result = RuleTileDirection.Right;
        }
        else if (vector == Vector3Int.down) {
            result = RuleTileDirection.Down;
        }
        return result;
    }

    public static RuleTileDirection GetOppositeDirection(this RuleTileDirection direction) {
        RuleTileDirection result = RuleTileDirection.Up;
        switch (direction) {
            case RuleTileDirection.Up:
                result = RuleTileDirection.Down;
                break;
            case RuleTileDirection.Down:
                result = RuleTileDirection.Up;
                break;
            case RuleTileDirection.Left:
                result = RuleTileDirection.Right;
                break;
            case RuleTileDirection.Right:
                result = RuleTileDirection.Left;
                break;
            default:
                break;
        }
        return result;
    }

}
