using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPositionTracker : MonoBehaviour
{
    // Track the Position of the Golf Ball and Player to determine when teleportation should be enabled/prompted


    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float teleportEnableThreshold;

    private PlayerController PlayerManager;



    // Start is called before the first frame update
    void Start()
    {
        PlayerManager = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (ComputeDistanceToPlayer(player.transform) >= teleportEnableThreshold && gameObject.GetComponent<Rigidbody>().velocity.magnitude <= 0.05) {
            PlayerManager.ToggleTeleportEnable(true);
        }
        else {
            PlayerManager.ToggleTeleportEnable(false);
        }
    }

    // Helper Function to Compute the distance from the player to the ball used to determine whether teleportation should be enabled/prompted
    private float ComputeDistanceToPlayer(Transform player) {
        return (transform.position - player.transform.position).magnitude;
    }
}
