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
    // Start is called before the first frame update
    void Start()
    {
        //timeVar = .75f;
        //amountTravel = 4f;
        verticalLayoutObj = GameObject.Find("ContentWallVerticalLayoutGroup");
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

    IEnumerator setOff(float waitTime)
    {
        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = false;

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
        gameObject.SetActive(false);
        verticalLayoutObj.GetComponent<VerticalLayoutGroup>().enabled = true;
    }

    }
