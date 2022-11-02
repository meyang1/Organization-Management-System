using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MySQLData : MonoBehaviour
{
    public Text nameText;
    public Text descText;
    public Text showAdd;

    public void AddText()
    {
        StartCoroutine(AddEntry(nameText.text, descText.text));
    }

    IEnumerator AddEntry(string name, string description)
    {
        /*List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormFileSection("eventName = " + nameText.text + " & eventDescription = " + descText.text));
        formData.Add(new MultipartFormDataSection("eventName=" + nameText.text + "&eventDescription=" + descText.text));

        UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexMain.php", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }*/
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("eventName", name);
        wwwForm.AddField("eventDescription  ", description);


        //WWW download = new WWW("http://www.max.redhawks.us/indexMain.php", wwwForm);
        //yield return download;
        //showAdd.text = showAdd.text + " " + download.text;
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexMain.php", wwwForm))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                showAdd.text = "Added " + nameText.text + ": " + descText.text;
            }
        }

    }
}