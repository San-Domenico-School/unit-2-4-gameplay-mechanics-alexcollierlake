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
        iceSphereRB = GetComponent<Rigidbody>();
        iceSphereController = GetComponent<IceSphereController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if(player.GetComponent<PlayerController>().hasPowerUp)
            {
                iceSphereRB.AddForce(-direction * playerRB.mass * GameManager.Instance.playerRepelForce, ForceMode.Impulse);
            }
            else
            {
                playerRB.AddForce(-direction * playerRB.mass * GameManager.Instance.playerRepelForce, ForceMode.Impulse);
            }
        }

    }
    
}
