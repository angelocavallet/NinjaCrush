using System;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip throwAudioClip;
    [SerializeField] private AudioClip hitTargetAudioClip;
    [SerializeField] private AudioClip hitSomethingAudioClip;

    private GameObject effectGameObject;
    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private AudioSource audioSource;
    private Boolean isHited;
    private Action<Collider2D, ThrowableBehaviour> onHitedSomethig;
    private const string TAG_ENEMY = "Enemy";

    public void Awake()
    {
        effectGameObject = transform.GetChild(0).gameObject;
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        isHited = false;
        audioSource.clip = throwAudioClip;
        audioSource.Play();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isHited)
        {
            audioSource.clip = collider.transform.CompareTag(TAG_ENEMY) ? hitTargetAudioClip : hitSomethingAudioClip;
            audioSource.Play();

            SetIsHited(true);

            transform.SetParent(collider.gameObject.transform);
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
        effectGameObject.SetActive(false);
        rb2D.velocity = Vector3.zero;
        rb2D.isKinematic = true;
        col2D.enabled = false;
    }
}
