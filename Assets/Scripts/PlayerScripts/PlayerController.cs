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


    [SerializeField] private int numOfClowns = 1; // Int to track number of clowns
    [SerializeField] private int maxNumOfClowns = 1000; //Maximum number of clowns
    [SerializeField] private GroupSizes currentSize = GroupSizes.Group1; // Current group sprite to use
    [SerializeField] private List<Sprite> clownGroupSprites; // List of various levels of group size
    private SpriteRenderer spriteRenderer; // Player's SpriteRenderer
    private Animator animator;
    [SerializeField] private GameObject sprite;

    private Vector2 input;

    private bool isAbleToAttack;

    private List<GameObject> activeClowns; 

    [SerializeField] private Bar clownBar;

    void Start(){
        movement = GetComponent<Movement>();
        playerAttack = GetComponent<PlayerAttack>();
        isAbleToAttack = true;
        playerAttack = GetComponent<QuickfireAttack>();
        activeClowns = new List<GameObject>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        movement.Move();
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
            Invoke("ResetAttack", playerAttack.GetAttackDelay());
        }

        if(Input.GetKeyDown("space")){
            CallClowns();
        }

        sprite.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * .1f);
        // Flips sprite depending on velocity
        if (velocity.x > 0.1)
        {
            spriteRenderer.flipX = false;
        }
        else if (velocity.x < -0.1)
        {
            spriteRenderer.flipX = true;
        }

        if(PlayerIsMoving()){
            animator.SetBool("isMoving", true);
        }else {
            animator.SetBool("isMoving", false);
        }

        if(clownBar != null) clownBar.percent = numOfClowns/maxNumOfClowns;
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
    }

    void OnCollisionEnter2D(Collision2D col){
        Debug.Log(col.gameObject.name);
    }

    public bool PlayerIsMoving(){
        if(movement.GetVelocity() != Vector2.zero){
            return true;
        }else{
            return false;
        }
    }

    public SpriteRenderer GetSpriteRenderer(){
        return spriteRenderer;
    }

    // TODO: Do player death stuff
    void OnDeath()
    {

    }
}
