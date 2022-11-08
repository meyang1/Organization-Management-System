using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class WebRequestAccess : MonoBehaviour
{
    public Text username;
    public Text password;
    public Text pageInfo;
    private string pageText;
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
        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("login", "12");

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(username.text + password.text);
            }
        }
    }


    int numberRows;
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.max.redhawks.us/indexUN.php");
        yield return www.SendWebRequest();


        // Show results as text
        Debug.Log(www.downloadHandler.text);
        pageText = www.downloadHandler.text;
        Debug.Log("Number of Rows: " + pageText.IndexOf("numberRows:"));


        Debug.Log("Text Length: " + pageText.Length);
        numberRows = int.Parse(pageText.Substring(pageText.IndexOf("numberRows:") + 11, pageText.Length - 4));
        Debug.Log(pageText);



        // Or retrieve results as binary data
        byte[] results = www.downloadHandler.data;

    }
}
