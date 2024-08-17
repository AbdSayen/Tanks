using System.Collections;
using UnityEngine;

public class BoostsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] boostsPrefabs;
    [SerializeField] private Transform[] spawnPlaces;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (; ; )
        {
            GameObject boost = Instantiate(boostsPrefabs[Random.Range(0, boostsPrefabs.Length)]);
            boost.transform.position = spawnPlaces[Random.Range(1, spawnPlaces.Length)].position;
            
            yield return new WaitForSeconds(15);
        }
    }
}