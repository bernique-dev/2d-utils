using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour {

    public void Collide(Collision2D collision) {
        Debug.Log("[COLLIDE]\t" + collision.gameObject.name + " -> " + gameObject.name);
    }

    public void Trigger(Collider2D collider) {
        Debug.Log("[TRIGGER]\t" + collider.gameObject.name + " -> " + gameObject.name);
    }

}
