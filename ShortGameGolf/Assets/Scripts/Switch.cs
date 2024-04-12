using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class Switch : MonoBehaviour, Interactable
{
    private Transform attachedHand;
    private Vector3 handleLocalStartPos;
    private Vector3 handRelativeToHandle;
    private Vector2 offsetOnGrab;
    [SerializeField]
    private Vector2 handleMinMaxRotation;
    [SerializeField]
    private UnityEvent onSwitchOn;
    [SerializeField]
    private UnityEvent onSwitchOff;


    public void OnBeginInteraction(Transform hand)
    {
        attachedHand = hand;

        // Perform Haptic Vibration
        SteamVR_Input_Sources leftOrRight = attachedHand.CompareTag("left") ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand;
        SteamVR_Actions.default_Haptic[leftOrRight].Execute(0, 1, 2, 0.4f);

        handleLocalStartPos = transform.localPosition;
        handRelativeToHandle = transform.parent.InverseTransformPoint(hand.position);

        offsetOnGrab = new Vector2(handRelativeToHandle.x, handRelativeToHandle.z);
    }

    public void OnEndInteraction()
    {
        // Reset Hand References.
        attachedHand = null;
    }

    public void updateToHand() {
        float newHandPositionZ = transform.parent.InverseTransformPoint(attachedHand.position).z;
        float handMovement = newHandPositionZ - offsetOnGrab.y;

        float newAngle = Mathf.Clamp((handleLocalStartPos.z + handMovement)*200, handleMinMaxRotation.x, handleMinMaxRotation.y);

        transform.localRotation = Quaternion.AngleAxis(newAngle, transform.InverseTransformDirection(transform.right));

        // Execute Switch Functionality.
        if (newAngle >= 15) {
            onSwitchOn.Invoke();
        }
        else if (newAngle <= -15) {
            onSwitchOff.Invoke();
        }

        if (Mathf.Abs(newAngle) > 0.5)
        {
            // Perform Haptic Vibration
            SteamVR_Input_Sources hand = attachedHand.CompareTag("left") ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand;
            SteamVR_Actions.default_Haptic[hand].Execute(0, 1, 2, 0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         if (attachedHand) {
            updateToHand();
        }
    }
}
