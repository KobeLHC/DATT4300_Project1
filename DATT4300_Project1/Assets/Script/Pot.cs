using System.Collections.Generic;
using System.Collections;   
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Pot : MonoBehaviour
{
    public RandomOrder randomOrder;  // drag the RandomOrder object here
    public ItemSlot itemSlot;        // drag the ItemSlot (basket) here
    public RandomPos randomPosIngredients;
    public TextMeshProUGUI cookingHint;
    public bool isCorrect;
    public TextMeshProUGUI endGameHint;
    public int finishedDish;

    [Header("Audio")]
    public AudioSource audioSource;     // drag an AudioSource here
    public AudioClip successClip;       // drag your success sound here
    public AudioClip failClip;          // fail sound

    private void Start()
    {
        finishedDish = 0;
        cookingHint.gameObject.SetActive(false);
        endGameHint.text = "Dish Finished\n" + finishedDish + "/10";
        endGameHint.gameObject.SetActive(true);
    }

    // this method will be called when the basket is released over the pot
    public void HandleBasketDrop()
    {
        if (randomOrder == null || itemSlot == null) return;

        if (CompareCollectedWithRecipe())
        {
            Debug.Log("recipe is correct");

             if (audioSource != null && successClip != null)
            {
                
                audioSource.PlayOneShot(successClip);
            }



            // clear collected items
            itemSlot.collectedItems.Clear();

            // show new recipe
            randomOrder.ShowRandomText();

            //generate random postition for ingredients when correct
            randomPosIngredients.ShuffleChildren();

            isCorrect = true;
            OnHint();

            finishedDish++;
            if(finishedDish == 3)
            {
                SceneManager.LoadScene("Win");
            }

            endGameHint.text = "Dish Finished\n" + finishedDish + "/10";
        }
        else
        {
            Debug.Log("wrong ingredients");

                 if (audioSource != null && failClip != null)
            {
                audioSource.PlayOneShot(failClip);
            }

            isCorrect = false;
            OnHint();

            // clear collected items too
            itemSlot.collectedItems.Clear();
        }
    }

    // Compare basket items to the current recipe
    private bool CompareCollectedWithRecipe()
    {
        List<string> currentIngredients = randomOrder.currentIngredients;
        List<GameObject> collectedItems = itemSlot.collectedItems;

        if (collectedItems.Count != currentIngredients.Count)
            return false;

        // Make a copy to "check off" items
        List<string> ingredientsCopy = new List<string>(currentIngredients);

        foreach (GameObject item in collectedItems)
        {
            string itemName = item.name.Replace("(Clone)", "").Trim();

            if (ingredientsCopy.Contains(itemName))
                ingredientsCopy.Remove(itemName); // matched one
            else
                return false; // wrong ingredient
        }

        return ingredientsCopy.Count == 0; // all matched
    }

    public void OnHint()
    {
        cookingHint.gameObject.SetActive(true);

        if (isCorrect)
        {
            cookingHint.text = "Correct Ingredients\nStart Cooking";
        }

        else
        {
            cookingHint.text = "Wrong Ingredients";
        }
    }

}
