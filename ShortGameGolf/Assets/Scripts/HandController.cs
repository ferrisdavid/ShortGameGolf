using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandController : MonoBehaviour
{
    // Reference to the Controller Action 
    public SteamVR_Action_Boolean GrabTrigger;
    // Reference to the Controller Hand Input
    public SteamVR_Input_Sources handType;
    // Reference to the Current Hand Pose for Tracking Velocity and Position
    public SteamVR_Behaviour_Pose handPose;

    // Store a reference to the object that we are currently holding
    private GameObject heldObject;
    // Store a reference to the object our hand trigger collider is currently intersecting
    private GameObject grabableObject;


    // Start is called before the first frame update
    void Start()
    {
        GrabTrigger.AddOnStateDownListener(TriggerDown, handType);
        GrabTrigger.AddOnStateUpListener(TriggerUp, handType);
    }

    /*******************************
     Trigger Event Handlers
    ********************************/
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (grabableObject)
        {
            GrabObject();
        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (heldObject)
        {
            ReleaseObject();
        }
    }

    /*****************************************
      Hand Trigger Collider Override Methods
    ******************************************/

    // Collider Trigger Enter Method - Detect when our hand trigger collider intersects a collider and attempt to update the Grabable Object reference
    public void OnTriggerEnter(Collider other)
    {
        UpdateInteractableObject(other);
    }

    // Collider Trigger Stay Method - Runs once per frame and ensures that controller hovering over an object for an extended period of time assigns the object as grabbable if it meets the necessary conditions
    public void OnTriggerStay(Collider other)
    {
        UpdateInteractableObject(other);
    }

    // Collider Trigger Exit Method - Reset Grabable Object on Trigger Exit
    public void OnTriggerExit(Collider other)
    {
        // Reset the Grabable Object
        grabableObject = null;
    }

    /****************************************************
      Object Manipulation Reference Updaters/Actions
    *****************************************************/
    
    // Update The Current Grabable Object when Trigger Enter Collider and Object has a rigidbody
    private void UpdateInteractableObject(Collider intersectedCollider)
    {
        // Only update the reference for the potential object to grab if we do not have a current grabable object and the object has a rigidbody
        if (!grabableObject)
        {
            grabableObject = intersectedCollider.gameObject;
        }
    }

    // Attach the Grabable Object to the Controller and Assign the held object reference.
    private void GrabObject()
    {
        // Execute OnGrab function for the object if it is Grabbable
        Grabbable grabObject;
        if (grabableObject.TryGetComponent<Grabbable>(out grabObject)) {
            grabObject.OnGrab(gameObject);
        }

        // Execute OnBeginInteraction function for the object if it is Interactable
        Interactable interactableObject;
        if (grabableObject.TryGetComponent<Interactable>(out interactableObject)) {
            interactableObject.OnBeginInteraction(gameObject.transform);
        }

        // // Assign the Held Object Reference to the Currently Intersected (grabable) Object and Reset the grabbable object
        heldObject = grabableObject;
        grabableObject = null;
    }

    // On Joint Break Method Override - Called when the Controller Joint Breaks due to a force (this automatically deletes the joint) - Clean up the Object References.
    public void OnJointBreak(float breakForce) {
        heldObject = null;
    }

    // Dettach the Currently Held Object from the Controller Fixed Joint when the User Releases the Trigger
    private void ReleaseObject()
    {
        if (!heldObject) return;

        // Execute OnRelease function for the object if it is Grabbable
        Grabbable grabObject;
        if (heldObject.TryGetComponent<Grabbable>(out grabObject)) {
            grabObject.OnRelease();

            // Assign the last known velocity of the hand controller to the released held object
            heldObject.GetComponent<Rigidbody>().velocity = handPose.GetVelocity();
            heldObject.GetComponent<Rigidbody>().angularVelocity = handPose.GetAngularVelocity();
        }

        // Execute OnEndInteraction function for the object if it is Interactable
        Interactable interactableObject;
        if (heldObject.TryGetComponent<Interactable>(out interactableObject)) {
            interactableObject.OnEndInteraction();
        }

        heldObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
