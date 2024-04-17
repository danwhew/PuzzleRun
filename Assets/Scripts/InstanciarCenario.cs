using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] puzzles;
    public Transform anchor;
    public Transform empty;

    public int rand;

    private void OnEnable()
    {
        //nao  sei pq mas o singleton nao funciona aqui, entao passei o instanciarPuzzles pro start
        //ainda nao me faz sentido estar funcionando mas..

    }

    private void Start()
    {

        //apos instanciar 5 puzzles da fase de coleta, trocar a fase pra proxima

        //criar um metodo pra instanciar puzzles da fase de montagem



        if(GameController.instance.contador < 5)
        {

        if (GameController.instance.fase == 0)
        {
                instanciarPuzzlesFaseColeta();
            GameController.instance.contador++;
        }

        }
    }

    public void instanciar()
    {
        GameObject cloneTile = Instantiate(tiles[0], anchor.position, Quaternion.identity, transform.parent);
    }

    public void instanciarPuzzlesFaseColeta()
    {

        /*com base no que o game controller definir, escolher os puzzles de acordo com as tarefas, e a 
        quantidade realizada das mesmas*/

            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                GameObject cloneTile = Instantiate(puzzles[0], empty.position, Quaternion.identity, transform.parent);

            }
            else
            {
                GameObject cloneTile = Instantiate(puzzles[1], empty.position, Quaternion.identity, transform.parent);

            }


       /* else
        {
            Debug.Log("cabou o round");
        }*/

    }


}
