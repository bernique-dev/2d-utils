using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bernique2DUtils {
    public class MultipleColorableCollider : MonoBehaviour {


        public bool displayGizmos;
        public List<BoxColliderData> colliderDataList;
        public BoxColliderData colliderData;

        private void Start() {
            //GameObject collidersParent = Instantiate(new GameObject("Multiple Colliders"), transform.position, Quaternion.identity);
            GameObject collidersParent = new GameObject("Multiple Colliders");
            collidersParent.transform.parent = transform;
            collidersParent.transform.localPosition = transform.position;
            foreach (BoxColliderData boxColliderData in colliderDataList) {
                GameObject instance = new GameObject("Collider");
                instance.transform.localPosition = transform.position;
                instance.transform.parent = collidersParent.transform;

                ColliderHandler colliderHandler = instance.AddComponent<ColliderHandler>();
                colliderHandler.colliderActionData = boxColliderData.colliderActionData;

                BoxCollider2D boxCollider2D = instance.AddComponent<BoxCollider2D>();
                boxCollider2D.size = boxColliderData.size;
                boxCollider2D.offset = boxColliderData.offset;
                boxCollider2D.isTrigger = boxColliderData.isTrigger;
            }
        }


        private void OnDrawGizmosSelected() {
            foreach (BoxColliderData boxColliderData in colliderDataList) {
                Gizmos.color = boxColliderData.normalColor;
                Gizmos.DrawWireCube(transform.position + boxColliderData.offset.GetVector3(), boxColliderData.size);
            }
        }

        private void OnDrawGizmos() {
            if (displayGizmos) OnDrawGizmosSelected();
        }
    }
}
