using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject golfCart;

    public bool isTeleportEnabled = false;

    public void ToggleTeleportEnable(bool isEnabled) {
        isTeleportEnabled = isEnabled;

        // Render the Golf Cart/Display Button for Teleportation
        golfCart.transform.localRotation = Quaternion.Euler(Vector3.zero);
        golfCart.transform.localPosition = new Vector3(golfCart.transform.localPosition.x, 1.5f, golfCart.transform.localPosition.z);
        golfCart.SetActive(isEnabled);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
