using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickfireAttack : PlayerAttack
{
    public override GameObject Attack(Vector3 direction){
        Quaternion angle = Quaternion.identity;
        angle.eulerAngles = new Vector3(0,0,Vector3.SignedAngle(Vector3.right, direction, Vector3.forward));
        Debug.Log(angle.eulerAngles);
        return Instantiate(clownBullet, transform.position + spawnDistanceFromCenter * direction, angle);
    }
}
