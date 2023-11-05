using UnityEngine;

public class BloodPointController : MonoBehaviour
{
    [SerializeField] private float secondsToDestroy = 30f;
    private float timeToDestroy = 0f;

    public void Awake()
    {
        timeToDestroy = Time.time + secondsToDestroy;
    }

    public void Update()
    {
        if (Time.time > timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
