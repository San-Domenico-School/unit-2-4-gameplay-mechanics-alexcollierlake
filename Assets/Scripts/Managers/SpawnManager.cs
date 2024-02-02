using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************
 * 
 * Alexandra Collier-Lake
 * 1/29/1014
 * Spawn Manager: controls waves of ice spheres
 * Component of spawn Manager
 * 
 *************/

public class SpawnManager : MonoBehaviour
{

    [Header("Objects to Spawn")]
    [SerializeField] private GameObject iceSphere, portal, powerUp;

    [Header("Wave Fields")]
    [SerializeField] private int initialWave, increaseEachWave, maximumWave;

    [Header("Portal Fields")]
    [SerializeField] private int portalFirstAppearance;
    [SerializeField] private float portalByWaveProbability, portalByWaveDuration;

    [Header("PowerUp Fields")]
    [SerializeField] private int powerUpFirstAppearance;
    [SerializeField] private float powerUpWaveProbability, powerUpByWaveDuration;

    [Header("Island")]
    [SerializeField] private GameObject island;

    private Vector3 islandSize;
    private int waveNumber;
    private bool portalActive, powerUpActive;


    // Start is called before the first frame update
    void Start()
    {

        islandSize = island.GetComponent<MeshCollider>().bounds.size;
        waveNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {

        //IceSphere[] iceSpheres = FindObjectsOfType<IceSpheres>();
        if (GameObject.Find("Player") != null && FindObjectsOfType<IceSphereController>().Length == 0)
        {
            SpawnIceWave();
        }
    }

    private void SpawnIceWave()
    {

        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(iceSphere, SetRandomPosition(1.6f), iceSphere.transform.rotation);
        }

        if (waveNumber <= maximumWave)
        {
            waveNumber++;
        }


    }

    private void SetObjectActive(GameObject obj, float byWaveProbability)
    {
        //
    }

    private Vector3 SetRandomPosition(float posY)
    {
        float randomX = Random.Range(-islandSize.x / 2, islandSize.x / 2);
        float randomZ = Random.Range(-islandSize.z / 2, islandSize.x / 2);

        return new Vector3(randomX, posY, randomZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        return null;
    }
}
