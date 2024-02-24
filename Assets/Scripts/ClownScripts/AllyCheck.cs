using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCheck : MonoBehaviour
{
    [SerializeField] private float checkRadius;
    [SerializeField] private string layerMask;


    // Update is called once per frame
    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }


    public Vector2 FindAllyDirectionSumOpposite(){
        RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, checkRadius, Vector3.forward, 0, LayerMask.GetMask(layerMask));

        Vector3 allyDifferenceSum = Vector3.zero;
        foreach(RaycastHit2D hit in raycastHits){

            allyDifferenceSum += (hit.transform.position - transform.position);
        }

        Vector2 allyOpposite = -1f * allyDifferenceSum.normalized;

        return allyOpposite;
    }
}
