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
    public GameObject itemPraCortar;
    public GameObject itemCortado;

    public Transform posItemCortar;
    public Transform posItemCortado;
    public GameObject totem;

    public Animator totemAnimator;
    public bool jaCortou = false;

    public Slider slider;


    private void OnEnable()
    {
        podeContar = false;
        jaCortou = false;
        contador = 0;
        slider.value = 0;
        totem.SetActive(false);
        temItemPraCortar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!jaCortou)
        {
            slider.value = contador;

            if (contador >= 2f)
            {
                itemPraCortar.SetActive(true);
                totem.SetActive(true);
                totemAnimator.Play("TotemEntrance");
                itemPraCortar.transform.position = posItemCortado.position;
                contador = 0;
                podeContar = false;
                jaCortou = true;
                temItemPraCortar = false;
            }

        }


    }



    private void OnTriggerEnter(Collider other)
    {
        if (jaCortou == false)
        {
            if (other.CompareTag("Item"))
            {
                itemPraCortar = other.transform.parent.gameObject;
                itemPraCortar.transform.position = posItemCortar.position;
                itemPraCortar.transform.parent = transform;
                itemPraCortar.SetActive(false);
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
            contador += Time.deltaTime;

            }
        }
    }

}
