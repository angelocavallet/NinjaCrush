using UnityEngine;

public class KunaiThrowable : Throwable
{
    public void Awake()
    {   
        base.Awake();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        base.UpdateOnTriggerEnter2D(collider);
    }

    void FixedUpdate()
    {
        base.UpdateMovement();
    }
}
