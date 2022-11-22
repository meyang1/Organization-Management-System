using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryData : MonoBehaviour
{
    public string entryID;
    public float timeVar=.6f;
    public float amountTravel=3f;
    public GameObject verticalLayoutObj;
    // Start is called before the first frame update
    void Start()
    {
        //timeVar = .75f;
        //amountTravel = 4f;
        verticalLayoutObj = GameObject.Find("ContentWallVerticalLayoutGroup");
    }
    public void clearTask()
    {
        LeanTween.moveX(gameObject, transform.position.x + amountTravel, timeVar).setEaseOutCirc();
        StartCoroutine(setOff(timeVar));
    }

    IEnumerator setOff(float waitTime)
    {
        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    }
