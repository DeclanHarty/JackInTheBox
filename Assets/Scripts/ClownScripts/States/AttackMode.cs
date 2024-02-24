using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMode : ClownState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        nearestEnemy = enemyCheck.CheckForNearestEnemy();
        allyOpposite = allyCheck.FindAllyDirectionSumOpposite();

        // Flips Sprite depending on Velocity
        if(rb.velocity == Vector2.zero){
            return;
        }else{
            transform.localScale = new Vector2(Mathf.Abs(rb.velocity.x)/rb.velocity.x, transform.localScale.y);
        }
    }

    public void FixedUpdate()
    {
        if(nearestEnemy == null){
            rb.velocity = Vector2.zero;
            return;
        }

        Vector3 enemyPosition = nearestEnemy.transform.position;

        if(Vector2.Distance(enemyPosition, transform.position) > 1.5f){

            if(allyOpposite == Vector2.zero){
                Vector2 directionToEnemy = (enemyPosition - transform.position);
                directionToEnemy.Normalize();
                rb.velocity = directionToEnemy * speed;
            }else{
                rb.velocity += allyOpposite * bumpSpeed;
            } 
            Debug.Log(rb.velocity);
        }else if(isAbleToAttack == false){
            rb.velocity = Vector2.zero;
            isAbleToAttack = true;
            Attack();
        }else{
            rb.velocity = Vector2.zero;
        }
        
    }

    void Attack(){
        Debug.Log("Attack");
        if(isAbleToAttack){
            bool enemyIsDead = nearestEnemy.GetComponent<EnemyBehavior>().TakeDamage(attackDamage);
            if(!enemyIsDead){
                Invoke("Attack", attackInterval);
            }else{
                isAbleToAttack = false;
            }
        } 
    }


}
