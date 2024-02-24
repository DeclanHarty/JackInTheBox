using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;
    private int health;

    private Rigidbody2D rb;
    private EnemyCheck enemyCheck;

    private GameObject nearestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemyCheck = GetComponent<EnemyCheck>();
    }
    // Update is called once per frame
    void Update()
    {
        nearestEnemy = enemyCheck.CheckForNearestEnemy();
    }

    void FixedUpdate(){
        if(nearestEnemy == null){
            rb.velocity = Vector2.zero;
            return;
        }

        Vector3 enemyPosition = nearestEnemy.transform.position;

        if(Vector2.Distance(enemyPosition, transform.position) > 1.5f){
            Vector2 directionToEnemy = (enemyPosition - transform.position);
            directionToEnemy.Normalize();
            rb.velocity = directionToEnemy * speed;
        }else{
            rb.velocity = Vector2.zero;
        }
        
        
    }

    public bool TakeDamage(int damage){
        health -= damage;
        Debug.Log("hit");
        if(health <= 0){
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
