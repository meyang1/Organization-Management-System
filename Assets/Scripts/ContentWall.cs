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

    public ViewportTasksDisplay viewport;

    public WebRequest webRequest;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public AudioClip changeContentSFX;
    public void changeType(int type)
    {
        currentType = type;
        NotificationBox.SetActive(false);
        GameObject.Find("SFXPlayer").GetComponent<AudioSource>().clip = changeContentSFX;
        GameObject.Find("SFXPlayer").GetComponent<AudioSource>().Play();
        if (type == 1)
        {
            //tasks
            contentTitle.text = "Tasks";
            //viewport.Generate(1); 

        }
        if(type == 2)
        {
            //calendar
            contentTitle.text = "Calendar";
            //viewport.Generate(2); 
        }
        if(type == 3)
        {
            //notes
            contentTitle.text = "Notes";
            //viewport.Generate(3); 

        }
        if(type == 0)
        {
            contentTitle.text = "All";
            //viewport.Generate(0); 
        }
        anim.SetInteger("animState", 1);
    }
    public void backType()
    {
        contentTitle.text = "";  
        anim.SetInteger("animState", 0);
    }
    public void changeAnimState(int animS)
    {
        anim.SetInteger("animState", animS);
    }

    public void lowerCam3(float num)
    {
        LeanTween.moveY(camv3, num, 0.5f).setEaseOutBack();
    }

    public void addEntry()
    {
        if (!addTitle.text.Equals("​"))
        {
            StartCoroutine(AddText());
            
        }
    }

    public Text calendarTextDate;
    public GameObject datePanel;
    IEnumerator AddText()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventName", addTitle.text);
        form.AddField("eventDescription", addDescription.text);
        form.AddField("eventType", currentType);

            form.AddField("startTimeDate", calendarTextDate.text + " 00:00:00.000");
            form.AddField("Deadline", calendarTextDate.text + " 00:00:00.000");
            Debug.Log(calendarTextDate.text + " 00:00:00.000");
            calendarTextDate.text = "0000-00-00";
        datePanel.SetActive(false);


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
                Debug.Log(www.downloadHandler.text);
            }
        }
        viewport.Generate(currentType);
    }

}
