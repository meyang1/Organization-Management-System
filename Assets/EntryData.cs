using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryData : MonoBehaviour
{
    public string entryID;
    public float timeVar;
    public GameObject verticalLayoutObj;
    // Start is called before the first frame update
    void Start()
    {
        timeVar = .75f;
        verticalLayoutObj = GameObject.Find("ContentWallVerticalLayoutGroup");
    }
    public void clearTask()
    {
        LeanTween.moveX(gameObject, transform.position.x + 4f, timeVar).setEaseOutCirc();
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
