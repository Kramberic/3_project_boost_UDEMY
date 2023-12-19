using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("Game over timer")]
    [Tooltip("Set the amount of seconds the game will continue after you crash."
    + " You will not have control of the rocket during this time.")]
    [SerializeField] float timerDelay = 1f;
    
    [Header("End Game Audio")]
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;

    [Header("End Game Particles")]
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource aS;
    CapsuleCollider rocketColliders;

    bool isTransitioning = false;
    bool collisionsEngaged = false;

    void Start()
    {
        aS = GetComponent<AudioSource>();
        rocketColliders = GetComponent<CapsuleCollider>();
        isTransitioning = true;
    }

    void Update()
    {
        DebugOptions();
    }

    // DBG options method (udemy sollution) ----------------------------------------------------------------------
    void DebugOptions()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // toggle to next level (Q)
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.W)) // toggle if collisions are working (W)
        {
            collisionsEngaged = !collisionsEngaged;
            Debug.Log("Collisions = " + collisionsEngaged);
        }   
    }

    // int currentSceneIndex = 0;
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            switch (other.gameObject.tag)
            {
                case "Friendly": // when you land on the takeoff pad
                    Debug.Log("Rocket landed on the takeoff pad.");
                    break;
                case "Finish": // when you finish the level
                    startNextLevelsequence();
                    isTransitioning = false;
                    break;
                default:
                    StartCrashSequence(); // when you crash the rocket
                    isTransitioning = false;
                    break;
            }
        }
    }
    /*
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionsEngaged) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly": // when you land on the takeoff pad
                Debug.Log("Rocket landed on the takeoff pad.");
                break;
            case "Finish": // when you finish the level
                startNextLevelsequence();
                isTransitioning = false;
                break;
            default:
                StartCrashSequence(); // when you crash the rocket
                isTransitioning = false;
                break;
        }
    }
    */


    void startNextLevelsequence()
    {
        isTransitioning = true;
        aS.PlayOneShot(successAudio);
        successParticles.Play(successParticles);
        GetComponent<RocketMovement>().enabled = false;        
        Invoke("NextLevel", timerDelay);
    }

    void StartCrashSequence()
    {   
        isTransitioning = true;
        aS.Stop();
        aS.PlayOneShot(crashAudio);
        crashParticles.Play(crashParticles);
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", timerDelay);
    }

    
    void NextLevel() // method to load next level when you land on a landing pad of a previous level
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }

        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    
    void ReloadLevel() // method for reloading same level if rocket crashes
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }
}
