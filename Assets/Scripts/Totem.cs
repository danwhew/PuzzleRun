using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    //fazer esse script ser especializado pra coleta de itens na fase de coleta

    //depois criar outros scripts pras respectivas fases, montagem e entrega

    //porta do puzzle atual
    public Porta portinha;
    public DectarEntrada puzzle;
    Animator anim;

    public int fase;
    public int round;
    public bool peperoni;
    //pegar animacao da porta aqui, excluir o script da porta

    private void OnEnable()
    {
        anim = GetComponentInChildren<Animator>();


    }

    public void fazerOsTrem()
    {

        if(GameController.instance.timerBateria < GameController.instance.tempoLimiteBateria)
        {
            GameController.instance.podeInstanciarBateria = true;
        }
        else
        {
            GameController.instance.podeInstanciarBateria = false;
        }

        GameController.instance.podeAtivarTimerBateria = false;
        anim.SetTrigger("tBack");
        portinha.condicao = true;
        GameController.instance.addScore(10);
        if (puzzle.puzzlesIdentity != 3)
        {
            puzzle.item.SetActive(false);

        }
        else
        {
            puzzle.item.transform.position = puzzle.posItemInicial;
            puzzle.item.transform.parent = null;
            puzzle.item.SetActive(false);
        }






    }


}
