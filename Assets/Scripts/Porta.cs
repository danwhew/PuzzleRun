using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    //condicao se a porta pode levantar
    public bool condicao;
    public Animator animPorta;

    //acho que pode apagar isso
    private void OnEnable()
    {
        condicao = false;
    }


    // Update is called once per frame
    void Update()
    {
        //verifica se pode tocar a animacao da porta descendo
        //fica verdadeira atraves do script do totem
        if (condicao == true)
        {
            //substituir por animacao
            animPorta.SetTrigger("tPlay");

        }

    }
}
