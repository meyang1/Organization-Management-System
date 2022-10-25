using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerConnect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     


    IEnumerator GetData()
    {
        gameObject.guiText.text = "Loading...";
        WWW www = new WWW("http://max.redhawks.us/index.php?table=shoes"); //GET data is sent via the URL

        while (!www.isDone && string.IsNullOrEmpty(www.error))
        {
            gameObject.guiText.text = "Loading... " + www.Progress.ToString("0%"); //Show progress
            yield return null;
        }

        if (string.IsNullOrEmpty(www.error)) gameObject.guiText.text = www.text;
        else Debug.LogWarning(www.error);
    }

}
