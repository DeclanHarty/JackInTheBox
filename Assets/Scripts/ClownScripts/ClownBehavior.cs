using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float bumpSpeed;

    [SerializeField] private float minDistanceToEnemy;
    private EnemyCheck enemyCheck;

    private AllyCheck allyCheck;

    private GameObject nearestEnemy;
    private Vector2 allyOpposite;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyCheck = GetComponent<EnemyCheck>();
        allyCheck = GetComponent<AllyCheck>();
    }
    // Update is called once per frame
    void Update()
    {
        nearestEnemy = enemyCheck.CheckForNearestEnemy();
        allyOpposite = allyCheck.FindAllyDirectionSumOpposite();
    }

    void FixedUpdate(){
        if(nearestEnemy == null){
            rb.velocity = Vector2.zero;
            return;
        }

        Vector3 enemyPosition = nearestEnemy.transform.position;

        if(Vector2.Distance(enemyPosition, transform.position) > 1.5f && allyOpposite == Vector2.zero){

            if(allyOpposite == Vector2.zero){
                Vector2 directionToEnemy = (enemyPosition - transform.position);
                directionToEnemy.Normalize();
                rb.velocity = directionToEnemy * speed;
            }else{
                rb.velocity += allyOpposite * bumpSpeed;
            } 
            Debug.Log(rb.velocity);
        }else{
            rb.velocity = Vector2.zero;
        }
        
    }
}
