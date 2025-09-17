using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject settingPanel;

    private void Start()
    {
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Setting()
    {
        settingPanel.SetActive(true);
    }

    public void Credit()
    {
        creditPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        settingPanel.SetActive(false);
        creditPanel.SetActive(false);
    }
}
