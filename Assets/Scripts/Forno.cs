using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Forno : MonoBehaviour
{
    public float contador;
    public bool podeContar;
    public GameObject massa;
    public GameObject massaPronta;
    public Transform massaPos;
    public GameObject totem;
    public  Player playerScript;
    public Animator totemAnimator;
    public DectarEntrada puzzle;

    public bool jaFritou = false;

    public Slider slider;


    private void OnEnable()
    {
        jaFritou = false;
        podeContar = false;
        contador = 0;
        slider.value = 0;

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = contador;

        if (podeContar)
        {
            contador += Time.deltaTime;

            if (contador >= 2f)
            {
                totemAnimator.SetTrigger("tPlay");

                if (playerScript.roundEuTo == 1)
                {
                    massa = Pool.poolerInstance.pizzas[3];

                }
                else
                {
                    if (Pool.poolerInstance.ehPeperoni == true)
                    {
                        massa = Pool.poolerInstance.pizzas[4];
                    }
                    else
                    {
                        massa = Pool.poolerInstance.pizzas[5];
                    }
                }


                massa.transform.position = massaPos.position;
                massa.SetActive(true);
                
                puzzle.item = massa;
                
                podeContar = false;
                jaFritou = true;

            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (jaFritou == false)
        {
            if (other.CompareTag("Item"))
            {
                massa = other.transform.parent.gameObject;
                massa.transform.position = massaPos.position;
                massa.transform.parent = transform;
                massa.SetActive(false);
                podeContar = true;
            }
             
        }
    }


}
