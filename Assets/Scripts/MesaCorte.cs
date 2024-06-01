using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MesaCorte : MonoBehaviour
{
    public float contador;
    public bool podeContar;
    public bool temItemPraCortar;
    public GameObject item;
    

    public Transform posItemCortar;
    public Transform posItemCortado;
    public GameObject totem;

    public Animator facaAnimator;
    public Animator totemAnimator;
    public GameObject ps;
    public bool jaCortou = false;

    public Slider slider;    


    private void OnEnable()
    {
        
        podeContar = false;
        jaCortou = false;
        contador = 0;
        slider.value = 0;
        temItemPraCortar = false;
        item.transform.GetChild(1).gameObject.SetActive(true);
        item.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!jaCortou)
        {
            slider.value = contador;

            if (contador >= 2f)
            {
                facaAnimator.SetBool("bAbrir",false);
                item.transform.GetChild(1).gameObject.SetActive(false);
                item.transform.GetChild(2).gameObject.SetActive(true);
                item.SetActive(true);
                totem.SetActive(true);
                totemAnimator.SetTrigger("tPlay");
                item.transform.position = posItemCortado.position;
                contador = 0;
                podeContar = false;
                jaCortou = true;
                temItemPraCortar = false;
                ps.SetActive(false);
            }

        }


    }



    private void OnTriggerEnter(Collider other)
    {
        if (jaCortou == false)
        {
            if (other.CompareTag("Item"))
            {
                item = other.transform.parent.gameObject;
                item.transform.position = posItemCortar.position;
                item.transform.parent = transform;
                item.SetActive(false);
                temItemPraCortar = true;
                podeContar = true;
            }

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (temItemPraCortar)
            {
                facaAnimator.SetBool("bAbrir", true);
                contador += Time.deltaTime;
                ps.SetActive(true);

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            facaAnimator.SetBool("bAbrir", false);
            ps.SetActive(false);
        }
    }

}
