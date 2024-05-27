using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Forno : MonoBehaviour
{
    public float contador;
    public bool podeContar;
    public GameObject massa;
    public Transform massaPos;
    public GameObject totem;

    public Animator totemAnimator;

    public bool jaFritou = false;

    public Slider slider;


    private void OnEnable()
    {
        jaFritou = false;
        podeContar = false;
        contador = 0;
        slider.value = 0;
        totem.SetActive(false);
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
                totem.SetActive(true);
                totemAnimator.SetTrigger("tPlay");
                massa.SetActive(true);
                contador = 0;
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
