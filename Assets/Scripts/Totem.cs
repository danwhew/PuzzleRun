using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    //fazer esse script ser especializado pra coleta de itens na fase de coleta

    //depois criar outros scripts pras respectivas fases, montagem e entrega

    //porta do puzzle atual
    public Porta portinha;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);

            portinha.condicao = true;
            GameController.instance.addScore(10);


        }
        if (other.CompareTag("ItemErrado"))
        {
            Destroy(other.gameObject);

            portinha.condicao = true;
            GameController.instance.addScore(-10);
        }


    }
}
