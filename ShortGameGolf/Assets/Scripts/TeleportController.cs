using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TeleportController : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Boolean Teleport;
    // a reference to the hand
    public SteamVR_Input_Sources handType;

    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject rig;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        
        Teleport.AddOnStateDownListener(onTeleportRequest, handType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onTeleportRequest(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (player.isTeleportEnabled)
        {
            // Fade Screen to black on successful Teleport Request before moving
            SteamVR_Fade.View(Color.black, 1.0f);
            Vector3 difference = rig.transform.position - cam.transform.position;
            // Start Fade Co routine which handles player movement and fade in
            StartCoroutine(TeleportFade(player.teleportPoint, difference));
        }
    }

    private void handleTeleport(Vector3 teleportPoint, Vector3 camPlayerDiff)
    {
        player.transform.position = new Vector3(camPlayerDiff.x + teleportPoint.x, player.transform.position.y, camPlayerDiff.z + teleportPoint.z);
    }

    IEnumerator TeleportFade(Vector3 teleportPoint, Vector3 camPlayerDiff)
    {
        yield return new WaitForSeconds(1.1f);
        handleTeleport(teleportPoint, camPlayerDiff);
        SteamVR_Fade.View(Color.clear, 1.0f);
    }
}
