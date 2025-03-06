using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(AudioSource))]
public class Throwable : MonoBehaviour
{
    [SerializeField] private ThrowableScriptableObject throwableData;

    public float damage { get; private set; }
    public Action<Throwable> onThrowed { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedTarget { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedSomething { private get; set; }
    public string targetTag { private get; set; }
    public string selfThrowerTag { private get; set; }
    public bool isGhost { private get; set; }

    private float mass;
    private float lifeTimeSeconds;

    //@todo: Need Effect base class
    private GameObject throwEffectPrefab;
    private GameObject hitTargetEffectPrefab;
    private GameObject hitOtherEffectPrefab;

    //@todo: Need Effect base class
    private AudioClip throwAudioClip;
    private AudioClip hitTargetAudioClip;
    private AudioClip hitOtherAudioClip;

    private Rigidbody2D rb2D;
    private Collider2D coll2D;
    private AudioSource audioSource;
    private Boolean isHited = false;
    private float timeToDie = 0f;

    public void Awake()
    {
        mass = throwableData.mass;
        damage = throwableData.damage;
        lifeTimeSeconds = throwableData.lifeTimeSeconds;

        throwEffectPrefab = throwableData.throwEffectPrefab;
        throwAudioClip = throwableData.throwAudioClip;

        hitTargetEffectPrefab = throwableData.hitTargetEffectPrefab;
        hitTargetAudioClip = throwableData.hitTargetAudioClip;

        hitOtherEffectPrefab = throwableData.hitOtherEffectPrefab;
        hitOtherAudioClip = throwableData.hitOtherAudioClip;

        rb2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Throw(Vector2 velocity)
    {
        rb2D.mass = mass;
        rb2D.linearVelocity = velocity;

        if (isGhost) return;

        throwEffectPrefab = Instantiate(throwEffectPrefab, transform.position, transform.rotation, transform);
        throwEffectPrefab.SetActive(true);
        audioSource.clip = throwAudioClip;
        audioSource.Play();
        timeToDie = Time.time + lifeTimeSeconds;
        onThrowed(this);
    }

    public void UpdateMovement()
    {
        if (!isHited)
        {
            float angle = Mathf.Atan2(rb2D.linearVelocity.y, rb2D.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (Time.time > timeToDie)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    public void UpdateOnTriggerEnter2D(Collider2D otherCollider2D)
    {
        if (isGhost) return;
        if (isHited) return;
        if (otherCollider2D.CompareTag(transform.tag)) return;
        if (otherCollider2D.CompareTag(selfThrowerTag)) return;

        isHited = true;

        //@todo: Need Effect base class
        //@todo: throwEffectPrefab as EffectScriptableObject
        // effectSO.Stop() ?
        //throwEffectPrefab.SetActive(false);
        throwEffectPrefab.GetComponent<ParticleSystem>().Stop();
        throwEffectPrefab.GetComponentInChildren<Transform>().gameObject.SetActive(false);

        Vector2 dirHit = rb2D.linearVelocity.normalized;
        float magHit = Mathf.Abs(rb2D.linearVelocity.magnitude * this.mass);

        rb2D.linearVelocity = Vector3.zero;
        rb2D.isKinematic = true;
        coll2D.enabled = false;
        transform.SetParent(otherCollider2D.transform);

        if (otherCollider2D.transform.CompareTag(targetTag))
        {
            audioSource.clip = hitTargetAudioClip;
            audioSource.Play();
            Instantiate(hitTargetEffectPrefab, transform.position, Quaternion.identity, transform);
            onHitedTarget(otherCollider2D, this, dirHit, magHit);
            return;
        }

        audioSource.clip = hitOtherAudioClip;
        audioSource.Play();
        Instantiate(hitOtherEffectPrefab, transform.position, transform.rotation, transform);
        onHitedSomething(otherCollider2D, this, dirHit, magHit);
    }
}