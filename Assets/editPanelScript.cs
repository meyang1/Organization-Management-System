using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class editPanelScript : MonoBehaviour
{
    public string entryID;
    public string entryName;
    public string entryDescription;
    public TMP_InputField editTextName;
    public TMP_InputField editTextDescription;
    public GameObject editPanel;
    public WebRequest webRequest;
    public ContentWall contentWall;
    public ViewportTasksDisplay viewportDisplay;


    public void editTaskNotes()
    {
        StartCoroutine(editOffNotes());
    }

    IEnumerator editOffNotes()
    {

        WWWForm form = new WWWForm();
        form.AddField("editID", entryID);
        webRequest = GameObject.Find("WebRequest").GetComponent<WebRequest>();
        form.AddField("user", webRequest.sessionUsername);
        //Debug.Log("|" + webRequest.sessionUsername + "|");
        //form.AddField("editName", GameObject.Find("EditPanelName").GetComponent<TMP_InputField>().text);
        //form.AddField("editDescription", GameObject.Find("EditPanelDescription").GetComponent<TMP_InputField>().text);
        form.AddField("editName", editTextName.text);
        form.AddField("editDescription", editTextDescription.text);
        form.AddField("editType", contentWall.currentType);


        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/editEntryUN.php", form))
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
                string textEntries = www.downloadHandler.text;  
                Debug.Log(textEntries);
                editPanel.SetActive(false);
                viewportDisplay.Generate(contentWall.currentType);
            }
        }

    }

    public void deleteEntry()
    {
        StartCoroutine(setOffNotes());
    }

    IEnumerator setOffNotes()
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

        viewportDisplay.Generate(contentWall.currentType);
        yield return new WaitForSeconds(0.5f);
    }

}
