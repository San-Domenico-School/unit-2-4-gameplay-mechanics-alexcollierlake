using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private GameManager gameManager;


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
        gameManager = GetComponent<GameManager>();
        //hasPowerUp = true;


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
    void FixedUpdate()
    {
        Move();
        if(transform.position.y < -20)
        {
            Debug.Log("You Lost");
            gameObject.SetActive(false);
            SceneManager.LoadScene("Level 1");
        }
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

        gameObject.layer = LayerMask.NameToLayer("Player");
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
        if(other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Portal");
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpController controller = other.GetComponent<PowerUpController>();
            other.gameObject.SetActive(false);
            StartCoroutine(PowerUpCooldown(5f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            if (transform.position.y <= other.transform.position.y - 1f)
            {
                transform.position = Vector3.up * 25f;
                GameManager.Instance.switchLevels = true;
            }
        }

    }

    private IEnumerator PowerUpCooldown(float cooldown)
    {
        hasPowerUp = true;
        powerUpIndicator.intensity = 3.5f;
        yield return new WaitForSeconds(cooldown);
        hasPowerUp = false;
        powerUpIndicator.intensity = 0.0f;
    }

}
