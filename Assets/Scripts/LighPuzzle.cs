using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public List<LightController> allLights;
    public bool puzzleSolved = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CheckPuzzleState()
    {
        foreach (var light in allLights)
        {
            if (light.isOn)
            {
                return; // At least one light is still on, puzzle not solved
            }
        }

        puzzleSolved = true;
        Debug.Log("Puzzle Solved!");
    }
}
