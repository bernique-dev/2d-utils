using System;
using System.Collections.Generic;
using UnityEngine;


namespace Bernique2DUtils {
    [Serializable]
    public class ColliderData {


        public bool isTrigger;

        public Color normalColor;
        public Color usedColor;
        public LayerMask layer;

        public ColliderActionData colliderActionData;

    }
}
