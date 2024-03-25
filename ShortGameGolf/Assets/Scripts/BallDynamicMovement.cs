using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDynamicMovement : MonoBehaviour
{
    // Initial Freeze State.
    private bool isFrozen = true;

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

        // TODO; Determine how to unfreeze the ball after the club comes in close contact with the ball.
    }

    private void FixedUpdate() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        if (ballRB.velocity.magnitude <= Mathf.Epsilon) {
            ballRB.angularDrag = baseAngularDrag;
        }
        else if (ballRB.velocity.magnitude <= 0.3) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 25, dragIncrement*100);
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
