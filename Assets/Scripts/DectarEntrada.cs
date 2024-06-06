using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering;

public class DectarEntrada : MonoBehaviour
{


    public Transform posBateria;
    public GameObject item; //item do puzzle
    public Totem totem; //totem do puzzle


    public Animator animatorEstante;
    public Animator animatorTotem;
    public Animator animatorGiraGira; //animator da plataforma que gira 360 graus
    public Animator animatorTotemFinal; //animator do totem da fase do forno

    public Transform posCesta; //posicao que a cesta eh colocada
    public Transform posPizza; //posicao que a pizza eh colocada

    public int puzzlesIdentity; //serve pra diferenciar o tipo dos puzzles
    //1 - da queda
    //2 - da plataforma giratoria
    //3 - forno

    public Vector3 posItemInicial; //posicao do item no inicio do puzzle

    public int fase; //fase que o tile foi ativado
    public int round; // round que o tile foi ativado
    public bool peperoni;

    public bool coleta; //pra saber se o puzzle eh de coleta/montagem

    public bool jaFezP3; //pra saber se o puzzle do forno ja foi feito



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
                Pool.poolerInstance.totemtemp = totem;
                
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
                peperoni = Pool.poolerInstance.ehPeperoni;
                fase = GameController.instance.fase;
                round = GameController.instance.round;
                
                totem.peperoni = peperoni;
                totem.fase = fase;
                totem.round = round;
            }




        }



    }


    private void OnDisable()
    {
        if (puzzlesIdentity != 3)
        {

            if (posItemInicial != null && item != null)
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


        if (other.CompareTag("Player"))
        {


            if (GameController.instance != null)
            {

                GameController.instance.podeAtivarTimerBateria = true;

                if (GameController.instance.podeInstanciarBateria == true)
                {
                    GameObject tmp;
                    tmp = Pool.poolerInstance.getBaterias();
                    tmp.transform.position = posBateria.position;
                    tmp.SetActive(true);
                    GameController.instance.podeInstanciarBateria = false;

                }


            }


            if (puzzlesIdentity != 3)
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
            else
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
