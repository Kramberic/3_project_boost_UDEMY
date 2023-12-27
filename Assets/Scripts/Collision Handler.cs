using System;
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

    bool isTransitioning = false;
    bool collisionsDisabled = false;

    // ------------------------------------------------------------------------------------------
    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugOptions();
    }
    // ------------------------------------------------------------------------------------------

    
    void DebugOptions() // DBG options method (udemy sollution)
    {
        if (Input.GetKeyDown(KeyCode.Q)) // toggle to next level (Q)
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.W)) // toggle if collisions are working (W)
        {
            collisionsDisabled = !collisionsDisabled;
            Debug.Log("Collisions disabled = " + collisionsDisabled);
        }   
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning  || collisionsDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly": // when you land on the takeoff pad
                break;
            case "Finish": // when you finish the level
                startNextLevelsequence();
                break;
            default:
                StartCrashSequence(); // when you crash the rocket
                break;
        }
    }

    void startNextLevelsequence()
    {
        isTransitioning = true;
        aS.Stop();
        aS.PlayOneShot(successAudio);
        successParticles.Play(successParticles);
        GetComponent<RocketMovement>().enabled = false;        
        Invoke("NextLevel", timerDelay);
        Start();
    }

    void StartCrashSequence()
    {   
        isTransitioning = true;
        aS.Stop();
        aS.PlayOneShot(crashAudio);
        crashParticles.Play(crashParticles);
        GetComponent<RocketMovement>().enabled = false;
        Invoke("ReloadLevel", timerDelay);
        Start();
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
        // EASY MODE WHERE YOU CONTINUE WITH THE SAME LEVEL
        /*
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
        */

        // HARD MODE WHERE YOU RESTART ON LEVEL 1 WHEN YOU DIE
        SceneManager.LoadScene(0);
    }
}
