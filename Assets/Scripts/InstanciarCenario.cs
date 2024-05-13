using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] puzzlesColeta;
    public GameObject[] puzzlesMontagem;

    public GameObject alteraFase;

    public Transform anchor; //pos do tile base
    public Transform empty; //pos puzzle

    public bool ehTileInicial;

    public int rand;

    //teste
    //public bool[] coletados = new bool[5];

    /*pensar em um esquema pra saber se tal puzzle ja foi instanciado ou nao
    apesar de serem aleatorios, nao podem ser repetidos*/

    /* private void OnEnable()
     {
         //nao  sei pq mas o singleton nao funciona aqui, entao passei o instanciarPuzzles pro start
         //ainda nao me faz sentido estar funcionando mas..

     }*/

    private void Start()
    {

        //apos instanciar 5 puzzles da fase de coleta, trocar a fase pra proxima

        //criar um metodo pra instanciar puzzles da fase de montagem

       /* if (GameController.instance.fase == 1)
        {
            if (GameController.instance.contador < 1)
            {
                instanciarPuzzlesFaseColeta();
                GameController.instance.contador++;

            }
            else
            {
                GameController.instance.fase++;
                GameController.instance.contador = 0;
                if (alteraFase != null)
                {
                GameObject marcador = Instantiate(alteraFase, anchor.position - new Vector3(0, 0, 21), Quaternion.identity);
                    instanciarPuzzlesFaseMontagem();
                }

            }
        }

        else if (GameController.instance.fase == 2)
        {
            if(GameController.instance.contador < 1)
            {
                instanciarPuzzlesFaseMontagem();
                GameController.instance.contador++;
                GameController.instance.fase  = 1;
                GameController.instance.contador = 0;
            }
            

        }*/

    
        if(GameController.instance.fase == 1 )
        {
            instanciarPuzzlesFaseColeta();
        }
        else
        {
            instanciarPuzzlesFaseMontagem();
        }


        //depois da da fase de entrega, mudar a exibicao da fase pra bonus e comecar a repetir os tiles,
        //talvez simplesmente resetando os valores do game controller



    }


    //instancia o chao base, talvez a textura possa variar dependendo da fase
    public void instanciar()
    {
        GameObject cloneTile = Instantiate(tiles[0], anchor.position, Quaternion.identity, transform.parent);

    }


    //instancia puzzles da fase de coleta em cima
    public void instanciarPuzzlesFaseColeta()
    {

        /*com base no que o game controller definir, escolher os puzzles de acordo com as tarefas, e a 
        quantidade realizada das mesmas*/

        if (ehTileInicial == true)
        {
            rand = 0;

        }
        else
        {
            rand = Random.Range(0, 4);
        }

        // Debug.Log(rand);

        GameObject cloneTile = Instantiate(puzzlesColeta[rand], empty.position, Quaternion.identity, transform.parent);


    }

    public void instanciarPuzzlesFaseMontagem()
    {
        //rand = Random.Range(0, 4);
        GameObject cloneTile = Instantiate(puzzlesMontagem[0], empty.position, Quaternion.identity, transform.parent);
    }



}
