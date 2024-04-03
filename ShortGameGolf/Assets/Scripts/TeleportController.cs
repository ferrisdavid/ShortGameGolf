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

    // Teleportable Objects
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject rig;

    // State Managers and Helpers.
    private PlayerController PlayerManager;
    [SerializeField]
    private float teleportOffset;
    [SerializeField]
    private Transform ball;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onTeleportRequest()
    {
        if (PlayerManager.isTeleportEnabled)
        {
            // Fade Screen to black on successful Teleport Request before moving
            SteamVR_Fade.View(Color.black, 1.0f);
            Vector3 difference = rig.transform.position - cam.transform.position;
            Vector3 teleportPoint = ball.position;
            // Start Fade Co routine which handles player movement and fade in
            StartCoroutine(TeleportFade(teleportPoint, difference));
        }
    }

    private void handleTeleport(Vector3 teleportPoint, Vector3 camPlayerDiff)
    {
        // Teleport Player.
        rig.transform.position = new Vector3(camPlayerDiff.x + teleportPoint.x, ball.position.y, camPlayerDiff.z + teleportPoint.z);
        // Get Look Vector to Hole.
        Vector3 lookToHole = GameObject.Find("CourseHole").transform.position - rig.transform.position;
        lookToHole.y = 0.0f;

        // Rotate Player to face the hole
        rig.transform.localRotation = Quaternion.LookRotation(lookToHole);
    }

    IEnumerator TeleportFade(Vector3 teleportPoint, Vector3 camPlayerDiff)
    {
        yield return new WaitForSeconds(1.1f);
        handleTeleport(teleportPoint, camPlayerDiff);
        SteamVR_Fade.View(Color.clear, 1.0f);
    }
}
