using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // Current Number of Strokes for the Game.
    [SerializeField]
    private int strokeCount = 0;


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
        strokeCount++;
    }

    public void DecrementStrokes() {
        strokeCount--;
    }
}
