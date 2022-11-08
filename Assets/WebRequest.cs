using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WebRequest : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;

    public TextMeshProUGUI registerUsername;
    public TextMeshProUGUI registerPassword;

    public GameObject notificationPanel;

    public TextMeshProUGUI pageInfo;
    private string pageText;
    string inputName, inputPassword;
    void Start()
    {
    }

    public void SubmitCredentials()
    {
        inputName = username.text;
        inputPassword = password.text;
        StartCoroutine(Upload());
    }
    public void GetAllEvents()
    {
        StartCoroutine(GetText());
    }
    public void Register()
    {
        StartCoroutine(RegisterAccount());
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", inputName);
        form.AddField("password", inputPassword);
        form.AddField("login", "12");

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/loginUN.php", form))
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
                // Show results as text
                pageInfo.text = www.downloadHandler.text;

                notificationPanel.SetActive(true);
            }
        }
    }
    IEnumerator RegisterAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", registerUsername.text);
        form.AddField("password", registerPassword.text);
        form.AddField("register", "12");

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/registrationUN.php", form))
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
                // Show results as text
                pageInfo.text = www.downloadHandler.text;
                notificationPanel.SetActive(true);
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
