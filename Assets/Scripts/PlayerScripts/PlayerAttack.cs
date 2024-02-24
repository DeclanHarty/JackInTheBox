using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float spawnDistanceFromCenter;

    [SerializeField] private GameObject clownBullet;
    public void Attack(Vector3 direction){
        Quaternion angle = Quaternion.identity;
        angle.eulerAngles = new Vector3(0,0,Vector3.SignedAngle(Vector3.right, direction, Vector3.forward));
        Debug.Log(angle.eulerAngles);
        Instantiate(clownBullet, transform.position + spawnDistanceFromCenter * direction, angle);
    }


}
