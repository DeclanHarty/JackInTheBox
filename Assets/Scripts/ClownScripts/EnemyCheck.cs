using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private string layerMask;


    // Update is called once per frame
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public GameObject CheckForNearestEnemy(){
        RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, checkRadius, Vector3.forward, 0, LayerMask.GetMask(layerMask));

        GameObject currentClosestGameObject = null;
        if(raycastHits.Length > 0){
            float smallestDistance = -1f;
            foreach (RaycastHit2D hit in raycastHits){
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if(distance < smallestDistance || smallestDistance == -1f){
                    smallestDistance = distance;
                    currentClosestGameObject = hit.transform.gameObject;
                }
            }
        }
        //return hit.transform.gameObject; 
        return currentClosestGameObject;
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("Quit");
            Application.Quit();
        }
    }

}
