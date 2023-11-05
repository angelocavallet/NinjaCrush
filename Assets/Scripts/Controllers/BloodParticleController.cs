using System.Collections.Generic;
using UnityEngine;

public class BloodParticleController : MonoBehaviour
{
    [SerializeField] private GameObject bloodPoint;
    private int minOffsetIntersection = 10;
    private int maxOffsetIntersection = 40;
    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> collisionEvents;
    private System.Random rand = new System.Random();

    public void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Ground")) return;

        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector2 position = collisionEvents[i].intersection - (collisionEvents[i].normal * (rand.Next(minOffsetIntersection, maxOffsetIntersection) * 0.01f));
            Instantiate(bloodPoint, position, Quaternion.identity, other.transform);
            i++;
        }
    }
}
