using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubBallDistanceDetector : MonoBehaviour
{

    // Reference to Putter Club Face.
    [SerializeField]
    private Transform putterClubFace;
    // Reference to Putter Club Face.
    [SerializeField]
    private Transform chipperClubFace;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check the Distance to the ball for each club face and remove any rigidbody constraints on the ball used to keep it in place for the current shot
        float putterDistanceToBall = (transform.position - putterClubFace.position).magnitude;
        float chipperDistanceToBall = (transform.position - chipperClubFace.position).magnitude;
        if (putterDistanceToBall < 0.1 || chipperDistanceToBall < 0.1) {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
