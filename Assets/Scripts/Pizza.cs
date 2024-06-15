using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{

    private void Start()
    {
        Esvaziar();
    }

    public void Esvaziar()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Preencher(string nomeObj)
    {
        for (int i = 0; i < transform.childCount; i++)
        {


            if (transform.GetChild(i).gameObject.name == nomeObj)
            {
                if(nomeObj == "Queijo")
                {
                    ScoreManager.instance.conquista[3] = true;
                    Debug.Log("vc fez a conquista 3");
                    ScoreManager.instance.atualizarConquistas();
                }
                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).SetSiblingIndex(1);
            }

        }

    }

 
}
