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
    public List<GameObject> pointsBeyond = new List<GameObject>();

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
    }
    bool hasMoved;
    // Update is called once per frame
    void Update()
    {
        FindPointsInDifferentDirections();
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
            pointsBeyond.Clear();
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

    Vector2 SearchForDirection()
    {
        if (player.transform.position.x < Camera.main.transform.position.x - halfCameraSize.x)
            return Vector2.left;
        if (player.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x )
            return Vector2.right;

        if(player.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y)
            return Vector2.down;
        if (player.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y)
            return Vector2.up;
        return Vector2.zero;
    }

    void FindPointsInDifferentDirections()
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
                    leftPoints.Add(point);

                if(point.transform.position.x > Camera.main.transform.position.x + halfCameraSize.x) //Right
                    rightPoints.Add(point);

                if(point.transform.position.y < Camera.main.transform.position.y - halfCameraSize.y) //Down
                    downPoints.Add(point);

                if(point.transform.position.y > Camera.main.transform.position.y + halfCameraSize.y) //Up
                    upPoints.Add(point);

            }
        }
    }

    void FindAllPointsInCurrentDirection()
    {
        pointsBeyond.Clear();

            if(direction == Vector2.left)
                pointsBeyond = leftPoints;
            else if(direction == Vector2.right)
                pointsBeyond = rightPoints;
            else if(direction == Vector2.down)
                pointsBeyond = downPoints;
            else if(direction == Vector2.up)
                pointsBeyond = upPoints;

        if(pointsBeyond.Count != 0)
        {
            float z = Mathf.Infinity;
            foreach(GameObject point in pointsBeyond)
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
}
