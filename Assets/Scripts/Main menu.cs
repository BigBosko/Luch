using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
