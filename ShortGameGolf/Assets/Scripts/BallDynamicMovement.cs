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

    // Ball Rigidbody
    private Rigidbody ballRB;


    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody>();

        // Initialize the Balls Drag Physics
        ballRB.drag = baseDrag;
        ballRB.angularDrag = baseAngularDrag;

        // Freeze the ball initially.
        ballRB.constraints = RigidbodyConstraints.FreezePosition;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("club") && ballRB.constraints == RigidbodyConstraints.FreezePosition) {
            ballRB.constraints = RigidbodyConstraints.None;
            ballRB.AddForce(collision.impulse);
        }
    }

    private void FixedUpdate() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        if (ballRB.velocity.magnitude <= Mathf.Epsilon) {
            ballRB.angularDrag = baseAngularDrag;
        }
        else if (ballRB.velocity.magnitude <= 0.3) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 20, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 1) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 1, dragIncrement);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
