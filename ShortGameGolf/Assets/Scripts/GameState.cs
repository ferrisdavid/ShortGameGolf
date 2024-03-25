using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Win State.
    public bool isWin = false;

    // Current Number of Strokes for the Game.
    [SerializeField]
    private int strokeCount = 0;

    private int freeStrokes = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
