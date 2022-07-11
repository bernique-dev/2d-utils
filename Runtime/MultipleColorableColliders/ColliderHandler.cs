using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour {

    public ColliderActionData colliderActionData;

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("OnTriggerEnter2D");
        colliderActionData.onTriggerEnterActions.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //Debug.Log("OnTriggerStay2D");
        colliderActionData.onTriggerStayActions.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //Debug.Log("OnTriggerExit2D");
        colliderActionData.onTriggerExitActions.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("OnCollisionEnter2D");
        colliderActionData.onCollisionEnterActions.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        //Debug.Log("OnCollisionStay2D");
        colliderActionData.onCollisionStayActions.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        //Debug.Log("OnCollisionExit2D");
        colliderActionData.onCollisionExitActions.Invoke(collision);
    }

}
