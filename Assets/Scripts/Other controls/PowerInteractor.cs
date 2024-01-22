using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*************
 * 
 * Alexandra Collier-Lake
 * 1/22/1014
 * Power interactor: //
 * Component of ice sphere
 * 
 *************/

public class PowerInteractor : MonoBehaviour
{

    [SerializeField] private float pushForce;
    private Rigidbody iceSphereRB;
    private IceSphereController iceSphereController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //
    }
    
}
