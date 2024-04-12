using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameState gameState;

    [SerializeField]
    private GameObject golfCart;

    public bool isTeleportEnabled = false;

    public void ToggleTeleportEnable(bool isEnabled) {
        isTeleportEnabled = isEnabled;

        // Render the Golf Cart/Display Button for Teleportation
        if (!gameState.isWin) {
            golfCart.transform.localRotation = Quaternion.Euler(Vector3.zero);
            golfCart.transform.localPosition = new Vector3(golfCart.transform.localPosition.x, 1.5f, golfCart.transform.localPosition.z);
            golfCart.SetActive(isEnabled);
        }
    }

    public void goNextHole() {
        if (gameState) gameState.LoadNextHole();
    }

    public void quitToMain() {
        if (gameState) gameState.ReturnToClubhouse();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Enable the Golf Cart Travel to Next Hole Interface.
        if (gameState.isWin) {
            if (!golfCart.activeSelf) {
                golfCart.transform.localRotation = Quaternion.Euler(Vector3.zero);
                golfCart.transform.localPosition = new Vector3(golfCart.transform.localPosition.x, 1.5f, golfCart.transform.localPosition.z);
            }

            golfCart.SetActive(true);            
            golfCart.transform.Find("Button").gameObject.SetActive(false);
            golfCart.transform.Find("NextHoleBtn").gameObject.SetActive(true);
        }
    }
}
