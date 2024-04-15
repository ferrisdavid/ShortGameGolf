using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePadController : MonoBehaviour, Grabbable
{
    // Reference to GameState.
    private GameState gameState;

    [SerializeField]
    private Transform restingSnapPoint;

    private GameObject attachedHand;
    [SerializeField]
    private Transform leftAttachPivot;
    [SerializeField]
    private Transform rightAttachPivot;
    

    // Text Label Object References.
    [SerializeField]
    private TextMeshPro holeLabel;
    [SerializeField]
    private TextMeshPro parLabel;
    [SerializeField]
    private TextMeshPro currentStrokesLabel;
    [SerializeField]
    private TextMeshPro activePickupLabel;
    [SerializeField]
    private TextMeshPro[] finalScoreLabels; 

    public void OnGrab(GameObject hand) {
        hand.transform.position = hand.CompareTag("right") ? rightAttachPivot.position : leftAttachPivot.position;
        // Create Fixed Joint for hand
        FixedJoint joint = hand.AddComponent<FixedJoint>();
        // Assign Break Force and Break Torque Properties for Joint
        joint.breakForce = 100000;
        joint.breakTorque = 100000;

        // Assign Object as connected rigidbody
        joint.connectedBody = gameObject.GetComponent<Rigidbody>();
        joint.connectedAnchor = hand.CompareTag("right") ? rightAttachPivot.position : leftAttachPivot.position;

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
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        // Set Static Score Card Labels.
        SetCourseHoleLabel(gameState.holeNumber);
        SetHoleParLabel(gameState.holePar);
        UpdateFinalHoleScores();
    }

    // Update is called once per frame
    void Update()
    {
        // Fix Position of Card to Bag when not held.
        if (!attachedHand) {
            transform.position = restingSnapPoint.position;
            transform.localRotation = Quaternion.AngleAxis(90.0f, Vector3.right);
        }

        // Updaters for Score Card Values.
        SetHoleStrokeCount(gameState.strokeCount);

        if (gameState.isWin) UpdateFinalHoleScores();
    }

    // Text Update Methods.
    public void SetCourseHoleLabel(int holeNumber) {
        holeLabel.text = "Hole: " + holeNumber.ToString();
    }

    public void SetHoleParLabel(int par) {
        parLabel.text = "Par: " + par.ToString();
    }

    public void SetHoleStrokeCount(int numStrokes) {
        currentStrokesLabel.text = "Strokes: " + numStrokes.ToString();
    }

    public void SetActivePickupLabel(string pickupName) {
        activePickupLabel.text = pickupName;
    }

    public void UpdateFinalHoleScores() {
        for (int i = 0; i < gameState.holeScores.Length; i++) {
            finalScoreLabels[i].text =  gameState.holeScores[i].ToString();
        }
    }
}
