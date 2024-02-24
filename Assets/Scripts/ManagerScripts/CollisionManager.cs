using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Lists of CollisionInfo classes
    [SerializeField] private CollisionInfo player;
    [SerializeField] private List<CollisionInfo> boxes = new List<CollisionInfo>();
    [SerializeField] private List<CollisionInfo> enemies = new List<CollisionInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (CollisionInfo box in boxes)
        {
            // If the player collides with a box, call its OpenBox() method
            if (Vector2.Distance(player.position, box.position) < player.radius + box.radius)
            {
                box.gameObject.GetComponent<BoxController>().OpenBox();
            }
        }
    }
}
