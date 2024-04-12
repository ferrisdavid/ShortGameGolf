using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionTracker : MonoBehaviour
{
    // Track the Position of the Golf Ball and Player to determine when teleportation should be enabled/prompted

    // Reference to Player Object
    [SerializeField]
    private GameObject player;

    // Min Distance Threshold for Player to Ball to allow teleportation.
    [SerializeField]
    private float teleportEnableThreshold;
    
    // Reference to PlayerController
    private PlayerController PlayerManager;

    // Reference to Ball Trail Particle System.
    [SerializeField]
    private ParticleSystem ballTrail;


    // Start is called before the first frame update
    void Start()
    {
        PlayerManager = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.sqrMagnitude > 0) {
            ballTrail.Play();
            ScaleBallTrailByVelocity();
            AlignBallTrail();
        }
        else {
            ResetTrailToDefault();
        }
    }

    private void FixedUpdate() {
        if (ComputeDistanceToPlayer(player.transform) >= teleportEnableThreshold && gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.05) {
           if (!PlayerManager.isTeleportEnabled) PlayerManager.ToggleTeleportEnable(true);
        }
        else {
            if (PlayerManager.isTeleportEnabled) PlayerManager.ToggleTeleportEnable(false);
        }
    }

    // Helper Function to Compute the distance from the player to the ball used to determine whether teleportation should be enabled/prompted
    private float ComputeDistanceToPlayer(Transform player) {
        return (transform.position - player.transform.position).magnitude;
    }

    // Helper Function to Manage Particle System on Ball
    private void AlignBallTrail() {
        Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
        ParticleSystem.ShapeModule systemShape = ballTrail.shape;

        systemShape.rotation = Quaternion.FromToRotation(ballVelocity.normalized, systemShape.rotation).eulerAngles;
    }

    private void ScaleBallTrailByVelocity() {
        Vector3 ballVelocity = GetComponent<Rigidbody>().velocity;
        ParticleSystem.MainModule trailProperties = ballTrail.main;
        trailProperties.startSpeed = Mathf.Clamp(ballVelocity.sqrMagnitude, 0, 20);
    }

    private void ResetTrailToDefault() {
        ballTrail.Stop();
        ParticleSystem.MainModule trailProperties = ballTrail.main;
        trailProperties.startSpeed = 0;
    }
}
