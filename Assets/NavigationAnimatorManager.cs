using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationAnimatorManager : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    public void changeAnimState(int animStateInt)
    {
        anim.SetInteger("animState", animStateInt);
    }
}
