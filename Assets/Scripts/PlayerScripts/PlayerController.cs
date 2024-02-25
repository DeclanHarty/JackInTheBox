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

    private List<GameObject> activeClowns; 

    void Start(){
        movement = GetComponent<Movement>();
        playerAttack = GetComponent<PlayerAttack>();
        isAbleToAttack = true;
        playerAttack = GetComponent<QuickfireAttack>();
        activeClowns = new List<GameObject>();
    }

    void FixedUpdate(){
        movement.Move();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * .1f);
    }

    void Update(){
        movement.GetInput(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        Vector2 velocity = movement.GetVelocity();

        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = (mousePositionInWorld - new Vector2(transform.position.x, transform.position.y)).normalized;

        if(Input.GetKeyDown("1")){
            playerAttack = GetComponent<QuickfireAttack>();
        }


        if(Input.GetMouseButton(0) && isAbleToAttack && numOfClowns > playerAttack.GetMinNumberOfClowns()){
            numOfClowns-= playerAttack.GetMinNumberOfClowns();
            GameObject bullet = playerAttack.Attack(attackDirection);
            bullet.GetComponent<ClownBulletBehavior>().SetPlayer(this);
            isAbleToAttack = false;
            Invoke("ResetAttack", attackDelay);
        }

        if(Input.GetKeyDown("space")){
            CallClowns();
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

    private void CallClowns(){
        foreach(GameObject clown in activeClowns){
            clown.GetComponent<ClownBehavior>().Return();
        }
        numOfClowns += activeClowns.Count;
        activeClowns = new List<GameObject>();
    }

    public void AddClownToActive(GameObject clown){
        activeClowns.Add(clown);
    }
}
