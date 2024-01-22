using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*************
 * 
 * Alexandra Collier-Lake
 * 1/16/1014
 * Player Controller: controls the movement of player
 * Component of the player
 * 
 *************/

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;
    private SphereCollider playerCollider;
    private Light powerUpIndicator;
    private PlayerInputActions inputAction;
    private Transform focalPoint;
    private float moveForceMagnitude, moveDirection;

    public bool hasPowerUp { get; private set; }



    private void Awake()
    {
        inputAction = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        powerUpIndicator = GetComponent<Light>();
        playerCollider.material.bounciness = 0.0f;
        powerUpIndicator.intensity = 0.0f;


    }

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Movement.performed += OnMovementPerformed;
        inputAction.Player.Movement.canceled += OnMovememntCanceled;
    }

    private void OnDisable()
    {
        inputAction.Disable();
        inputAction.Player.Movement.performed -= OnMovementPerformed;
        inputAction.Player.Movement.canceled -= OnMovememntCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
       
        moveDirection = value.ReadValue<Vector2>().y;
    }

    private void OnMovememntCanceled(InputAction.CallbackContext value)
    {
        moveDirection = 0f;
    }
    private void AssignLevelValues()
    {
        transform.localScale = GameManager.Instance.playerScale;
        playerRB.mass = GameManager.Instance.playerMass;
        playerRB.drag = GameManager.Instance.playerDrag;
        moveForceMagnitude = GameManager.Instance.playerMoveForce;
        focalPoint = GameObject.Find("FocalPoint").transform;
    }

    private void Move()
    {
        if(focalPoint != null)
        {
            
            playerRB.AddForce(focalPoint.forward.normalized * moveForceMagnitude * moveDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Startup"))
        {
            collision.gameObject.tag = "Ground";
            playerCollider.material.bounciness = GameManager.Instance.playerBounce;
            AssignLevelValues();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //
    }

    private void OnTriggerExit(Collider other)
    {
        //
    }

    private IEnumerator PowerUpCooldown(float cooldown)
    {
        return null;
    }
}
