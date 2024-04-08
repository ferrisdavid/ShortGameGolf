using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sits on The Golf Course Hole Inside Collder Object and Handles the Detection of the ball, triggers physics updates and success state.
public class HoleDetect : MonoBehaviour
{
    // Reference to Global Game State to Trigger Win State.
    [SerializeField]
    private GameState gameState;

    // Reference to Hole Particle System
    [SerializeField]
    private ParticleSystem confettiSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("ball")) {
            other.gameObject.layer = 10;
        }
    }
    private IEnumerator OnTriggerStay(Collider other) {
        yield return new WaitForSeconds(0.5f);
        if (other.gameObject.CompareTag("ball") && !gameState.isWin) {
            gameState.isWin = true;
            confettiSystem.Play();
            GetComponentInParent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
