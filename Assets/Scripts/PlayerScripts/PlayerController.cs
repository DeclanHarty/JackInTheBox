using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum to make accessing ClownGroupSprites indices easier
enum GroupSizes
{
    Group1 = 0,
    Group5 = 1,
    Group10 = 2,
    Group20 = 3,
    Group50 = 4
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private List<Sprite>ClownGroupSprites = new List<Sprite>(); // List of various sizes of clown groups
    [SerializeField] private int numOfClowns = 1; // Int to track number of clowns
    [SerializeField] private GroupSizes currentSize = GroupSizes.Group1; // Current group sprite to use

    private Vector2 input;

    void FixedUpdate(){
        movement.Move();
    }

    void Update(){
        movement.GetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        Vector2 velocity = movement.GetVelocity();
        // Flips sprite depending on velocity
        if(velocity == Vector2.zero){
            return;
        }else{
            transform.localScale = new Vector2(Mathf.Abs(velocity.x)/velocity.x, transform.localScale.y);
        }
    }
}
