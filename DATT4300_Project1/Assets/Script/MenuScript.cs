using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject settingPanel;

    [Header("Audio")]
    public AudioSource audioSource;   // drag your AudioSource here
    public AudioClip clickSound;      // drag your click sound here

    private void Start()
    {
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void PlayGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("Main");
    }

    public void Setting()
    {
        PlayClickSound();
        settingPanel.SetActive(true);
    }

    public void Credit()
    {
        PlayClickSound();
        creditPanel.SetActive(true);
    }

    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void Resume()
    {
        PlayClickSound();
        settingPanel.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void MainMenu()
    {
        PlayClickSound();
        SceneManager.LoadScene("MainMenu");
    }
}