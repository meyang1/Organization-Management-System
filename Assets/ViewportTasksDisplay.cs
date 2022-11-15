using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportTasksDisplay : MonoBehaviour
{
    public GameObject EntryWithDescriptionPrefab; //height = 60
    public GameObject EntryNoDescriptionPrefab; //height = 80

    public double currentHeight = 300;
    public GameObject allEntries; // increase Y value with currentHeight

    float currentMoveAmount = 0f;
    public void CreateNewEntry(int type)
    {
        if(type == 1){
            // Entry with description
            var entryObject = (Object)Instantiate(EntryWithDescriptionPrefab, Vector3.zero, Quaternion.identity, allEntries.transform);
            currentMoveAmount = 60f;
            entryObject.name = "New Entry WIth Descrition";
        }
        if(type == 2){
            // Entry without description
            var entryObject = (Object)Instantiate(EntryNoDescriptionPrefab, Vector3.zero, Quaternion.identity, allEntries.transform);
            currentMoveAmount = 80f;
            entryObject.name = "New Entry no Description";
        }
        currentHeight += currentMoveAmount;
        //allEntries.GetComponent<RectTransform>();
    
        allEntries.transform.localscale.y = currentMoveAmount;
        LeanTween.moveY(entryobject, 0f-currentMoveAmount, 0.75f).setEaseOutCubic();
    }


}
