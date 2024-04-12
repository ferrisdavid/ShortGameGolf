using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour, Grabbable
{
    // Reference to GameState.
    [SerializeField]
    private GameState gameState;

    // Grab Fields.
    [SerializeField]
    private Transform attachPivot;
    private GameObject attachedHand;

    public void OnGrab(GameObject hand) {
        hand.transform.position = attachPivot.position;
        // Create Fixed Joint for hand
        FixedJoint joint = hand.AddComponent<FixedJoint>();

        GetComponent<Rigidbody>().isKinematic = false;
        // Assign Break Force and Break Torque Properties for Joint
        joint.breakForce = 100000;
        joint.breakTorque = 100000;

        // Assign Object as connected rigidbody
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint.connectedAnchor = attachPivot.position;

        attachedHand = hand;
    }

    public void OnRelease() {
        // Reset the Connected Body Property of the Controller fixed joint.
        attachedHand.GetComponent<FixedJoint>().connectedBody = null;
        // // Manually Destroy the Fixed Joint
        Destroy(attachedHand.GetComponent<FixedJoint>());
        
        // Reset Hand References.
        attachedHand = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attachedHand) StartCoroutine(ReturnToClubhouse());
    }

    IEnumerator ReturnToClubhouse() {
        yield return new WaitForSeconds(0.5f);
        gameState.ReturnToClubhouse();
    }
}
