using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public int clowns;
    public int maxClowns;

    bool inFightingState;

    public GameObject[] points;
    List<GameObject> pointsBeyond = new List<GameObject>();

    List<GameObject> leftPoints = new List<GameObject>();
    List<GameObject> rightPoints = new List<GameObject>();
    List<GameObject> upPoints = new List<GameObject>();
    List<GameObject> downPoints = new List<GameObject>();

    GameObject currentPoint;

    #region Screen wrapping variables
    protected Vector2 halfCameraSize = Vector2.zero;
    #endregion

    Vector2 direction = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        float z = Mathf.Infinity;
        foreach(GameObject point in points)
        {
            float distance = Vector3.Distance(point.transform.position, Camera.main.transform.position);
            if(distance < z)
            {
                z = distance;
                currentPoint = point;
            }
        }

        //  Calcuale the half size of the Camera
        halfCameraSize.y = Camera.main.orthographicSize;
        halfCameraSize.x = halfCameraSize.y * Camera.main.aspect;

        FindPointsInDifferentDirectionsSetup();
    }
    bool hasMoved;
    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(currentPoint.transform.position.x,currentPoint.transform.position.y,Camera.main.transform.position.z), Time.deltaTime * 5);
        if(OutOfBounds())
        {
            if(!hasMoved)
            {
                direction = SearchForDirection();
                FindAllPointsInCurrentDirection();
                hasMoved = true;
            }
            
        }
        else
        {
            hasMoved = false;
            direction = Vector2.zero;
        }

        FindPointsInDifferentDirectionsSetup();
        //FindPointsInDifferentDirectionsUpdate();

        //cameraNotMoving = Camera.main.transform.position == new Vector3(currentPoint.transform.position.x,currentPoint.transform.position.y,Camera.main.transform.position.z);

        if(PureOutOfBounds())
        {
            //Debug.Log("Touching the edge");
            DontWalkOffEdge();
        }
            
    }

    void TriggerFight()
    {

    }

    bool OutOfBounds()
	{
        if (player.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x || player.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x)
        {
            return true;
        }
        if (player.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y || player.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y)
        {
            return true;
        }
        return false;
	}

    bool PureOutOfBounds()
    {
        if (player.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x - (player.transform.localScale.x/2)|| player.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x + (player.transform.localScale.x/2))
        {
            return true;
        }
        if (player.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y - (player.transform.localScale.y/2)|| player.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y + (player.transform.localScale.y/2))
        {
            return true;
        }
        return false;
    }

    Vector2 SearchForDirection()
    {
        if (player.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x)
            return Vector2.left;
        if (player.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x)
            return Vector2.right;

        if(player.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y)
            return Vector2.down;
        if (player.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y)
            return Vector2.up;
        return Vector2.zero;
    }

    Vector2 BoundsSearchForDirection()
    {
        if (player.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x + (player.transform.localScale.x/2))
            return Vector2.left;
        if (player.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x - (player.transform.localScale.x/2))
            return Vector2.right;

        if(player.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y + (player.transform.localScale.y/2))
            return Vector2.down;
        if (player.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y - (player.transform.localScale.y/2))
            return Vector2.up;
        return Vector2.zero;
    }

    void FindPointsInDifferentDirectionsSetup()
    {
        leftPoints.Clear();
        rightPoints.Clear();
        upPoints.Clear();
        downPoints.Clear();

        if(points.Length != 0)
        {
            foreach(GameObject point in points)
            {
                if(point.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x) //Left
                    //if(!leftPoints.Contains(point))
                    leftPoints.Add(point);

                if(point.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x) //Right
                    //if(!rightPoints.Contains(point))
                    rightPoints.Add(point);

                if(point.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y) //Down
                    //if(!downPoints.Contains(point))
                    downPoints.Add(point);

                if(point.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y) //Up
                    //if(!upPoints.Contains(point))
                    upPoints.Add(point);
            }
        }
    }

    void FindAllPointsInCurrentDirection()
    {
        //pointsBeyond.Clear();

            float z = Mathf.Infinity;
            GameObject previousPoint = currentPoint;
            if(direction == Vector2.left)
            {
                foreach(GameObject point in leftPoints)
                {
                    float distance = Vector3.Distance(point.transform.position, player.transform.position);
                    if(distance < z)
                    {
                        z = distance;
                        currentPoint = point;
                    }
                }

            }
            else if(direction == Vector2.right)
            {
                foreach(GameObject point in rightPoints)
                {
                    float distance = Vector3.Distance(point.transform.position, player.transform.position);
                    if(distance < z)
                    {
                        z = distance;
                        currentPoint = point;
                    }
                }

            }
            else if(direction == Vector2.down)
            {
                foreach(GameObject point in downPoints)
                {
                    float distance = Vector3.Distance(point.transform.position, player.transform.position);
                    if(distance < z)
                    {
                        z = distance;
                        currentPoint = point;
                    }
                }

            }
            else if(direction == Vector2.up)
            {
                foreach(GameObject point in upPoints)
                {
                    float distance = Vector3.Distance(point.transform.position, player.transform.position);
                    if(distance < z)
                    {
                        z = distance;
                        currentPoint = point;
                    }
                }

            }
    }

    void DontWalkOffEdge()
    {
        Vector2 position = BoundsSearchForDirection();

        float x = player.transform.position.x;
        float y = player.transform.position.y;

        if(position == Vector2.left)
        {
            if(leftPoints.Count == 0)
            {
                x = Camera.main.transform.position.x - halfCameraSize.x + (player.transform.localScale.x/2);
            }
        }
        else if(position == Vector2.right)
        {
            if(rightPoints.Count == 0)
            {
                x = Camera.main.transform.position.x + halfCameraSize.x - (player.transform.localScale.x/2);
            }
        }
        else if(position == Vector2.up)
        {
            if(upPoints.Count == 0)
            {
                y = Camera.main.transform.position.y + halfCameraSize.y - (player.transform.localScale.y/2);
            }
        }
        else if(position == Vector2.down)
        {
            if(downPoints.Count == 0)
            {
                y = Camera.main.transform.position.y - halfCameraSize.y + (player.transform.localScale.y/2);
            }
        }

        player.transform.position = new Vector3(x,y,player.transform.position.z);
    }

    bool CheckMatch(List<GameObject> l1, List<GameObject> l2) 
    {
        if (l1.Count != l2.Count)
            return false;
        
        for (int i = 0; i < l1.Count; i++) {
            if (l1[i] != l2[i])
                return false;
            }
        return true;
    }
}
