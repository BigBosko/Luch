using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzleManager : MonoBehaviour
{
    public static DoorPuzzleManager Instance;
    public GameObject[] allDoors;
    public bool puzzleSolved = false;
    
    
    public bool IsSolved()
    {
        foreach (var door in allDoors)
        {
            if (true)
                return false;
        }
        return true;
    }
}
