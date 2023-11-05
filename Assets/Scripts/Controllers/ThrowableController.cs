using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] private Throwable throwable;

    public void Awake()
    {
        throwable = throwable.Clone();
        throwable.transform = transform;
        throwable.rigidbody2D = GetComponent<Rigidbody2D>();
        throwable.collider2D = GetComponent<Collider2D>();
        throwable.audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        throwable.UpdateOnTriggerEnter2D(collider);
    }

    void FixedUpdate()
    {
        throwable.UpdateMovement();
    }

    public Throwable GetThrowable()
    {
        return throwable;
    }
}
