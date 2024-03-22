using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickup : MonoBehaviour
{
    [SerializeField]
    private float maxHeightOffset;

    private float originalY;

    // Start is called before the first frame update
    void Start()
    {
        originalY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle Pickup Rotation.
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        // Handle Pickup Vertical Oscillation.
        if (transform.position.y == originalY + maxHeightOffset) {
            maxHeightOffset = -maxHeightOffset;
        }

        Vector3 targetPosition = new Vector3(transform.position.x, originalY + maxHeightOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.2f);
    }
}
