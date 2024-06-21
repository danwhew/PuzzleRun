using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFinal : MonoBehaviour
{

    
    Animator anim;

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void fazerOsTrem()
    {
        
        anim.SetTrigger("tBack");

    }
}
