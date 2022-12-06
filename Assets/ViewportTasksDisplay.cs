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
    string textEntries;
    void Start()
    {
         
    }
    public void Generate()
    {
        StartCoroutine(GetText1());
    }


    IEnumerator GetText1()
    {
        yield return new WaitForSeconds(0.5f);
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
        form.AddField("eventType", 0);
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
            if(numberOfParameters%3 == 0)
            {
                // Entry ID
                tempEntryID = entry.Trim();
            }
            if(numberOfParameters%3 == 1)
            {
                // Name
                tempEntryName = entry.Trim();
            }
            if(numberOfParameters%3 == 2)
            {
                //Description
                tempEntryDescription = entry.Trim();

                var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
                // do something with the instantiated item -- for instance
                item_go.GetComponentInChildren<Text>().text = "<b>"+
                    tempEntryName + "</b>\n" + 
                    tempEntryDescription;
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