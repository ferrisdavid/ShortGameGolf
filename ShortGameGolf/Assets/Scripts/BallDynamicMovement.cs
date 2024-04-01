using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDynamicMovement : MonoBehaviour
{

    // Rigidbody Values.
    [SerializeField]
    private float baseDrag;
    [SerializeField]
    private float baseAngularDrag;

    [SerializeField]
    private float dragIncrement;

    // Environment Modifiers.
    private bool isModifiedPhysics = false;
    [SerializeField]
    private float sandDrag;
    [SerializeField]
    private float sandAngularDrag;

    // Ball Rigidbody
    private Rigidbody ballRB;

    // Audio Source and Clips (Ball Hit Sounds).
    private AudioSource audio;
    [SerializeField]
    private AudioClip putClip;
    [SerializeField]
    private AudioClip smackClip;


    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();

        // Initialize the Balls Drag Physics
        ballRB.drag = baseDrag;
        ballRB.angularDrag = baseAngularDrag;

        // Freeze the ball initially.
        ballRB.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("club")) {
            if (ballRB.constraints == RigidbodyConstraints.FreezePosition) {
                ballRB.constraints = RigidbodyConstraints.None;
            }

            // Play Hit Audio Based on Collision Force.
            if (collision.impulse.magnitude >= 2) {
                audio.PlayOneShot(smackClip);
            } 
            else {
                audio.PlayOneShot(putClip);
            } 
        }

        // Rest Ground Environment Physics Modifiers.
        if (collision.collider.gameObject.CompareTag("sand")) {
            isModifiedPhysics = true;
        }
        else if (!collision.collider.gameObject.CompareTag("club")) {
            isModifiedPhysics = false;
        }
    }

    private void FixedUpdate() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        if (!isModifiedPhysics) ApplyBaseDrag();
        else ApplySandDrag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ball Dynamic Drag Functions.
    private void ApplyBaseDrag() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        if (ballRB.velocity.magnitude <= Mathf.Epsilon) {
            ballRB.angularDrag = baseAngularDrag;
        }
        else if (ballRB.velocity.magnitude <= 0.08) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 45, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 0.3) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 20, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 1) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 1, dragIncrement);
        }
    }

    private void ApplySandDrag() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling (When on Sand)
        if (ballRB.velocity.magnitude <= Mathf.Epsilon) {
            ballRB.angularDrag = sandAngularDrag;
            ballRB.drag = sandDrag;
        }
        else if (ballRB.velocity.magnitude <= 0.1) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 45, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 0.3) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 25, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 1) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 3, dragIncrement);
        }
    }
}
