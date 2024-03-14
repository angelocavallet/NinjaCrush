using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "new Throwable", menuName = "ScriptableObjects/Throwable")]
public class Throwable : ScriptableObject
{
    [Header("General Info")]
    [SerializeField] public string namePrefab;
    [SerializeField] public GameObject throwablePrefab;
    [SerializeField] public float mass;
    [SerializeField] public float damage;
    [SerializeField] public float lifeTimeSeconds;

    [Header("Effect Prefabs")]
    [SerializeField] private GameObject throwEffectPrefab;
    [SerializeField] private GameObject hitTargetEffectPrefab;
    [SerializeField] private GameObject hitOtherEffectPrefab;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip throwAudioClip;
    [SerializeField] private AudioClip hitTargetAudioClip;
    [SerializeField] private AudioClip hitOtherAudioClip;

    public Rigidbody2D rigidbody2D { private get; set; }
    public Collider2D collider2D { private get; set; }
    public AudioSource audioSource {
        private get => _audioSource;
        set
        {
            _audioSource = value;
            SoundManager.instance.RegisterSfxSource(value);
        } 
    }
    public Transform transform { private get; set; }
    public Action<Throwable> onThrowed { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedTarget { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedSomething { private get; set; }
    public string targetTag { private get; set; }
    public string selfThrowerTag { private get; set; }
    public string selfTag { private get; set; }
    public bool isGhost { private get; set; }

    private AudioSource _audioSource;
    private Boolean isHited = false;
    private int instanceNumber = 1;
    private float timeToDie = 0f;

    public Throwable Clone()
    {
        return Instantiate(this);
    }

    public Throwable InstantiateCloneAtTransform(Transform transform)
    {
        GameObject newThrowableInstance = Instantiate(throwablePrefab, transform.position, transform.rotation);
        instanceNumber++;
        newThrowableInstance.name = $"{namePrefab} ({instanceNumber})";
        return newThrowableInstance.GetComponent<ThrowableController>().GetThrowable();
    }

    public void Throw(Vector2 velocity)
    {
        rigidbody2D.mass = mass;
        rigidbody2D.velocity = velocity;

        if (isGhost) return;

        PlayThrowAudioClip();
        throwEffectPrefab = Instantiate(throwEffectPrefab, transform.position, transform.rotation, transform);
        throwEffectPrefab.SetActive(true);
        timeToDie = Time.time + lifeTimeSeconds;
        onThrowed(this);
    }

    public void PlayThrowAudioClip()
    {   
        audioSource.clip = throwAudioClip;
        audioSource.Play();
    }

    public void UpdateMovement()
    {
        if (!isHited)
        {
            float angle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            if (Time.time > timeToDie)
            {
                SoundManager.instance.UnregisterSfxSource(_audioSource);
                Destroy(transform.gameObject);
            }
        }
    }

    public void UpdateOnTriggerEnter2D(Collider2D otherCollider2D)
    {
        if (isGhost) return;
        if (isHited) return;
        if (otherCollider2D.CompareTag(selfTag)) return;
        if (otherCollider2D.CompareTag(selfThrowerTag)) return;

        isHited = true;

        //@todo: throwEffectPrefab as EffectScriptableObject
        // effectSO.Stop() ?
        //throwEffectPrefab.SetActive(false);
        throwEffectPrefab.GetComponent<ParticleSystem>().Stop();
        throwEffectPrefab.GetComponentInChildren<Transform>().gameObject.SetActive(false);

        Vector2 dirHit = rigidbody2D.velocity.normalized;
        float magHit = Mathf.Abs(rigidbody2D.velocity.magnitude * this.mass);

        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.isKinematic = true;
        collider2D.enabled = false;
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