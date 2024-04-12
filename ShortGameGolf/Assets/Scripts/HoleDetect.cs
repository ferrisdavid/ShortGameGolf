using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sits on The Golf Course Hole Inside Collder Object and Handles the Detection of the ball, triggers physics updates and success state.
public class HoleDetect : MonoBehaviour
{
    // Reference to Global Game State to Trigger Win State.
    private GameState gameState;

    // Reference to Hole Particle System
    [SerializeField]
    private ParticleSystem confettiSystem;

    Rigidbody ballRB;
 
    float MaxDistance = 0.5f; // Maximum range at which the marble will start being pulled to the cup
    float MaxStrength = 1f; // Strength with which the marble will be pulled when it is right next to the cup (reduces with distance)
 
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        ballRB = GameObject.FindGameObjectWithTag("ball").GetComponent<Rigidbody>();
    }

    private IEnumerator OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("ball")) {
            other.gameObject.layer = 10;
        }
        
        yield return new WaitForSeconds(0.5f);
        if (other.gameObject.CompareTag("ball") && !gameState.isWin) {
            gameState.isWin = true;
            gameState.SetHoleFinalScore();

            confettiSystem.Play();
            GetComponentInParent<AudioSource>().Play();
        }
    }

    void ApplyMagnetism()
    {
        float Distance = Vector3.Distance(ballRB.transform.position, transform.position);
 
        if (Distance < MaxDistance) // Ball is in range of the hole
        {
            float TDistance = Mathf.InverseLerp(MaxDistance, 0f, Distance); // Give a decimal representing how far between 0 distance and max distance.
            float strength = Mathf.Lerp(0f, MaxStrength, TDistance); // Use that decimal to work out how much strength the magnet should apply
            Vector3 DirectionToCup = (transform.position - ballRB.transform.position).normalized; // Get the direction from the ball to the hole
 
            ballRB.AddForce(DirectionToCup * strength, ForceMode.Force);// apply force to the ball
 
        }
    }


    // Update is called once per frame
    void Update()
    {
        ApplyMagnetism();
        if (gameState.isWin) ballRB.constraints = RigidbodyConstraints.None;
    }
}
