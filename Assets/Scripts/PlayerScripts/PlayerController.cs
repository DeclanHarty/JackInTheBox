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
        // Adds (or subtracts) a given number to the player's number of clowns
    public void AddClowns(int numToAdd)
    {
        // Add the clowns
        numOfClowns += numToAdd;

        // If the new total goes below one: do death stuff (TODO)
        if (numOfClowns < 1)
        {
            OnDeath();
            return;
        }
        else if (numOfClowns < 5) // 1 - 4
        {
            // Set to single clown sprite
            spriteRenderer.sprite = clownGroupSprites[(int)GroupSizes.Group1];
        }
        else if (numOfClowns < 10) // 5 - 9
        {
            // Set to 5 clowns sprite
            spriteRenderer.sprite = clownGroupSprites[(int)GroupSizes.Group5];
        }
        else if (numOfClowns < 20) // 10 - 19
        {
            // Set to 10 clowns sprite
            spriteRenderer.sprite = clownGroupSprites[(int)GroupSizes.Group10];
        }
        else if (numOfClowns < 50) // 20 - 49
        {
            // Set to 20 clowns sprite
            spriteRenderer.sprite = clownGroupSprites[(int)GroupSizes.Group20];
        }
        else // 50+
        {
            // Set to 50 clowns sprite
            spriteRenderer.sprite = clownGroupSprites[(int)GroupSizes.Group50];
        }

    // TODO: Do player death stuff
    void OnDeath()
    {

    }
}
