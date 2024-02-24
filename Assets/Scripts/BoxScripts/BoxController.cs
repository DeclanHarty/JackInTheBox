using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] public bool isClosed = true; // Whether or not the box has been opened
    [SerializeField] private SpriteRenderer closedSprite; // Opened/Closed SpriteRenderers; will be activated/deactivated depending on state
    [SerializeField] private SpriteRenderer openedSprite;

    [SerializeField] private int clownValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Will be called whenever the box is opened
    public void OpenBox()
    {
        isClosed = false;
        // Disable closedSprite and enable openedSprite
        closedSprite.enabled = false;
        openedSprite.enabled = true;
    }

    // Will be called whenever the box is opened
    public void CloseBox()
    {
        isClosed = true;
        // Disable openedSprite and enable closedSprite
        openedSprite.enabled = false;
        closedSprite.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Col");
        if(col.gameObject.tag == "Player"){
            OpenBox();
            col.gameObject.GetComponent<PlayerController>().AddClowns(clownValue);
        }
    }
}
