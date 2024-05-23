using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    //fazer esse script ser especializado pra coleta de itens na fase de coleta

    //depois criar outros scripts pras respectivas fases, montagem e entrega

    //porta do puzzle atual
    public Porta portinha;
    public GameObject excluir;

    //pegar animacao da porta aqui, excluir o script da porta


    public void fazerOsTrem()
    {
        portinha.condicao = true;
        GameController.instance.addScore(10);
        Destroy(excluir);
    }

}
