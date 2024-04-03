using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubBallDistanceDetector : MonoBehaviour
{
    // Reference to Golf Ball.
    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("GolfBall");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToBall = (ball.transform.position - transform.position).magnitude;
        if (distanceToBall < 0.1) {
            ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
