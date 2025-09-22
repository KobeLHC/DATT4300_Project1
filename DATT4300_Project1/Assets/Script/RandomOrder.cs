using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomOrder : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public string[] randomTexts;

    // store the current ingredients
    public List<string> currentIngredients = new List<string>();

    void Start()
    {
        ShowRandomText();
    }

    public void ShowRandomText()
    {
        if (randomTexts.Length == 0 || tmpText == null) return;

        // pick a random recipe
        int randomOrderIndex = Random.Range(0, randomTexts.Length);
        string recipeText = randomTexts[randomOrderIndex];

       
        recipeText = recipeText.Replace("\\n", "\n");

        // display 
        tmpText.text = recipeText;

        // parse ingredients
        ExtractIngredients(recipeText);
    }

    private void ExtractIngredients(string recipeText)
    {
        currentIngredients.Clear();

        // split by real newlines
        string[] lines = recipeText.Split('\n');

        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            // Ingredient lines start with "- "
            if (trimmed.StartsWith("- "))
            {
                string ingredient = trimmed.Substring(2).Trim();
                if (!string.IsNullOrEmpty(ingredient))
                {
                    currentIngredients.Add(ingredient);
                }
            }
        }

        // showing whats parsed in console
        Debug.Log("Ingredients: " + string.Join(", ", currentIngredients));
    }
}
