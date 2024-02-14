using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



/*************
 * 
 * Alexandra Collier-Lake
 * 1/16/1014
 * Game Manager: controls levels and start/end of the game
 * Component of the game manager
 * 
 *************/

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private int numLevel;
    [SerializeField] private GameObject[] waypoints;

    [Header("Player Fields")]
    public Vector3 playerScale;
    public float playerMass, playerDrag, playerMoveForce, playerRepelForce, playerBounce;

    [Header("Debug Fields")]
    public bool debugSpawnWaves, debugSpawnPortal, debugSpawnPowerUp, debugPowerUpRepel;

    public bool switchLevels { private get; set; }
    public bool gameOver { private get; set; }
    public bool playerHasPowerUp { get; set; }


    private void Awake()
    {
        // Awake is called before any Start methods are called
        //This is a common approach to handling a class with a reference to itself.
        //If instance variable doesn't exist, assign this object to it
        if (Instance == null)
        {
            Instance = this;
        }
        //Otherwise, if the instance variable does exist, but it isn't this object, destroy this object.
        //This is useful so that we cannot have more than one GameManager object in a scene at a time.
        else if (Instance != this)
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(switchLevels == true)
        {
            SwitchLevels();
        }
    }

    private void EnablePlayer()
    {
        //
    }

    private void SwitchLevels()
    {
        switchLevels = false;

        string currentScene = SceneManager.GetActiveScene().name;

        int nextLevel = int.Parse(currentScene.Substring(5)) + 1;

        if(nextLevel <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Level " + nextLevel.ToString());
        }
        else
        {
            gameOver = true;
            Debug.Log("You Won");
        }
    }
}
