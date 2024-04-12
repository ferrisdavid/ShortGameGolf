using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameState : MonoBehaviour
{
    // Global Course Info.
    public int holeNumber;
    public int holePar;

    // Hole Win State.
    public bool isWin = false;

    // Current Number of Strokes for the Game.
    public int strokeCount = 0;
    private int freeStrokes = 0;

    // Course Hole Scores.
    public int[] holeScores;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteamVR_Actions.Golf.Activate();
    }

    // Game State Access and Updater Methods.
    public void IncrementStrokes() {
        if (freeStrokes > 0) {
            freeStrokes--;
            return;
        }

        strokeCount++;
    }

    public void DecrementStrokes() {
        strokeCount--;
    }

    public void AddFreeStroke() {
        freeStrokes++;
    }

    // System Control Functions.
    public void LoadScene(string scene) {
        SteamVR_LoadLevel.Begin(scene);
    }

    public void QuitGame() {
        // Quit Built Application.
        Application.Quit();
        // Quit Play Mode in Editor.
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReturnToClubhouse() {
        SteamVR_LoadLevel.Begin("ClubHouse");
    }
}


