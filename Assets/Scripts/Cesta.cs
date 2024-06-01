using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cesta : MonoBehaviour
{

    public int contador;
    Player playerScript;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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

                transform.GetChild(i).gameObject.SetActive(true);
                transform.GetChild(i).SetSiblingIndex(1);

            }


        }


        if (playerScript.roundEuTo == 1)
        {

            if (transform.GetChild(transform.childCount - 3).gameObject.activeInHierarchy == true)
            {
                Esvaziar();
            }
        }
        else
        {
            if (transform.GetChild(transform.childCount - 2).gameObject.activeInHierarchy == true)
            {
                Esvaziar();
            }
        }
        
            
               
         

        

    }

}
