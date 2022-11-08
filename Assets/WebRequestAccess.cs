using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WebRequestAccess : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text pageInfo;
    private string pageText;

    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI passwordText;
    public TextMeshProUGUI pageInfoText;
    void Start()
    {
    }

    public void SubmitCredentials()
    {
        StartCoroutine(Upload());
    }
    public void GetAllEvents()
    {
        StartCoroutine(GetText());
    }

    IEnumerator Upload()    
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameText.text);
        form.AddField("password", passwordText.text);
        form.AddField("login", "12");

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/login.php", form))
        {
            www.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(usernameText.text + passwordText.text); 
            }
        }
    }


    int numberRows;
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.max.redhawks.us/indexUN.php");
        www.SetRequestHeader("Access-Control-Allow-Origin", "*");
        yield return www.SendWebRequest();


        // Show results as text
        Debug.Log(www.downloadHandler.text);
        pageInfoText.text = www.downloadHandler.text;
        Debug.Log("Number of Rows: " + pageText.IndexOf("numberRows:"));


        /*Debug.Log("Text Length: " + pageText.Length);
        numberRows = int.Parse(pageText.Substring(pageText.IndexOf("numberRows:") + 11, pageText.Length - 4));
        Debug.Log(pageText);*/



        // Or retrieve results as binary data
        byte[] results = www.downloadHandler.data;

    }
}
