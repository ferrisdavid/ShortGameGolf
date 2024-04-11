using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallDynamicMovement : MonoBehaviour
{

    // Rigidbody Values.
    [SerializeField]
    private float baseDrag;
    [SerializeField]
    private float baseAngularDrag;

    // Club Physics Modifiers.
    public float clubDragModifier = 1;
    public float clubAngularDragModifier = 1;

    // Environment Modifiers.
    private bool isModifiedPhysics = false;
    [SerializeField]
    private float sandDrag;
    [SerializeField]
    private float sandAngularDrag;

    // Ball Rigidbody
    private Rigidbody ballRB;

    // Audio Source and Clips (Ball Hit Sounds).
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip putClip;
    [SerializeField]
    private AudioClip smackClip;

    // Roll Timer
    private bool startTimer = false;
    private float rollTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Initialize the Balls Drag Physics
        ballRB.drag = baseDrag;
        ballRB.angularDrag = baseAngularDrag;

        // Freeze the ball initially.
        ballRB.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("club")) {
            // Play Hit Audio Based on Collision Force.
            if (collision.impulse.magnitude >= 2) {
                audioSource.PlayOneShot(smackClip);
            } 
            else {
                audioSource.PlayOneShot(putClip);
            }

            // Apply Artifical Gravity for Putter Hits to Prevent Putter from Performing Aerial Shots.
            ConstantForce appliedGravity;
            if (collision.collider.gameObject.transform.parent.gameObject.name == "Putter" && collision.impulse.y > 1 && !TryGetComponent<ConstantForce>(out appliedGravity)) {
                ConstantForce artificalPutterGravity = gameObject.AddComponent<ConstantForce>();
                artificalPutterGravity.force = new Vector3(0, -20.81f, 0);
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

        ConstantForce artificalPutterGravity;
        bool ballInAir = !Physics.Raycast(transform.position, Vector3.down, 0.1f);
        if (!ballInAir && TryGetComponent<ConstantForce>(out artificalPutterGravity)) Destroy(artificalPutterGravity);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer && rollTimer <= 6.0f) {
            rollTimer += Time.deltaTime;
        }
        else {
            rollTimer = 0.0f;
            startTimer = false;
        }
    }

    // Ball Dynamic Drag Functions.
    private void ApplyBaseDrag() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        ballRB.angularDrag = baseAngularDrag * clubAngularDragModifier;
        ballRB.drag = baseDrag * clubDragModifier;

        bool ballOnGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (ballRB.velocity.magnitude < 0.2 && ballOnGround && ballRB.constraints != RigidbodyConstraints.FreezePosition) {
            startTimer = true;
        }
        else {
            startTimer = false;
        }
        
        if (rollTimer >= 6.0f) {
            startTimer = false;
            ballRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void ApplySandDrag() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling (When on Sand)
        ballRB.angularDrag = sandAngularDrag * clubAngularDragModifier;
        ballRB.drag = sandDrag * clubDragModifier;

        bool ballOnGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (ballRB.velocity.magnitude < 0.8 && ballOnGround && ballRB.constraints != RigidbodyConstraints.FreezePosition) {
            startTimer = true;
        }
        else {
            startTimer = false;
        }
        
        if (rollTimer >= 4.0f) {
            startTimer = false;
            ballRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
