using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomOrder : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public string[] randomTexts;         

    void Start()
    {
        ShowRandomText();
    }

    public void ShowRandomText()
    {
        if (randomTexts.Length == 0 || tmpText == null) return;

        int randomOrderIndex = Random.Range(0, randomTexts.Length);

        tmpText.text = randomTexts[randomOrderIndex].Replace("\\n", "\n");
    }
}
