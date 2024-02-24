using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input;

    [SerializeField] private float speed;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(){
        rb.velocity = input * speed;
    }

    public void GetInput(Vector2 input){
        this.input = input;
        this.input.Normalize();
    }

    public Vector2 GetVelocity(){
        return rb.velocity;
    }
}
