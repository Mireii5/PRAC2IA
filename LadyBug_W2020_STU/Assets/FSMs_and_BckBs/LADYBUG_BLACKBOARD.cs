using UnityEngine;

public class LADYBUG_BLACKBOARD : MonoBehaviour
{
    public float reachedRadius = 2.0f;
    public float detectionClosestEggRadius = 50;
    public float detectionEggRadius = 180;
    public float detectionClosestSeedRadius = 80;
    public float detectionSeedRadius = 125;
    public float detectionEggWhileTranspotingSeed = 25;

    private GameObject[] hatchingPoints;
    private GameObject[] storePoints;
    public GameObject[] wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        hatchingPoints = GameObject.FindGameObjectsWithTag("HATCHINGPOINT");
        storePoints = GameObject.FindGameObjectsWithTag("STOREPOINT");
        storePoints = GameObject.FindGameObjectsWithTag("WAYPOINT");
    }

    // Update is called once per frame
    public GameObject GetRandomHatchingPoint()
    {
        return hatchingPoints[Random.Range(0, hatchingPoints.Length)];
    }

    public GameObject GetRandomStorePoint()
    {
        return storePoints[Random.Range(0, storePoints.Length)];
    }

    public GameObject GetRandomWayPoint()
    {
        return wayPoints[Random.Range(0, wayPoints.Length)];
    }
}
