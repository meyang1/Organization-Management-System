using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class cubeManager : MonoBehaviour
{
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private int m_ItemsToGenerate;

    public WebRequest webRequest;
    public GameObject titleTasks_Prefab;
    public GameObject titleCalendar_Prefab;
    public float multiplier = 1f; // 1 : 0, 2: 1, 3 : 1.3, 4, 1.5, 5 : 1.6, 6 : 1.65, 7 : 1.7, 8 : 1.75, 9: 1.775
    public GameObject titleNotes_Prefab;
    public Scrollbar scroll;
    string textEntries;
    void Start()
    {

    }

    Vector3 startPos = new Vector3(-18.6f, 1, -10);
    public void moveObjects(GameObject obj)
    {
        obj.transform.position = startPos + (new Vector3(2, 0, 2)) * scroll.value * (countEntries-1);
    }

    public void createObjects()
    {
        StartCoroutine(GenerateObjects());
    }

    public void Generate(int type)
    {
        StartCoroutine(GetText1(type));
    }


    public int countEntries;
    IEnumerator GenerateObjects()
    {
        foreach (Transform child in m_ContentContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventType", 3);
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

        countEntries = 0;
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

                // instantiates the prefab object to the content container
                var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
                // for now, set title to orange and text to white (dark gray background)
                string tempText = "<b>" + tempEntryName + "</b>\n<color=white>" + tempEntryDescription + "</color>";
                if (tempEntryDescription == "")
                {
                    tempText = "<b>" + tempEntryName + "</b>";
                }
                //item_go.GetComponentInChildren<Text>().text = tempText;
                item_go.GetComponentInChildren<EntryData>().entryID = tempEntryID;
                item_go.GetComponentInChildren<EntryData>().textDescription = tempText;
                item_go.GetComponentInChildren<EntryData>().cubeTitle.text = tempEntryName;
                item_go.GetComponentInChildren<EntryData>().cubeDescription.text = tempEntryDescription;

                //item_go.GetComponent<Image>().color = i % 2 == 0 ? Color.yellow : Color.cyan;

                item_go.transform.SetParent(m_ContentContainer);
                item_go.transform.position += (new Vector3(2, 0, 2))*countEntries;
                //reset the item's scale -- this can get munged with UI prefabs
                //item_go.transform.localScale = Vector2.one;
                countEntries++;
            }
            numberOfParameters++;
        }


    }
    IEnumerator GetText1(int type)
    {
        foreach (Transform child in m_ContentContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
        yield return new WaitForSeconds(.5f);
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventType", type);
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


        if (type == 1) { var title_go = Instantiate(titleTasks_Prefab, m_ContentContainer); }
        if (type == 2) { var title_go = Instantiate(titleCalendar_Prefab, m_ContentContainer); }
        if (type == 3) { var title_go = Instantiate(titleNotes_Prefab, m_ContentContainer); }

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

                // instantiates the prefab object to the content container
                var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
                // for now, set title to orange and text to white (dark gray background)
                string tempText = "<b>" + tempEntryName + "</b>\n<color=white>" + tempEntryDescription + "</color>";
                if (tempEntryDescription == "")
                {
                    tempText = "<b>" + tempEntryName + "</b>";
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
