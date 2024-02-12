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
    [SerializeField] private GameObject iceSphere;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject powerUp;

    [Header("Wave Fields")]
    [SerializeField] private int maximumWave;
    [SerializeField] private int increaseEachWave;
    [SerializeField] private int initialWave;

    [Header("Portal Fields")]
    [SerializeField] private int portalFirstAppearance;
    [SerializeField] private float portalByWaveProbability;
    [SerializeField] private float portalByWaveDuration;

    [Header("PowerUp Fields")]
    [SerializeField] private int powerUpFirstAppearance;
    [SerializeField] private float powerUpWaveProbability;
    [SerializeField] private float powerUpByWaveDuration;

    [Header("Island")]
    [SerializeField] private GameObject island;

    private Vector3 islandSize;
    private int waveNumber;
    private bool portalActive, powerUpActive;


    // Start is called before the first frame update
    void Start()
    {

        islandSize = island.GetComponent<MeshCollider>().bounds.size;
        waveNumber = initialWave;
    }

    // Update is called once per frame
    void Update()
    {
        if((waveNumber > portalFirstAppearance || GameManager.Instance.debugSpawnPortal) && !portalActive)
        {
            SetObjectActive(portal, portalByWaveProbability);
        }
    
        if (GameObject.Find("Player") != null && FindObjectsOfType<IceSphereController>().Length == 0)
        {
            SpawnIceWave();
        }

        if (GameManager.Instance.debugSpawnPortal)
        {
            portalByWaveDuration = 99;
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
        if(Random.value < waveNumber * byWaveProbability * Time.deltaTime)
        {
            obj.transform.position = SetRandomPosition(obj.transform.position.y);
            StartCoroutine(CountdownTimer(obj.tag));
        }
    }

    private Vector3 SetRandomPosition(float posY)
    {
        float randomX = Random.Range(-islandSize.x / 2, islandSize.x / 2);
        float randomZ = Random.Range(-islandSize.z / 2, islandSize.x / 2);

        return new Vector3(randomX, posY, randomZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        float byWaveDuration = 0;

        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(true);
                portalActive = true;
                byWaveDuration = portalByWaveDuration;
                break;
        }

        yield return new WaitForSeconds(waveNumber * byWaveDuration);

        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(false);
                portalActive = false;
                break;
        }
    }
}
