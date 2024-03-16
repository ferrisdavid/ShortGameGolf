using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private float teleportEnableThreshold;
    public bool isTeleportEnabled = false;
    public Vector3 teleportPoint;
    
    [SerializeField]
    private Vector3 teleportOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate() {
        if (ComputeDistanceToBall(ball) >= teleportEnableThreshold) {
            isTeleportEnabled = true;

            // Compute Point to Teleport to
            teleportPoint = ball.position - teleportOffset;
        }
        else {
            isTeleportEnabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Helper Function to Compute the distance from the player to the ball used to determine whether teleportation should be enabled/prompted
    private float ComputeDistanceToBall(Transform ball) {
        return (ball.position - transform.position).magnitude;
    }
}
