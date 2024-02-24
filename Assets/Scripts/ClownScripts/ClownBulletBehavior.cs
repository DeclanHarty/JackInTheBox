using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBulletBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float airTime;

    [SerializeField] private GameObject clown;
    

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Invoke("MakeClown", airTime);
    }
    void FixedUpdate(){
        rb.velocity = transform.right * speed;
        Debug.Log(transform.right);
    }

    void MakeClown(){
        Instantiate(clown, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
