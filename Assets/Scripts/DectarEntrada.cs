using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DectarEntrada : MonoBehaviour
{

    public Animator animatorEstante;
    public Animator animatorTotem;
    public GameObject cesta;
    public Transform posCesta;

    public bool playerTerminou = false;

    public int puzzlesIdentity;

   

    //1 - da queda



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            Debug.Log("Player entrou neste puzzle");

            if (posCesta != null)
            {
                cesta = GameObject.FindGameObjectWithTag("Cesta").gameObject;
                cesta.transform.position = posCesta.position;

            }


            switch (puzzlesIdentity)
            {
                case 1:
                    if (animatorEstante != null && animatorTotem != null)
                    {

                        animatorEstante.Play("EstanteCair");
                        animatorTotem.Play("TotemEntrance");

                    }
                    break;
                default:
                    {
                        if (animatorTotem != null)
                        {
                            animatorTotem.Play("TotemEntrance");
                        }
                    }
                    break;

            }


        }
    }
}
