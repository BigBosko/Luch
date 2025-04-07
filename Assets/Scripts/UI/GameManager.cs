using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.AI.Navigation;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;

    [SerializeField] private Transform startPosition;

    private void Awake()
    {
        MovePlayerToStart();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void MovePlayerToStart()
    {
        player.transform.position = new Vector3(startPosition.position.x, 1.8f, startPosition.position.z);
    }

}

