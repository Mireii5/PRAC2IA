using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour {

    private GameObject antEggPrefab;
    private GameObject antSeedPrefab;
    private float timer = 0;

	// Use this for initialization
	void Start () {
        // get the prefabs
        antEggPrefab = Resources.Load<GameObject>("Resources/EGG_ANT");
        antSeedPrefab = Resources.Load<GameObject>("Resources/SEED_ANT");
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Debug.Log(timer);
        /*if (timer >= 12)
        {
            Debug.Log("HOLA");
            int number = Random.Range(0, 10);
            AntSpawn(number);
            timer = 15;
        }*/
        
	}

    void AntSpawn (int number)
    {
        if (number <= 2)
        {
            Instantiate(antEggPrefab);
        }
        else
        {
            Instantiate(antSeedPrefab);
        }
    }


}
