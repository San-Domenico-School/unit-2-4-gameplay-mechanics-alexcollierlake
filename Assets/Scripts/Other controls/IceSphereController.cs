using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************
 * 
 * Alexandra Collier-Lake
 * 1/22/1014
 * Ice sphere controller: //
 * Component of ice sphere
 * 
 *************/

public class IceSphereController : MonoBehaviour
{

    [SerializeField] private float startDelay, reductionEachRepeat, minimumVolume;
    private Rigidbody iceRB;
    private ParticleSystem iceVFX;
    private float pushForce;
    



    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.debugSpawnWaves)
        {
            reductionEachRepeat = .5f;
        }

        iceRB = GetComponent<Rigidbody>();
        iceVFX = GetComponent<ParticleSystem>();
        RandomizeSizeAndMass();

        InvokeRepeating("Melt", startDelay, 0.4f);
    }

    private void RandomizeSizeAndMass()
    {
        Vector3 originalScale = transform.localScale;
        float originalMass = GetComponent<Rigidbody>().mass;
  
        float randomScalePercent = Random.Range(0.5f, 1.0f);
        float randomMassPercent = Random.Range(0.5f, 1.0f);

        //creating a random new mass and scale that is 50% - 100% of the 
        Vector3 newScale = originalScale * randomScalePercent;
        float newMass = originalMass * randomMassPercent;

        //set scale and mass to new scale and mass
        transform.localScale = newScale;
        GetComponent<Rigidbody>().mass = newMass;
    }

    private void Dissolution()
    { 
            Destroy(gameObject);

    }

    private void Melt()
    {
        float volume = 4f / 3f * Mathf.PI * Mathf.Pow(transform.localScale.x, 3);
        

        if (volume > minimumVolume)
        {
            transform.localScale *= reductionEachRepeat;
            GetComponent<Rigidbody>().mass *= reductionEachRepeat;
        }
        else
        {
            Dissolution();
        }


    }



   
}
