using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeSwingPickup : MonoBehaviour
{
    // Reference to global GameState.
    private GameState gameState;

    // Reference to Confetti Particle System
    [SerializeField]
    private ParticleSystem confettiSystem;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
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
        gameState.AddFreeStroke();

        // Handle General Pickup Collection State Changes
        confettiSystem.Play();
        GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
