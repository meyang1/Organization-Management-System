using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class WebRequestAccess : MonoBehaviour
{

    

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void HelloString(string str);

    [DllImport("__Internal")]
    private static extern void PrintFloatArray(float[] array, int size);

    [DllImport("__Internal")]
    private static extern string JSLogin(string url, string formData);

    [DllImport("__Internal")]
    private static extern void JSGET(string url);

    [DllImport("__Internal")]
    private static extern void setCookie(string cname, string cvalue, int exdays);

    [DllImport("__Internal")]
    private static extern void getCookie(string cname);

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);

    [DllImport("__Internal")]
    private static extern string StringReturnValueFunction();

    [DllImport("__Internal")]
    private static extern void BindWebGLTexture(int texture);


    private string pageText;

    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI passwordText;
    public TextMeshProUGUI pageInfoText;
    public TextMeshProUGUI testText;
    void Start()
    {
    }

    public void SubmitCredentials()
    {
        StartCoroutine(Upload());
        Hello();

        HelloString("This is a string.");
        string formString = "username=" + usernameText.text + "&password=" + passwordText.text + "&login=12";
        string check = JSLogin("http://www.max.redhawks.us/login.php", formString);
        float[] myArray = new float[10];
        PrintFloatArray(myArray, myArray.Length);

        int result = AddNumbers(5, 7);
        Debug.Log(result);
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

        string formString2 = "username=" + usernameText.text + "; password=" + passwordText.text+"; login=12"; 
        string check2 = JSLogin("http://www.max.redhawks.us/login.php", formString2);

        pageInfoText.text = check2;
        //using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/login.php", form))
        //{
        /*www.SetRequestHeader("Access-Control-Allow-Origin", "*");
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
        //}*/

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username="+usernameText.text+"&password="+passwordText.text));
        //JSLogin("http://www.max.redhawks.us/login.php", formData);
        //UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/loginUN.php", formData);
        //yield return www.SendWebRequest();
        yield return 1;




    }


    int numberRows;
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://www.max.redhawks.us/indexUN.php");
        www.SetRequestHeader("Access-Control-Allow-Origin", "*");
        yield return www.SendWebRequest();


        // Show results as text
        Debug.Log(www.downloadHandler.text);
        //pageInfoText.text = www.downloadHandler.text;
        Debug.Log(pageInfoText.text);
        //pageInfoText.text = JS
        JSGET("http://www.max.redhawks.us/indexUN.php");
        testText.text = JSLogin("http://www.max.redhawks.us/indexUN.php", "");
        /*Debug.Log("Text Length: " + pageText.Length);
        numberRows = int.Parse(pageText.Substring(pageText.IndexOf("numberRows:") + 11, pageText.Length - 4));
        Debug.Log(pageText);*/



        // Or retrieve results as binary data
        byte[] results = www.downloadHandler.data;

    } 

    //Declare below inside the class body



}
