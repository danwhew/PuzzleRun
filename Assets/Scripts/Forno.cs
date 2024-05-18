using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forno : MonoBehaviour
{
    public float cooldown = 2f;
    public float contador;
    public bool podeContar;
    public GameObject massa;
    public Transform massaPos;
    public GameObject totem;

    public Animator totemAnimator;

    public float teste = 0;
    public bool jaFritou = false;


    // Update is called once per frame
    void Update()
    {
        if (podeContar)
        {
            contador += Time.deltaTime;

            if (contador >= 2f)
            {
                totem.SetActive(true);
                totemAnimator.Play("TotemEntrance");
                massa.SetActive(true);
                contador = 0;
                podeContar = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (jaFritou == false)
        {
            if (other.CompareTag("Item"))
            {
                podeContar = true;
                massa = other.gameObject;
                massa.transform.parent = transform;
                massa.transform.position = massaPos.position;
                massa.SetActive(false);
                jaFritou = true;
            }

        }
    }


}
