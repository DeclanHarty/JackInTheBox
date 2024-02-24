using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float bumpSpeed;

    [SerializeField] private float minDistanceToEnemy;

    [SerializeField] private bool isAbleToAttack;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackInterval;
    private EnemyCheck enemyCheck;

    private AllyCheck allyCheck;

    private GameObject nearestEnemy;
    private Vector2 allyOpposite;

    private ClownState state;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemyCheck = GetComponent<EnemyCheck>();
        allyCheck = GetComponent<AllyCheck>();
    }
    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    void FixedUpdate(){
        state.FixedUpdate();
    }

    

    
}
