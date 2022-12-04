using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraWithVCam : MonoBehaviour
{
    public float moveXValue = 1f;
    public float moveZValue = 2f;
    Vector3 startPos = new Vector3(-18.6f, 1, -10);
    public void moveObjectCam()
    {
        LeanTween.moveX(this.gameObject, -18.6f, moveXValue).setEaseOutBack(); 
        LeanTween.moveZ(this.gameObject, -10, moveZValue).setEaseOutBack();
    }
}
