using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public GameObject[] screens;
    private int currentIndex = 1; // starting screen (middle screen cooking area)

    void Start()
    {
        ShowScreen(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentIndex < screens.Length - 1)
            {
                currentIndex++;
                ShowScreen(currentIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowScreen(currentIndex);
            }
        }
    }

    void ShowScreen(int index)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(i == index);
        }
    }
}