using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Button : MonoBehaviour, Interactable
{
    // Reference to Player Object.
    [SerializeField]
    private PlayerController player;

    // Compression Tolerance before button activation
    [SerializeField]
    private float compressionThreshold;

    private Transform attachedHand;
    private Vector3 buttonLocalStartPos;
    private Vector3 handRelativeToButton;

    private float offsetOnEnter;

    [SerializeField]
    private float buttonMaxCompression;
    private bool isFromRest = true;

    // Reference to Button Mesh
    private MeshRenderer buttonMesh;

    // Button Highlight/Active State
    [SerializeField]
    private Color inactiveColor;
    [SerializeField]
    private Color activeColor;
    [SerializeField]
    private Color highlightColor;

    public void OnBeginInteraction(Transform hand) { }
    public void OnEndInteraction() { }

    
    // Start is called before the first frame update
    void Start()
    {
        buttonLocalStartPos = transform.localPosition;
        buttonMesh = GetComponent<MeshRenderer>();
        buttonMesh.materials[0].color = inactiveColor;
        buttonMesh.materials[1].SetColor("g_vOutlineColor", Color.clear);
    }

    // Update is called once per frame
    void Update()
    {
        if (!attachedHand)
        {
            // Lerp Button Compression back to original rest
            transform.localPosition = Vector3.Lerp(transform.localPosition, buttonLocalStartPos, 0.2f);
        }

        if (player.isTeleportEnabled) {
            FlashActiveHighlight();
        }
        else {
            buttonMesh.materials[0].color = inactiveColor;
            buttonMesh.materials[1].SetColor("g_vOutlineColor", Color.clear); 
        }
    }

    public void OnHoverEnter(Transform hand) {
        attachedHand = hand;

        handRelativeToButton = transform.parent.InverseTransformPoint(hand.position);
        offsetOnEnter = handRelativeToButton.y;
    }

    public void OnHoverExit(Transform hand) {
        attachedHand = null;
        isFromRest = true;
    }

    public void OnHoverStay(Transform hand) {
        if (attachedHand)
        {
            float newHandPositionY = transform.parent.InverseTransformPoint(attachedHand.position).y;
            float handMovement = newHandPositionY - offsetOnEnter;

            float compression = Mathf.Clamp(buttonLocalStartPos.y + handMovement, -buttonMaxCompression, 0.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, buttonLocalStartPos.y + compression, transform.localPosition.z);
            
            // Apply Impulse force to selected object on button compression
            if (Mathf.Abs(compression) > compressionThreshold && isFromRest) {
                // Execute Teleport Action.
                if (player.isTeleportEnabled) player.GetComponent<TeleportController>().onTeleportRequest();

                isFromRest = false;

                // Perform Haptic Vibration
                SteamVR_Input_Sources rightOrLeft = attachedHand.CompareTag("left") ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand;
                SteamVR_Actions.default_Haptic[rightOrLeft].Execute(0, 1, 2, 0.4f);
            }
        }
    }

    private void FlashActiveHighlight() {

        if (buttonMesh.materials[0].color != activeColor) {
            buttonMesh.materials[0].color = Color.Lerp(buttonMesh.materials[0].color, activeColor, 0.1f);
            buttonMesh.materials[1].SetColor("g_vOutlineColor", Color.Lerp(buttonMesh.materials[1].GetColor("g_vOutlineColor"), highlightColor, 0.1f)); 
        }
        else {
            buttonMesh.materials[0].color = Color.Lerp(buttonMesh.materials[0].color, inactiveColor, 0.1f);
            buttonMesh.materials[1].SetColor("g_vOutlineColor", Color.Lerp(buttonMesh.materials[1].GetColor("g_vOutlineColor"), Color.clear, 0.1f)); 
        }

    }
}
