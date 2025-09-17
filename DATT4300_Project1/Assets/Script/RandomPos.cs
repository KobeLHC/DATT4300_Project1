using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{ 
    void Start()
    {
        ShuffleChildren();
    }

    public void ShuffleChildren()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            // pick a random index
            int randomIndex = Random.Range(0, childCount);
            // set sibling index to that random position
            child.SetSiblingIndex(randomIndex);
        }
    }
}
