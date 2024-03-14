using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

        // // Translate Grip to Anchor Point.
        hand.transform.position = attachPivot.position;

        // Create Fixed Joint for hand
        FixedJoint joint = hand.AddComponent<FixedJoint>();
        // Assign Break Force and Break Torque Properties for Joint
        joint.breakForce = 100000;
        joint.breakTorque = 100000;

        // Assign Object as connected rigidbody
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint.connectedAnchor = attachPivot.position;

        attachedHand = hand;

        gameObject.layer = 6;
        SetChildLayers(gameObject, 6);
    }

    public void OnRelease() {
        // Reset the Connected Body Property of the Controller fixed joint.
        attachedHand.GetComponent<FixedJoint>().connectedBody = null;
        // // Manually Destroy the Fixed Joint
        Destroy(attachedHand.GetComponent<FixedJoint>());

        gameObject.layer = 9;
        SetChildLayers(gameObject, 9);
    }

    private void SetChildLayers(GameObject obj, int layer) {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.layer = layer;
        }
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
