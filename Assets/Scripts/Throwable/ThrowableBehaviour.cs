using System;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour
{
    private GameObject effectGameObject;
    private Rigidbody2D rb2D;
    private Boolean isHited;
    private Action<Collider2D, ThrowableBehaviour> onHitedSomethig;

    public void Awake()
    {
        effectGameObject = transform.GetChild(0).gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        isHited = false;
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isHited)
        {
            onHitedSomethig(collider, this);
        }
    }

    public GameObject GetEffectGameObject()
    {
        return effectGameObject;
    }

    public Rigidbody2D GetRigidBody2D()
    {
        return rb2D;
    }

    public void SetOnHitedSomething(Action<Collider2D, ThrowableBehaviour> onHitedSomething)
    { 
        this.onHitedSomethig = onHitedSomething;
    }

    public void SetIsHited(Boolean isHited)
    {
        this.isHited = isHited;
    }
}
