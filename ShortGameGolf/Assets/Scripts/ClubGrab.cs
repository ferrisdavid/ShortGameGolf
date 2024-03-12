using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClubGrab : MonoBehaviour, Grabbable
{
    [SerializeField]
    private Transform attachPivot;
    private GameObject attachedHand;



    public void OnGrab(GameObject hand) {
        // Rotate to have properly oriented club head
        transform.localRotation = Quaternion.LookRotation(hand.transform.up, -hand.transform.forward);

        // Translate Grip to Anchor Point.
        hand.transform.position = attachPivot.position;

         // Create Fixed Joint for hand
        FixedJoint joint = hand.AddComponent<FixedJoint>();
        // Assign Break Force and Break Torque Properties for Joint
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;

        // Assign Object as connected rigidbody
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint.anchor = attachPivot.position;
        joint.connectedAnchor = attachPivot.position;

        attachedHand = hand;
    }

    public void OnRelease() {
        // Reset the Connected Body Property of the Controller fixed joint.
        attachedHand.GetComponent<FixedJoint>().connectedBody = null;
        // Manually Destroy the Fixed Joint
        Destroy(attachedHand.GetComponent<FixedJoint>());
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
