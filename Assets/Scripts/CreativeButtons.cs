using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;

public class CreativeButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //!!!!! MUST SET IMAGE TO READ/WRITE ENABLED AND FULL RECT MESH TYPE
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
     
}
