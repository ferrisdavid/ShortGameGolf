using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO BE REMOVED
public class TeleportDebug : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isTeleportEnabled) {
            gameObject.GetComponent<Material>().color = Color.green;
        } 
        else {
            gameObject.GetComponent<Material>().color = Color.red;
        }
    }
}
