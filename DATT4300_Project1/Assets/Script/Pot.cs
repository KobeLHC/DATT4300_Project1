using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public RandomOrder randomOrder;  // drag the RandomOrder object here
    public ItemSlot itemSlot;        // drag the ItemSlot (basket) here

    // this method will be called when the basket is released over the pot
    public void HandleBasketDrop()
    {
        if (randomOrder == null || itemSlot == null) return;

        if (CompareCollectedWithRecipe())
        {
            Debug.Log("recipe is correct");

            // clear collected items
            itemSlot.collectedItems.Clear();

            // show new recipe
            randomOrder.ShowRandomText();
        }
        else
        {
            Debug.Log("wrong ingredients");

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
}
