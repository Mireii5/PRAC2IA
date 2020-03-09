using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANT_BLACKBOARD : MonoBehaviour
{
    public float reachedRadius = 2.0f;

    private GameObject[] foodPoints;
    private GameObject[] exitPoints;
    // Start is called before the first frame update
    void Start()
    {
        foodPoints = GameObject.FindGameObjectsWithTag("FOODPOINTS");
        exitPoints = GameObject.FindGameObjectsWithTag("EXITPOINT");
    }

    // Update is called once per frame
    public GameObject GetRandomFoodPoints()
    {
        return foodPoints[Random.Range(0, foodPoints.Length)];
    }

    public GameObject GetRandomExitPoints()
    {
        return exitPoints[Random.Range(0, exitPoints.Length)];
    }
}
