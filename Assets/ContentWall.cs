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

    public WebRequest webRequest;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void changeType(int type)
    {
        if(type == 1)
        {
            //tasks
            contentTitle.text = "Tasks";
            StartCoroutine(GetText());

        }
        if(type == 2)
        {
            //calendar
            contentTitle.text = "Calendar";
            StartCoroutine(GetText());
        }
        if(type == 3)
        {
            //notes
            contentTitle.text = "Notes";
            StartCoroutine(GetText());

        }
        anim.SetInteger("animState", 1);
    }
    public void backType()
    {
        contentTitle.text = "";
        anim.SetInteger("animState", 0);
    }
    IEnumerator GetText()
    {
        WWWForm form = new WWWForm();
        form.AddField("user", webRequest.sessionUsername);
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
