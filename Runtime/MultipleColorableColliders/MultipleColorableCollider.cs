using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bernique2DUtils {
    public class MultipleColorableCollider : MonoBehaviour {


        public bool displayGizmos;

        public BoxColliderList boxColliders;
        public CircleColliderList circleColliders;

        private void Start() {
            //GameObject collidersParent = Instantiate(new GameObject("Multiple Colliders"), transform.position, Quaternion.identity);
            GameObject collidersParent = new GameObject("Multiple Colliders");
            collidersParent.transform.parent = transform;
            collidersParent.transform.position = transform.position;
            foreach (BoxColliderData boxColliderData in boxColliders.list) {
                GameObject instance = new GameObject(boxColliderData.name);
                instance.transform.position = transform.position;
                instance.transform.parent = collidersParent.transform;

                ColliderHandler colliderHandler = instance.AddComponent<ColliderHandler>();
                colliderHandler.colliderActionData = boxColliderData.colliderActionData;

                BoxCollider2D boxCollider2D = instance.AddComponent<BoxCollider2D>();
                boxCollider2D.size = boxColliderData.size;
                boxCollider2D.offset = boxColliderData.offset;
                boxCollider2D.isTrigger = boxColliderData.isTrigger;
            }

            foreach (CircleColliderData circleColliderData in circleColliders.list) {
                GameObject instance = new GameObject(circleColliderData.name);
                instance.transform.position = transform.position;
                instance.transform.parent = collidersParent.transform;

                ColliderHandler colliderHandler = instance.AddComponent<ColliderHandler>();
                colliderHandler.colliderActionData = circleColliderData.colliderActionData;

                CircleCollider2D circleCollider2D= instance.AddComponent<CircleCollider2D>();
                circleCollider2D.radius = circleColliderData.radius;
                circleCollider2D.offset = circleColliderData.offset;
                circleCollider2D.isTrigger = circleColliderData.isTrigger;
            }
        }


        private void OnDrawGizmosSelected() {

#if UNITY_EDITOR
            foreach (BoxColliderData boxColliderData in boxColliders.list) {
                Handles.color = boxColliderData.normalColor;
                Handles.DrawSolidRectangleWithOutline(RectUtils.GetRect(transform.position + boxColliderData.offset.GetVector3(), boxColliderData.size), new Color32(0,0,0,0), boxColliderData.normalColor);
            }

            foreach (CircleColliderData circleColliderData in circleColliders.list) {
                Handles.color = circleColliderData.normalColor;
                Handles.DrawWireDisc(transform.position + circleColliderData.offset.GetVector3(), Vector3.forward, circleColliderData.radius);
            }
#endif
        }

        private void OnDrawGizmos() {
            if (displayGizmos) OnDrawGizmosSelected();
        }
    }
}
