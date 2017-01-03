using UnityEngine;
using System.Collections;

public class RandomLevelGenerator : MonoBehaviour
{

    private const int TOTAL_CIRCLES_OBSTACLES = 10;
    private const int CIRCLES_OBSTACLE_MINSCALE = 2;
    private const int CIRCLES_OBSTACLE_MAXSCALE = 9;
    private const float X_POS = 30.0f;
    private const float Y_POS = 15.0f;
    private const float OFFSET = 20.0f;

    private Transform circleParentTransform;
    private GameObject circleObstaclePrefab;
    private GameObject[] circleObstacles;
    private float[] radii;
    private Vector3[] positions;
    private bool isPositionNotAcceptable;
    private GameObject avoidingActor;

    private void Awake()
    {
        circleParentTransform = GameObject.Find("CircleParent").GetComponent<Transform>();
        circleObstaclePrefab = Resources.Load<GameObject>("Prefabs/CircleObstacle");
        circleObstacles = new GameObject[TOTAL_CIRCLES_OBSTACLES];
        radii = new float[TOTAL_CIRCLES_OBSTACLES + 1];
        positions = new Vector3[TOTAL_CIRCLES_OBSTACLES + 1];
        avoidingActor = GameObject.Find("AvoidingActor");
        radii[0] = avoidingActor.GetComponent<SpriteRenderer>().bounds.extents.x;
        positions[0] = avoidingActor.transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < circleObstacles.Length; i++)
        {
            circleObstacles[i] = Instantiate(circleObstaclePrefab, circleParentTransform) as GameObject;
            float scale = Random.Range(CIRCLES_OBSTACLE_MINSCALE, CIRCLES_OBSTACLE_MAXSCALE);
            circleObstacles[i].transform.localScale = new Vector3(scale, scale, 1.0f);
            radii[i + 1] = circleObstacles[i].GetComponent<SpriteRenderer>().bounds.extents.x;
            isPositionNotAcceptable = true;
            while (isPositionNotAcceptable)
            {
                positions[i + 1] = GenerateRandomPostion();
                AcceptaceChecker(i + 1);
            }
            circleObstacles[i].transform.position = positions[i + 1];
            circleObstacles[i].transform.rotation = Quaternion.identity;
        }
    }

    private Vector3 GenerateRandomPostion()
    {
        return new Vector3(Random.Range(-X_POS, X_POS), Random.Range(-Y_POS, Y_POS), 0.0f);
    }

    private void AcceptaceChecker(int currentIndex)
    {
        bool result = false;
        for (int j = 0; j < currentIndex; j++)
        {
            float sqr_dist = (positions[j] - positions[currentIndex]).sqrMagnitude;
            float sqr_min_dist = (radii[j] + radii[currentIndex]) * (radii[j] + radii[currentIndex]);
            if (sqr_dist > (sqr_min_dist + OFFSET))
            {
                result = true;
            }
            else
            {
                result = false;
                break;
            }
        }
        if (result)
            isPositionNotAcceptable = false;
        else
            isPositionNotAcceptable = true;
    }
}
