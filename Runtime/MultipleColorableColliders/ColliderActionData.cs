using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ColliderActionData {

    public UnityEvent<Collider2D> onTriggerEnterActions;
    public UnityEvent<Collider2D> onTriggerStayActions;
    public UnityEvent<Collider2D> onTriggerExitActions;
    public UnityEvent<Collision2D> onCollisionEnterActions;
    public UnityEvent<Collision2D> onCollisionStayActions;
    public UnityEvent<Collision2D> onCollisionExitActions;

}