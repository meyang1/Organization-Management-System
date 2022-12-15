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
    public Scrollbar scrollUpDown;
    public GameObject scrollUpDownObject;
    string textEntries;
    public bool editModeBoolean = false;
    public int maxNumberPages = 0;

    void Start()
    {

    }

    Vector3 startPos = new Vector3(-18.6f, 1, -10);
    public void moveObjects(GameObject obj)
    {
        if(countEntries>0)
           obj.transform.position = startPos + (new Vector3(2 * scroll.value * (countEntries - 1), -(maxNumberPages - 1) * scrollUpDown.value, 2 * scroll.value * (countEntries - 1)));

    }

    public void createObjects()
    {
        //maxNumberPages = 0;
        StartCoroutine(GenerateObjects());

    }

    public void Generate(int type)
    {
        StartCoroutine(GetText1(type));
    }

    public void setEditMode(bool editMode)
    {
        editModeBoolean = editMode;
        foreach (Transform child in transform)
            child.GetComponentInChildren<EntryData>().setEditModeNotes(editMode);
    }

    public int countEntries;
    public int currentMaxNumberPages;
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


        int numberOfParameters = 4;
        string tempEntryID = "";
        string tempEntryName = "";
        string tempEntryDate = "";

        countEntries = 0;
        string tempEntryDescription = "";
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
                //item_go.GetComponentInChildren<Text>().text = tempText;
                item_go.GetComponentInChildren<EntryData>().entryID = tempEntryID;
                item_go.GetComponentInChildren<EntryData>().cubeTitle.text = tempEntryName;
                item_go.GetComponentInChildren<EntryData>().cubeDescription.text = tempEntryDescription;


                item_go.GetComponentInChildren<EntryData>().entryName = tempEntryName;
                item_go.GetComponentInChildren<EntryData>().entryDescription = tempEntryDescription;

                item_go.transform.SetParent(m_ContentContainer);
                item_go.transform.position += (new Vector3(2, 0, 2)) * countEntries;

                /*yield return new WaitForSeconds(1f);
                item_go.GetComponentInChildren<EntryData>().getHighestNumPages();
                currentMaxNumberPages = item_go.GetComponentInChildren<EntryData>().highestNumPages;

                for (int i = 2; i<currentMaxNumberPages; i++)
                {

                    var item_go2 = Instantiate(m_ItemPrefab, m_ContentContainer);
                    item_go2.transform.position += (new Vector3(0, 1f, 0)) * (i - 1);
                    item_go2.GetComponentInChildren<EntryData>().cubeTitle.text = tempEntryName;
                    item_go2.GetComponentInChildren<EntryData>().cubeDescription.text = tempEntryDescription;

                    item_go2.GetComponentInChildren<EntryData>().cubeTitle.pageToDisplay = i;
                    item_go2.GetComponentInChildren<EntryData>().cubeDescription.pageToDisplay = i;

                } 
                */
                //item_go.GetComponent<Image>().color = i % 2 == 0 ? Color.yellow : Color.cyan;

                //reset the item's scale -- this can get munged with UI prefabs
                //item_go.transform.localScale = Vector2.one;

                countEntries++;
            }

            numberOfParameters++;
        }

        if (editModeBoolean)
        {
            setEditMode(editModeBoolean);
        }

        //yield return new WaitForSeconds(.5f);
        /*maxNumberPages = 0;
        foreach (Transform child in m_ContentContainer)
        {
            child.GetComponentInChildren<EntryData>().getHighestNumPages();
            currentMaxNumberPages = child.GetComponentInChildren<EntryData>().highestNumPages;

            for(int i=1; i<currentMaxNumberPages; i++)
            {
                var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
                item_go.transform.position += (new Vector3(0, 1, 0)) * (i-1);
            }
            
            if (child.GetComponentInChildren<EntryData>().highestNumPages > maxNumberPages)
            {
                maxNumberPages = child.GetComponentInChildren<EntryData>().highestNumPages;

            }
            Debug.Log(maxNumberPages);
        }*/
        if(maxNumberPages == 1)
        {
            scrollUpDownObject.SetActive(false);
        }
        else
        {
            //scrollUpDownObject.SetActive(true);
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
