using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused;
    public GameObject settingMenu;
    //public GameObject recipeMenu;
    //public bool isShowed;
    public GameObject wok;
    public GameObject basket;
    public GameObject flashlight;
    public GameObject timer;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingMenu.SetActive(false);
        //recipeMenu.SetActive(true);
        //isShowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }

            else
            {
                PauseGame();
            }
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    if (isShowed)
        //    {
        //        OffRecipeMenu();
        //    }

        //    else
        //    {
        //        OnRecipeMenu();
        //    }
        //}
    }

    public void PauseGame()
    {
        wok.SetActive(false);
        basket.SetActive(false);
        flashlight.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        flashlight.SetActive(true);
        basket.SetActive(true);
        wok.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        timer.SetActive(false);
    }

    public void ResumeMenu()
    {
        settingMenu.SetActive(false);
        timer.SetActive(true);
    }

    //public void OnRecipeMenu()
    //{
    //    recipeMenu.SetActive(true);
    //    isShowed = true;
    //}

    //public void OffRecipeMenu()
    //{
    //    recipeMenu.SetActive(false);
    //    isShowed = false;
    //}
}
