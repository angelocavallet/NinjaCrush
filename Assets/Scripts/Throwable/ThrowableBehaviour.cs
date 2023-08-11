using System;
using UnityEngine;

public class ThrowableBehaviour : MonoBehaviour
{
    private GameObject effectGameObject;
    private Rigidbody2D rb2D;
    private Boolean isHited;

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
            Debug.Log(collider.gameObject.name);
            isHited = true;
            effectGameObject.SetActive(false);
            rb2D.velocity = Vector3.zero;
            rb2D.isKinematic = true;
            transform.SetParent(collider.gameObject.transform);
        }
    }

}
