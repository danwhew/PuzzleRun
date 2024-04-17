using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    //fazer esse script ser especializado pra coleta de itens na fase de coleta

    //depois criar outros scripts pras respectivas fases, montagem e entrega

    //talvez criar um switch pras possiveis tags e casos de comparacao

    [Range(1, 4)] public int itensNecessarios;

    //1 queijo
    //2 tomate
    //3 farinha
    //4 molho

    public Porta portinha;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);

            portinha.condicao = true;
            GameController.instance.addScore();


        }





    
    }
}
