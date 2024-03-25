using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubGrab : MonoBehaviour, Grabbable
{
    [SerializeField]
    private Transform attachPivot;
    private GameObject attachedHand;
    private GameObject detachedHand;

    [SerializeField]
    private Transform releasedSnapPoint;



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

        // Hide the Inactive Hand.
        string otherhandTag = attachedHand.CompareTag("left") ? "right" : "left";
        GameObject otherHand = GameObject.FindGameObjectWithTag(otherhandTag);
        detachedHand = otherHand;
        otherHand.SetActive(false);

        // Dynamic Physic Layer to Prevent Collision with Non ball objects when held
        gameObject.layer = 6;
        SetChildLayers(gameObject, 6);
    }

    public void OnRelease() {
        // Reset the Connected Body Property of the Controller fixed joint.
        attachedHand.GetComponent<FixedJoint>().connectedBody = null;
        // // Manually Destroy the Fixed Joint
        Destroy(attachedHand.GetComponent<FixedJoint>());
        
        // Reset Hand References.
        attachedHand = null;
        detachedHand.SetActive(true);
        detachedHand = null;

        // Dynamic Physic Layer to Enable Collision with Non ball objects when released
        gameObject.layer = 9;
        SetChildLayers(gameObject, 9);

        transform.position = releasedSnapPoint.position;
    }

    public GameObject GetAttachedHand() {
        return attachedHand;
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
