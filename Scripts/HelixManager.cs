using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 0;
    public float ringsDistance = 5;
    public int numberOfRings;
    // Start is called before the first frame update
    void Start()
    {
        numberOfRings = GameManager.currentLevelIndex + 5;

        SpawnRings(0);
        for (int i = 0; i < numberOfRings; i++)
            {
            SpawnRings(Random.Range(1, helixRings.Length-1));
            }
        SpawnRings(helixRings.Length-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRings(int ringIndex)
        {
        GameObject gameOb = Instantiate(helixRings[ringIndex], transform.up * ySpawn, Quaternion.identity);
        gameOb.transform.parent = transform;
        ySpawn -= ringsDistance;
        }
}
