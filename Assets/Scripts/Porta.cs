using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    //posicao onde a porta vai parar
    public Transform empty;
    //condicao se a porta pode levantar
    public bool condicao;

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
            transform.position = Vector3.Lerp(transform.position, empty.position,4f * Time.deltaTime);

        }

    }
}
