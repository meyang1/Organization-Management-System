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

    public string sessionUsername;
    void Start()
    {
    }

    public void MoveMidCube(float amount)
    {
        leftCubeAnim.SetInteger("AnimState", 1);
        StartCoroutine(moveCube(amount));
    }
    public Animator leftCubeAnim;
    public GameObject Cam3;
    IEnumerator moveCube(float amount)
    {
        yield return new WaitForSeconds(0f);
        LeanTween.moveX(MidCube, amount, 2.5f).setEaseOutCirc();
        leftCubeAnim.SetInteger("AnimState", 0);
    }

    public void MoveContentWall(int moveType)
    { 
        
        StartCoroutine(moveCWall(moveType));
    } 
    IEnumerator moveCWall(int moveType)
    {
        if(moveType == 1) { 
            //down
            yield return new WaitForSeconds(1f);
            LeanTween.moveY(ContentWall, 0.85f, 1.75f).setEaseOutBack();
        }
        if(moveType == 2)
        {
            //up
            LeanTween.moveY(ContentWall, 9f, 2.0f).setEaseInOutBack();
        }
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
    public GameObject ContentWall;
    public GameObject MidCube;
    public GameObject ActualMenuLight;
    public GameObject OpenMenu;
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
                if (www.downloadHandler.text.IndexOf("Success")!=-1)
                {
                    sessionUsername = username.text;
                    pageInfo.text = username.text + " has successfully logged in!";
                    ActualMenuLight.SetActive(false);
                    MoveMidCube(9.41f);

                    OpenMenu.SetActive(true);
                    Cam3.SetActive(true);

                    MoveContentWall(1);

                }
                else
                {
                    pageInfo.text = www.downloadHandler.text;
                }

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

    public void ChangeEventType(int newType)
    {
        currentEventType = newType;
    }

    public TextMeshProUGUI addEventName;
    public TextMeshProUGUI addEventDescription;

    public TextMeshProUGUI calendarStart;
    public TextMeshProUGUI calendarEnd;
    public TextMeshProUGUI calendarRepeat;

    public TextMeshProUGUI taskPriority;
    public TextMeshProUGUI taskCompletion;
    public TextMeshProUGUI taskDeadline;

    public int currentEventType;

    IEnumerator CreateEntry()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", sessionUsername);
        form.AddField("eventName", addEventName.text);
        form.AddField("eventDescription", addEventDescription.text);
        form.AddField("eventType", currentEventType);
        form.AddField("startTimeDate", calendarStart.text);
        form.AddField("endTimeDate", calendarEnd.text);
        form.AddField("repeatStatus", calendarRepeat.text);
        form.AddField("PriorityLevel", taskPriority.text);
        form.AddField("CompletionStatus", taskCompletion.text);
        form.AddField("Deadline", taskDeadline.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/addEntryUN.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(addEventName.text + ": "+ addEventDescription.text);
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



