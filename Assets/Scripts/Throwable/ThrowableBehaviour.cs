using System;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip throwAudioClip;
    [SerializeField] private AudioClip hitTargetAudioClip;
    [SerializeField] private AudioClip hitSomethingAudioClip;

    [SerializeField] private GameObject effectGameObject;
    [SerializeField] private GameObject hitEffectGameObject;

    private Rigidbody2D rb2D;
    private Collider2D col2D;
    private AudioSource audioSource;
    private Boolean isHited;
    private Action<Collider2D, ThrowableBehaviour> onHitedSomethig;
    private const string TAG_PLAYER = "Player";
    private const string TAG_ENEMY = "Enemy";
    private const string TAG_BULLET = "Bullet";

    public void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        isHited = false;
        audioSource.clip = throwAudioClip;
        audioSource.Play();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isHited && !collider.CompareTag(TAG_BULLET) && !collider.CompareTag(TAG_PLAYER))
        {
            //@todo change hit audios to hurted GameObjects (Enemy, Ground) and check for component to play audio from them
            audioSource.clip = collider.transform.CompareTag(TAG_ENEMY) ? hitTargetAudioClip : hitSomethingAudioClip;
            audioSource.Play();

            SetIsHited(true);

            transform.SetParent(collider.transform);
            onHitedSomethig(collider, this);
        }
    }

    void FixedUpdate()
    {
        if (!isHited)
        {
            float angle = Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    public void Bleed()
    {
        GameObject blood = Instantiate(hitEffectGameObject, transform.position, transform.rotation);
        blood.transform.SetParent(transform);
    }
}
