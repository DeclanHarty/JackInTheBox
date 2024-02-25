using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBulletBehavior : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private int bulletDamage;
    [SerializeField] private float airTime;

    [SerializeField] private GameObject clown;

    private PlayerController player;
    

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        Invoke("MakeClown", airTime);
    }
    void FixedUpdate(){
        rb.velocity = transform.right * speed;
        Debug.Log(transform.right);
    }

    void MakeClown(){
        GameObject activeClown = Instantiate(clown, transform.position, Quaternion.identity);
        player.AddClownToActive(activeClown);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag != "Player"){
            MakeClown();
        }
        if(col.gameObject.tag == "Enemy"){
            col.gameObject.GetComponent<EnemyBehavior>().TakeDamage(bulletDamage);
        }
    }

    public void SetPlayer(PlayerController player){
        this.player = player;
    }

}
