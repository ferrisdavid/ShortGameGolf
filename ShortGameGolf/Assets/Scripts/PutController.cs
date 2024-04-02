using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PutController : MonoBehaviour
{
    // High Level Global Game State Object
    private GameState gameState;

    // Vibration Instance Variables
    public float minVibrationForce = 0.0f;
    public float maxVibrationForce = 0.2f;

    // Club Rigidbody;
    Rigidbody clubRB;
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("ball")) {
            // Increment Game State Stroke Count
            gameState.IncrementStrokes();

            GameObject attachedHand = gameObject.GetComponentInParent<ClubGrab>().GetAttachedHand();

            // Calculate Vibration force based on club velocity
            float amplitude = Mathf.Clamp((clubRB.velocity.magnitude - 50)/50, minVibrationForce, maxVibrationForce);

            // Perform Haptic Vibration
            SteamVR_Input_Sources hand = attachedHand.CompareTag("left") ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand;
            SteamVR_Actions.default_Haptic[hand].Execute(0, 0.2f, 1, amplitude);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        clubRB = gameObject.GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
