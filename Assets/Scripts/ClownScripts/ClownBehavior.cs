using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float bumpSpeed;

    [SerializeField] private float minDistanceToEnemy;

    private bool isAbleToAttack;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackInterval;
    private EnemyCheck enemyCheck;

    private AllyCheck allyCheck;

    private GameObject nearestEnemy;
    private Vector2 allyOpposite;

    private Animator animator;

    [SerializeField] private PlayerController player;

    private ClownState state;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyCheck = GetComponent<EnemyCheck>();
        allyCheck = GetComponent<AllyCheck>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        nearestEnemy = enemyCheck.CheckForNearestEnemy();
        allyOpposite = allyCheck.FindAllyDirectionSumOpposite();

        // Flips Sprite depending on Velocity
        /*if(rb.velocity == Vector2.zero){
            return;
        }else{
            transform.localScale = new Vector2(transform.localScale.x * Mathf.Abs(rb.velocity.x)/rb.velocity.x, transform.localScale.y);
        }*/

        if (rb.velocity.x > 0.1)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else if (rb.velocity.x < -0.1)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if(Mathf.Abs(rb.velocity.x) > 0.1f){
            animator.SetBool("isMoving", true);
        }else{
            animator.SetBool("isMoving", false);
        }
    }

    void FixedUpdate(){
        if(nearestEnemy == null){
            if(allyOpposite != Vector2.zero){
                rb.velocity += allyOpposite.normalized * bumpSpeed * Time.deltaTime;
            }else{
                rb.velocity = Vector2.zero;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y * .1f);
            return;
        }

        Vector3 enemyPosition = nearestEnemy.transform.position;

        if(Vector2.Distance(enemyPosition, transform.position) > 1.5f){
            Vector2 directionToEnemy = (enemyPosition - transform.position);
            directionToEnemy.Normalize();

            if(allyOpposite == Vector2.zero){    
                rb.velocity = directionToEnemy * speed;
            }else{
                rb.velocity += allyOpposite.normalized * bumpSpeed * Time.deltaTime;
            } 
        }else if(isAbleToAttack == false){
            rb.velocity = Vector2.zero;
            isAbleToAttack = true;
            Attack();
        }else{
            if(allyOpposite != Vector2.zero){
                rb.velocity += allyOpposite.normalized * bumpSpeed * Time.deltaTime;
            }else{
                rb.velocity = Vector2.zero;
            }
        }
    }

    void Attack(){
        if(isAbleToAttack && nearestEnemy != null){
            bool enemyIsDead = nearestEnemy.GetComponent<EnemyBehavior>().TakeDamage(attackDamage);
            if(!enemyIsDead){
                Invoke("Attack", attackInterval);
            }else{
                isAbleToAttack = false;
            }
        } 
    }

    public void Return(){
        Destroy(gameObject);
    }

    public Rigidbody2D GetRigidbody(){
        return rb;
    }

    public Transform GetTransform(){
        return transform;
    }

    public EnemyCheck GetEnemyCheck(){
        return enemyCheck;
    }

    public AllyCheck GetAllyCheck(){
        return allyCheck;
    }

    public int GetAttackDamage(){
        return attackDamage;
    }

    public float GetAttackInterval(){
        return attackInterval;
    }

    public float GetSpeed(){
        return speed;
    }

    public float GetBumpSpeed(){
        return bumpSpeed;
    }


    

    
}
