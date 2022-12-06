using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ContentWall : MonoBehaviour
{
    public GameObject ContentBox;
    public TextMeshProUGUI contentTitle;
    public Text contents;
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
            StartCoroutine(GetText(1));

        }
        if(type == 2)
        {
            //calendar
            contentTitle.text = "Calendar";
            StartCoroutine(GetText(2));
        }
        if(type == 3)
        {
            //notes
            contentTitle.text = "Notes";
            StartCoroutine(GetText(3));

        }
        if(type == 0)
        {
            contentTitle.text = "All";
            StartCoroutine(GetText(0));
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
    string textEntries;
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private int m_ItemsToGenerate;


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
                contents.text = www.downloadHandler.text;
            }
        }
        StartCoroutine(GetText(currentType));
    }
    IEnumerator GetText(int type1)
    {
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
                //contents.text = www.downloadHandler.text;
                textEntries = www.downloadHandler.text;
                Debug.Log(textEntries);
            }
        }

        string[] separatingStrings = { "&%^&" };

        string[] entries = textEntries.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);


        int numberOfParameters = 3;
        string tempEntryID = "";
        string tempEntryName = "";
        string tempEntryDescription = "";
        foreach (string entry in entries)
        {
            if (numberOfParameters % 3 == 0)
            {
                // Entry ID
                tempEntryID = entry.Trim();
            }
            if (numberOfParameters % 3 == 1)
            {
                // Name
                tempEntryName = entry.Trim();
            }
            if (numberOfParameters % 3 == 2)
            {
                //Description
                tempEntryDescription = entry.Trim();

                var item_go = Instantiate(m_ItemPrefab);
                // do something with the instantiated item -- for instance
                item_go.GetComponentInChildren<TextMeshProUGUI>().text = "<b>" +
                    tempEntryName + "</b>\n" +
                    tempEntryDescription;
                Debug.Log(numberOfParameters + " entry name: " + tempEntryName);
                //item_go.GetComponent<Image>().color = i % 2 == 0 ? Color.yellow : Color.cyan;
                //parent the item to the content container
                item_go.transform.SetParent(m_ContentContainer);
                //reset the item's scale -- this can get munged with UI prefabs
                item_go.transform.localScale = Vector2.one;
            }
            numberOfParameters++;
        }

    }

}
