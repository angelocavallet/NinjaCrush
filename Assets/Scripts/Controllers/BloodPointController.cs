using UnityEngine;

public class BloodPointController : MonoBehaviour
{
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float secondsToDestroy;

    private SpriteRenderer spriteRenderer;
    private float timeToFade = 0f;
    private float timeToDestroy = 0f;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeToFade = Time.time + secondsToDestroy;
        timeToDestroy = Time.time + timeToFade + fadeOutTime;
    }

    public void Update()
    {
        if (Time.time > timeToFade)
        {
            Color color = spriteRenderer.color;
            color.a -= Time.deltaTime / fadeOutTime;
            spriteRenderer.color = color;
        }

        if (Time.time > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
