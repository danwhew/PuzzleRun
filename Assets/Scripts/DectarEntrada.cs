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
    public Animator animatorTotemInicial; //animator do primeiro totem da fase de entrega

    public Transform posCesta; //posicao que a cesta eh colocada
    public Transform posPizza; //posicao que a pizza eh colocada

    public int puzzlesIdentity; //serve pra diferenciar o tipo dos puzzles
    //1 - da queda
    //2 - da plataforma giratoria
    //3 - forno
    //4 - primeiro puzzle da fase de entrega

    public Vector3 posItemInicial; //posicao do item no inicio do puzzle

    public int fase; //fase que o tile foi ativado
    public int round; // round que o tile foi ativado
    public bool peperoni;

    public int caso; //pra saber se o puzzle eh de coleta/montagem
    // 1 coleta
    // 2 montagem
    // 3 entrega

    public bool jaFezP3; //pra saber se o puzzle do forno ja foi feito
    public bool jaEncostei; // pra saber se ja detectou a entrada do player


    private void OnEnable()
    {
        jaEncostei = false;

        if (puzzlesIdentity != 3 && caso != 3)
        {
            posItemInicial = item.transform.position;
            item.SetActive(true);

        }
        else if (caso != 3)
        {
            jaFezP3 = false; //voltar isso pro inicio do onenable se der algum erro
            posItemInicial = posPizza.transform.position;
        }


        if (GameController.instance != null)
        {


            peperoni = Pool.poolerInstance.ehPeperoni;
            if (totem != null)
            {
                totem.peperoni = peperoni;

            }

            if (GameController.instance.passou == true)
            {
                Pool.poolerInstance.totemtemp = totem;

                fase = GameController.instance.fase;
                round = GameController.instance.round;

                if (fase == 1)
                {
                    fase = 2;
                }
                else if (fase == 2)
                {
                    fase = 3;
                }
                else
                {

                    fase = 1;
                    round++;
                }

                if (totem != null)
                {
                    totem.fase = fase;
                    totem.round = round;
                    GameController.instance.passou = false;
                }
            }
            else
            {
                fase = GameController.instance.fase;
                round = GameController.instance.round;

                if (totem != null)
                {

                    totem.fase = fase;
                    totem.round = round;
                }
            }
        }
    }


    private void OnDisable()
    {


        if (puzzlesIdentity != 3)
        {
            if (puzzlesIdentity == 4)
            {

            }

            else if (posItemInicial != null && item != null)
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
            if (jaEncostei == false)
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

                //se esse puzzle nao eh o puzzle do forno
                if (puzzlesIdentity != 3)
                {
                    if (caso == 1)
                    {
                        casoColeta();
                    }
                    else if (caso == 2)
                    {
                        casoMontagem();

                    }
                    else
                    {
                        casoEntrega();
                    }

                }
                else
                {
                    Player playerScript = other.GetComponentInParent<Player>();
                    casoPuzzleForno(playerScript);
                }

                jaEncostei = true;

            }


        }



    }

    public void casoColeta()
    {
        // Debug.Log("Player entrou neste puzzle");

        GameObject cesta;

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


        GameObject pizza;

        pizza = GameObject.FindGameObjectWithTag("Pizza").gameObject;




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

    public void casoPuzzleForno(Player playerScript)
    {


        if (jaFezP3 == false)
        {
            GameObject pizza;

            Pizza pizzaScript;




            pizza = GameObject.FindGameObjectWithTag("Pizza").gameObject;
            pizzaScript = pizza.GetComponent<Pizza>();

            pizzaScript.EsvaziarInicio();
            pizza.transform.parent = posPizza.transform;
            pizza.gameObject.transform.position = posPizza.position;


            if (playerScript.roundEuTo == 1)
            {
                item = Pool.poolerInstance.pizzas[0];

            }
            else
            {
                if (playerScript.toPeperoni == true)
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

    public void casoEntrega()
    {

        if (puzzlesIdentity == 4)
        {
            GameObject caixaPizza;

            caixaPizza = GameObject.FindGameObjectWithTag("CaixaPizza").gameObject;

            Collider colTemp;

            colTemp =  caixaPizza.GetComponentInChildren<Collider>();
            colTemp.enabled = true;
            


            if (posPizza != null)
            {
                if (caixaPizza != null)
                {
                    caixaPizza.SetActive(true);
                    caixaPizza.transform.position = posPizza.position;
                    caixaPizza.transform.parent = posPizza.transform;
                }
            }
            

            animatorTotemInicial.SetTrigger("tPlay");
           
            

        }

        else if (puzzlesIdentity == 5)
        {
            animatorTotemFinal.SetTrigger("tPlay");
        }
    }

}
