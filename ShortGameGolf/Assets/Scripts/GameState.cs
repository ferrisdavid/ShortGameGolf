using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GameState : MonoBehaviour
{
    public static GameState Instance; 

    // Course Per Level Information.
    [SerializeField]
    private string[] holeNames;
    [SerializeField]
    private int[] holePars;

    // Global Hole Info.
    private bool isMenu = true;
    public int holeNumber;
    public int holePar;

    // Hole Win State.
    public bool isWin = false;

    // Current Number of Strokes for the Game.
    public int strokeCount = 0;
    private int freeStrokes = 0;

    // Course Hole Scores.
    public int[] holeScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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

    public void SetHoleFinalScore() {
        if (!isMenu) holeScores[holeNumber - 1] = strokeCount;
    }

    public void LoadNextHole() {
        if (isWin && holeNumber != holeScores.Length) {
            LoadScene(holeNames[holeNumber]);
        }
    }

    private void ResetHoleState(bool isMainMenu, int holeNumber, int holePar) {
        isWin = false;

        this.holeNumber = holeNumber;
        this.holePar = holePar;
        
        strokeCount = 0;
        freeStrokes = 0;

        isMenu = isMainMenu;
    }

    // System Control Functions.
    public void LoadScene(string scene) {
        SteamVR_LoadLevel.Begin(scene);
        ResetHoleState(false, holeNumber + 1, holePars[holeNumber]);
    }

    public void QuitGame() {
        // Quit Built Application.
        Application.Quit();
        // Quit Play Mode in Editor.
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ReturnToClubhouse() {
        ResetHoleState(true, 0, 0);

        SteamVR_LoadLevel.Begin("ClubHouse");
    }
}


