using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Bernique2DUtils {
    [Serializable]
    public class CircleColliderData : ColliderData {

        public Vector2 offset;
        public float radius = 1;

    }

}