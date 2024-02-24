using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private Vector2 input;

    void FixedUpdate(){
        movement.Move();
    }

    void Update(){
        movement.GetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }
}
