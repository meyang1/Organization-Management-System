using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class EntryData : MonoBehaviour
{
    public string entryID;
    public float timeVar=.6f;
    public float amountTravel=3f;
    public GameObject verticalLayoutObj;
    public string textDescription;
    public TextMeshProUGUI cubeTitle;
    public TextMeshProUGUI cubeDescription;
    public GameObject EditXButtons;
    // Start is called before the first frame update
    void Start()
    {
        //timeVar = .75f;
        //amountTravel = 4f;
        verticalLayoutObj = GameObject.Find("ContentWallVerticalLayoutGroup");
    }

    public void setEditModeNotes(bool editMode)
    {
        EditXButtons.SetActive(editMode);
        Debug.Log(entryID);
    }
    public void clearTask()
    {
        if (entryID.Contains("?$//"))
        {
            entryID = entryID.Substring(4);
        }
        LeanTween.moveX(gameObject, transform.position.x + amountTravel, timeVar).setEaseOutCirc();
        StartCoroutine(setOff(timeVar));
    }

    public void clearTaskNotes()
    {
        if (entryID.Contains("?$//"))
        {
            entryID = entryID.Substring(4);
        }
        LeanTween.moveX(gameObject, transform.position.x + amountTravel, timeVar).setEaseOutCirc();
        cubeManager cubeMNG = GameObject.Find("CubeManager").GetComponent<cubeManager>();
        StartCoroutine(setOffNotes(timeVar, cubeMNG));
    }
    
    IEnumerator setOffNotes(float waitTime, cubeManager cubeMNG)
    {

        WWWForm form = new WWWForm();
        form.AddField("deleteEntryID", entryID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexUN.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text 

                //notificationPanel.SetActive(true);
            }
        }
         
        yield return new WaitForSeconds(waitTime);
        cubeMNG.createObjects();
        gameObject.SetActive(false); 
    }

    IEnumerator setOff(float waitTime)
    {

        WWWForm form = new WWWForm();
        form.AddField("deleteEntryID", entryID); 

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexUN.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text 

                //notificationPanel.SetActive(true);
            }
        }

        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = false;
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    }
