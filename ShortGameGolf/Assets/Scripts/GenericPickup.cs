using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericPickup : MonoBehaviour
{
    [SerializeField]
    private float maxHeightOffset;

    [SerializeField]
    private float spinSpeed;

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
        transform.Rotate(Vector3.up * Time.deltaTime * spinSpeed);

        // Handle Pickup Vertical Oscillation.
        if (Mathf.Abs(transform.position.y - (originalY + maxHeightOffset)) <= 0.01) {
            maxHeightOffset = -maxHeightOffset;
        }
        float clampedHeight = maxHeightOffset > 0 ? Mathf.Clamp(transform.position.y + 0.01f, transform.position.y, originalY + maxHeightOffset) : Mathf.Clamp(transform.position.y - 0.01f, originalY + maxHeightOffset, transform.position.y);

        Vector3 targetPosition = new Vector3(transform.position.x, clampedHeight, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
    }
}
