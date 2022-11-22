using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ContentWall : MonoBehaviour
{
    public GameObject ContentBox;
    public TextMeshProUGUI contentTitle;
    public TextMeshProUGUI contents;
    private Animator anim;
    public int currentType; // 1, 2, 3 : Task, Calendar, Notes
    public GameObject NotificationBox;
    public GameObject camv3;


    public TextMeshProUGUI addTitle;
    public TextMeshProUGUI addDescription;

    public WebRequest webRequest;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void changeType(int type)
    {
        currentType = type;
        if(type == 1)
        {
            //tasks
            contentTitle.text = "Tasks";
            //StartCoroutine(GetText(1));

        }
        if(type == 2)
        {
            //calendar
            contentTitle.text = "Calendar";
            //StartCoroutine(GetText(2));
        }
        if(type == 3)
        {
            //notes
            contentTitle.text = "Notes";
            //StartCoroutine(GetText(3));

        }
        if(type == 0)
        {
            contentTitle.text = "All";
            //StartCoroutine(GetText(0));
        }
        anim.SetInteger("animState", 1);
    }
    public void backType()
    {
        contentTitle.text = "";  
        anim.SetInteger("animState", 0);
    }

    public void lowerCam3(float num)
    {
        LeanTween.moveY(camv3, num, 0.5f).setEaseOutBack();
    }

    public void addEntry()
    {
        StartCoroutine(AddText());
        
    }

    IEnumerator AddText()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventName", addTitle.text);
        form.AddField("eventDescription", addDescription.text);
        form.AddField("eventType", currentType);
        Debug.Log(webRequest.sessionUsername + addTitle.text +addDescription.text + currentType);
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
                //contents.text = www.downloadHandler.text;
            }
        }
        //StartCoroutine(GetText(currentType));
    }
    IEnumerator GetText(int selectType)
    {
        NotificationBox.SetActive(false);
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventType", currentType);
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexUN.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                contents.text = www.downloadHandler.text;
            }
        }

        //pageText = pageInfo.text;
        /*Debug.Log("Number of Rows: " + pageText.IndexOf("numberRows:"));


        Debug.Log("Text Length: " + pageText.Length);
        numberRows = int.Parse(pageText.Substring((pageText.IndexOf("numberRows:") + 11), (pageText.Length - 4)));
        Debug.Log(pageText);*/

    }

}
