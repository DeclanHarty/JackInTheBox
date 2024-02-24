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
    private Movement movement;
    private PlayerAttack playerAttack;


    [SerializeField] private List<Sprite>ClownGroupSprites = new List<Sprite>(); // List of various sizes of clown groups
    [SerializeField] private int numOfClowns = 1; // Int to track number of clowns
    [SerializeField] private GroupSizes currentSize = GroupSizes.Group1; // Current group sprite to use

    private Vector2 input;

    private bool isAbleToAttack;
    [SerializeField] private float attackDelay;

    void Start(){
        movement = GetComponent<Movement>();
        playerAttack = GetComponent<PlayerAttack>();
        isAbleToAttack = true;
    }

    void FixedUpdate(){
        movement.Move();
    }

    void Update(){
        movement.GetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        Vector2 velocity = movement.GetVelocity();

        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = (mousePositionInWorld - new Vector2(transform.position.x, transform.position.y)).normalized;


        if(Input.GetMouseButton(0) && isAbleToAttack && numOfClowns > 1){
            numOfClowns--;
            playerAttack.Attack(attackDirection);
            isAbleToAttack = false;
            Invoke("ResetAttack", attackDelay);
        }

        // Flips sprite depending on velocity
        if(velocity.x == 0){
            return;
        }else{
            transform.localScale = new Vector3(Mathf.Abs(velocity.x)/(velocity.x != 0 ? velocity.x : 1), transform.localScale.y);
        } 

    }

    private void ResetAttack(){
        isAbleToAttack = true;
    }
}
