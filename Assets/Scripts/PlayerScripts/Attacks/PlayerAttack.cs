using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float spawnDistanceFromCenter;

    [SerializeField] protected GameObject clownBullet;

    [SerializeField] private int minNumberOfClowns;

    public abstract GameObject Attack(Vector3 direction);

    public int GetMinNumberOfClowns(){
        return minNumberOfClowns;
    }

}
