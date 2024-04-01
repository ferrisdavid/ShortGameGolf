using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceScorePickup : MonoBehaviour
{
    // Reference to global GameState.
    private GameState gameState;
    // Reference to Confetti Particle System
    [SerializeField]
    private ParticleSystem confettiSystem;
    // Reference to Game ScoreCard Object
    private ScorePadController scorePad;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        scorePad = GameObject.Find("ScorePad").GetComponent<ScorePadController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Detect Pickup Collision with Golf Ball.
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("ball")) {
            OnPickupActivate();
        }
    }

    // On Pickup Collision Functionality Handler.
    public void OnPickupActivate() {
        // Handle Pickup Specific GameState Changes
        gameState.DecrementStrokes();
        
        // Handle General Pickup Collection State Changes
        StartCoroutine(PickupEffect());
    }

    IEnumerator PickupEffect() {
        confettiSystem.Play();
        GetComponent<AudioSource>().Play();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1);

        Destroy(gameObject); 
    }
}
