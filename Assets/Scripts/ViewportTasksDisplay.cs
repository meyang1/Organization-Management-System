using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ViewportTasksDisplay : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private int m_ItemsToGenerate;

    public WebRequest webRequest;
    public GameObject titleTasks_Prefab;
    public GameObject titleCalendar_Prefab;
    public GameObject titleNotes_Prefab;
    string textEntries;
    public GameObject blockCanvas;
    void Start()
    {

    }
    public void Generate(int type)
    {
        blockCanvas.SetActive(true);
        StartCoroutine(GetText1(type));
    }

    public string currentYear="";
    IEnumerator GetText1(int type)
    {
        foreach (Transform child in m_ContentContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(.25f);
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventType", type);
        using (UnityWebRequest www = UnityWebRequest.Post("http://www.max.redhawks.us/indexUN.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);

                blockCanvas.SetActive(false);
            }
            else
            {
                Debug.Log("Form upload complete!");
                //contents.text = www.downloadHandler.text;
                textEntries = www.downloadHandler.text;
                Debug.Log(textEntries);

                blockCanvas.SetActive(false);
            }
        }

        string[] separatingStrings = { "&%^&" };

        string[] entries = textEntries.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);


        int numberOfParameters = 4;
        string tempEntryID = "";
        string tempEntryName = "";
        string tempEntryDate = "";


        if (type == 1) { var title_go = Instantiate(titleTasks_Prefab, m_ContentContainer); }
        if (type == 2) { var title_go = Instantiate(titleCalendar_Prefab, m_ContentContainer); }
        if (type == 3) { var title_go = Instantiate(titleNotes_Prefab, m_ContentContainer); }

        string tempEntryDescription = "";
        //private DateTime _dateTime = DateTime.Now;
        
        foreach (string entry in entries)
        { 
            if (numberOfParameters % 4 == 0)
            {
                // Entry ID
                tempEntryID = entry.Trim();
            }
            if (numberOfParameters % 4 == 1)
            {
                // Name
                tempEntryName = entry.Trim();
            }
            if (numberOfParameters % 4 == 2)
            {
                tempEntryDate = entry.Trim();
            }
            if (numberOfParameters % 4 == 3)
            {
                //Description
                tempEntryDescription = entry.Trim();

                // instantiates the prefab object to the content container
                var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
                // for now, set title to orange and text to white (dark gray background)
                string tempText = "<b>" + tempEntryName + "</b>\n<color=white>" + tempEntryDescription + "</color>";
                if (tempEntryDescription == "")
                {
                    tempText = "<b>" + tempEntryName + "</b>";
                }

                item_go.GetComponentInChildren<EntryData>().entryName = tempEntryName;
                item_go.GetComponentInChildren<EntryData>().entryDescription = tempEntryDescription;
                item_go.GetComponentInChildren<EntryData>().entryDate = tempEntryDate;
                if (!tempEntryDate.Equals("9999-12-31"))
                {
                    item_go.GetComponentInChildren<EntryData>().dateText.text = tempEntryDate.Substring(5);
                    if (!tempEntryDate.Substring(0, 4).Equals(currentYear))
                    {
                        item_go.GetComponentInChildren<EntryData>().yearText.text = "("+tempEntryDate.Substring(0, 4)+")";

                    }
                }


                item_go.GetComponentInChildren<Text>().text = tempText; 
                item_go.GetComponentInChildren<EntryData>().entryID = tempEntryID;
                //item_go.GetComponent<Image>().color = i % 2 == 0 ? Color.yellow : Color.cyan;

                item_go.transform.SetParent(m_ContentContainer);
                //reset the item's scale -- this can get munged with UI prefabs
                item_go.transform.localScale = Vector2.one;
            }

            numberOfParameters++;
        }

    }
}
