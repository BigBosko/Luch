using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public Light[] allLights;
    public bool puzzleSolved = false;
    
    
    public bool IsSolved()
    {
        foreach (var light in allLights)
        {
            if (light.GetComponent<StaticLight>())
                return false;
        }
        return true;
    }
}
