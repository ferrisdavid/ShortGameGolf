using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDynamicMovement : MonoBehaviour
{
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

        ballRB.drag = baseDrag;
        ballRB.angularDrag = baseAngularDrag;
    }

    private void FixedUpdate() {
        // Handle Dynamic Drag Forces to Prevent infinite rolling
        if (ballRB.velocity.magnitude == 0) {
            ballRB.drag = baseDrag;
            ballRB.angularDrag = baseAngularDrag;
        }
        else if (ballRB.velocity.magnitude <= 0.3) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 25, dragIncrement*100);
            ballRB.drag = Mathf.Lerp(ballRB.drag, 25, dragIncrement*100);
        }
        else if (ballRB.velocity.magnitude <= 1) {
            ballRB.angularDrag = Mathf.Lerp(ballRB.angularDrag, 1, dragIncrement);
            ballRB.drag = Mathf.Lerp(ballRB.drag, 1, dragIncrement);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
