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
    public Animator animatorTotemFinal;

    public Transform posCesta;
    public Transform posPizza;

    public int puzzlesIdentity;

    public Vector3 posItemInicial;

    public int fase;
    public int round;

    public bool coleta;

    public bool jaFezP3;

    //1 - da queda
    //2 - da plataforma giratoria
    //3 - forno

    private void OnEnable()
    {
        jaFezP3 = false;

        if (puzzlesIdentity != 3)
        {
            posItemInicial = item.transform.position;
            item.SetActive(true);

        }
        else
        {
            posItemInicial = posPizza.transform.position;
        }









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
        if (puzzlesIdentity != 3)
        {

            if (posItemInicial != null)
            {
                item.transform.position = posItemInicial;
                item.SetActive(false);

            }
        }
        else
        {
            if (item != null)
            {
                item.transform.position = posItemInicial;

            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if (puzzlesIdentity != 3)
        {
            if (other.CompareTag("Player"))
            {
                if (coleta)
                {
                    casoColeta();
                }
                else
                {
                    casoMontagem();
                }

            }

        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (jaFezP3 == false)
                {
                    GameObject pizza;
                    Player playerScript;
                    Pizza pizzaScript;
                    playerScript = other.GetComponentInParent<Player>();



                    pizza = GameObject.FindGameObjectWithTag("Pizza").gameObject;
                    pizzaScript = pizza.GetComponent<Pizza>();

                    pizzaScript.Esvaziar();
                    pizza.transform.parent = posPizza.transform;
                    pizza.gameObject.transform.position = posPizza.position;


                    if (playerScript.roundEuTo == 1)
                    {
                        item = Pool.poolerInstance.pizzas[0];

                    }
                    else
                    {
                        if (Pool.poolerInstance.ehPeperoni == true)
                        {
                            item = Pool.poolerInstance.pizzas[1];
                        }
                        else
                        {
                            item = Pool.poolerInstance.pizzas[2];
                        }
                    }



                    item.transform.parent = posPizza.transform;
                    item.transform.position = posPizza.position;


                    item.SetActive(true);

                    animatorTotemFinal.SetTrigger("tPlay");
                    jaFezP3 = true;
                }


            }
        }


    }

    public void casoColeta()
    {
        // Debug.Log("Player entrou neste puzzle");

        GameObject cesta;
        GameObject pizza;

        pizza = GameObject.FindGameObjectWithTag("Pizza").gameObject;
        cesta = GameObject.FindGameObjectWithTag("Cesta").gameObject;

        if (posCesta != null)
        {
            if (cesta != null)
            {
                cesta.transform.position = posCesta.position;
                cesta.transform.parent = posCesta.transform;

            }



        }
        else
        {
            if (cesta != null)
            {
                cesta.transform.parent = null;
            }

        }

        if (posPizza != null)
        {
            if (pizza != null)
            {
                pizza.transform.position = posPizza.position;
                pizza.transform.parent = posPizza.transform;
            }
        }
        else
        {
            if (pizza != null)
            {
                pizza.transform.parent = null;
            }
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

    public void casoMontagem()
    {
        // Debug.Log("Player entrou neste puzzle");

        GameObject cesta;
        GameObject pizza;

        pizza = GameObject.FindGameObjectWithTag("Pizza").gameObject;
        cesta = GameObject.FindGameObjectWithTag("Cesta").gameObject;

        if (posCesta != null)
        {
            if (cesta != null)
            {
                cesta.transform.position = posCesta.position;
                cesta.transform.parent = posCesta.transform;

            }



        }
        else
        {
            if (cesta != null)
            {
                cesta.transform.parent = null;
            }

        }

        if (posPizza != null)
        {
            if (pizza != null)
            {
                pizza.transform.position = posPizza.position;
                pizza.transform.parent = posPizza.transform;
            }
        }
        else
        {
            if (pizza != null)
            {
                pizza.transform.parent = null;
            }
        }


        switch (puzzlesIdentity)
        {
            case 1:
                if (animatorEstante != null && animatorTotem != null)
                {
                    animatorEstante.SetTrigger("tPlay");


                }
                break;
            case 2:
                if (animatorGiraGira != null && animatorTotem != null)
                {
                    animatorGiraGira.SetTrigger("tPlay");


                }
                break;
            default:
                {

                }
                break;

        }
    }


}
