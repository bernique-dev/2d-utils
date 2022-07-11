using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Bernique2DUtils {
    [Serializable]
    public class BoxColliderData : ColliderData {

        public Vector2 offset;
        public Vector2 size;

    }

}