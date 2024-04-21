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

    public GameObject totem;

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
                massa.SetActive(true);
                totem.SetActive(true);
                contador = 0;
                podeContar = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(jaFritou == false)
        {
        if (other.CompareTag("Item"))
        {
            podeContar = true;
            massa = other.gameObject;
            massa.transform.parent = null;
            massa.transform.position = new Vector3(massa.transform.position.x, 0.1f , massa.transform.position.z);
            massa.SetActive(false);
            jaFritou=true;
        }

        }
    }

    
}
