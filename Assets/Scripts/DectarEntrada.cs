using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class DectarEntrada : MonoBehaviour
{


    public GameObject item;

    public Totem totem;

    public Animator animatorEstante;
    public Animator animatorTotem;
    public Animator animatorGiraGira;
    public GameObject cesta;
    public Transform posCesta;

    public int puzzlesIdentity;

    public Vector3 posItemInicial;

    public int fase;
    public int round;

    public Totem tot;


    //1 - da queda
    //2 - da plataforma giratoria

    private void OnEnable()
    {
        posItemInicial = item.transform.position;

        item.SetActive(true);

        if (GameController.instance != null)
        {
            if (GameController.instance.passou == true)
            {
                fase = GameController.instance.fase;
                round = GameController.instance.round;

                if (fase == 1)
                {
                    fase = 2;
                }
                else
                {
                    fase = 1;
                    round++;
                }

                totem.fase = fase;
                totem.round = round;
                GameController.instance.passou = false;
            }
            else
            {
                fase = GameController.instance.fase;
                round = GameController.instance.round;
                totem.fase = fase;
                totem.round = round;
            }




        }

    }


    private void OnDisable()
    {
        item.transform.position = posItemInicial;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            // Debug.Log("Player entrou neste puzzle");

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
                        animatorEstante.SetTrigger("tPlay");
                        animatorTotem.SetTrigger("tPlay");


                    }
                    break;
                case 2:
                    if (animatorGiraGira != null && animatorTotem != null)
                    {
                        animatorGiraGira.SetTrigger("tPlay");
                        animatorTotem.SetTrigger("tPlay");


                    }
                    break;
                default:
                    {
                        if (animatorTotem != null)
                        {
                            animatorTotem.SetTrigger("tPlay");
                        }
                    }
                    break;

            }


        }
    }


}
