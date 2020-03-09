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
        antEggPrefab = Resources.Load<GameObject>("EGG_ANT");
        antSeedPrefab = Resources.Load<GameObject>("SEED_ANT");
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 10)
        {
            Debug.Log("HOLA");
            int number = Random.Range(0, 10);
            AntSpawn(number);
            timer = 0;
        }
        
	}

    void AntSpawn (int number)
    {
        if (number <= 2)
        {
            var position = this.transform.position;
            position.z = 0;
            GameObject antEgg = GameObject.Instantiate(antEggPrefab);
            antEgg.transform.position = position;
        }
        else
        {
            var position = this.transform.position;
            position.z = 0;
            GameObject antSeed = GameObject.Instantiate(antSeedPrefab);
            antSeed.transform.position = position;
        }
    }


}
