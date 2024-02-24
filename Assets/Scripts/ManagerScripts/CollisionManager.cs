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
            // If 1. The box is closed, and 2. The player and box are colliding: Open the box and add 5 clowns
            if (box.gameObject.GetComponent<BoxController>().isClosed && Vector2.Distance(player.position, box.position) < player.radius + box.radius)
            {
                box.gameObject.GetComponent<BoxController>().OpenBox();
                player.gameObject.GetComponent<PlayerController>().AddClowns(5);
            }
        }
    }
}
