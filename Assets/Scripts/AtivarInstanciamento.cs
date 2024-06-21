using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;
    public Transform posAlteradorDeFase;


    private void OnTriggerEnter(Collider other)
    {



        if (other.CompareTag("End"))
        {


            pai.instanciar();
            pai.initTeste();

            GameController.instance.contador++;

            GameController.instance.configFases();

        }

    }


}
