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
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
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
    }

    public void ResumeMenu()
    {
        settingMenu.SetActive(false);
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
