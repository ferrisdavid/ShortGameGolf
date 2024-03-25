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
