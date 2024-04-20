using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    //posicao onde a porta vai parar
    public Transform empty;
    public bool condicao;

    private void OnEnable()
    {
        condicao = false;
    }

    // Update is called once per frame
    void Update()
    {
        //fica verdadeira atraves do script do totem
        if (condicao == true)
        {
            transform.position = Vector3.Lerp(transform.position, empty.position, 1f * Time.deltaTime);

        }

    }
}
