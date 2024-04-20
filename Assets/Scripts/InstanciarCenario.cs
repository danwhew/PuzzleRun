using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] puzzlesColeta;
    public GameObject[] puzzlesMontagem;
    public Transform anchor;
    public Transform empty;

    public bool ehTileInicial;

    public int rand;

    //teste

    public bool[] coletados = new bool [5];

    /*pensar em um esquema pra saber se tal puzzle ja foi instanciado ou nao
    apesar de serem aleatorios, nao podem ser repetidos*/

    private void OnEnable()
    {
        //nao  sei pq mas o singleton nao funciona aqui, entao passei o instanciarPuzzles pro start
        //ainda nao me faz sentido estar funcionando mas..

    }

    private void Start()
    {

        //apos instanciar 5 puzzles da fase de coleta, trocar a fase pra proxima

        //criar um metodo pra instanciar puzzles da fase de montagem



        if (GameController.instance.contador < 3)
        {
            Debug.Log("fase1");
            if (GameController.instance.fase == 0)
            {
                instanciarPuzzlesFaseColeta();
                GameController.instance.contador++;
            }

        }
        else if (GameController.instance.contador > 3 && GameController.instance.contador < 6)
        {
            GameController.instance.fase = 1;

            if (GameController.instance.fase == 1)
            {
                Debug.Log("fase2");
            }
        }
        else
        {
            Debug.Log("fase3");
        }
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


}
