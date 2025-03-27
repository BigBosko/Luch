using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.AI;
using Unity.AI.Navigation;
public class GameManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}

