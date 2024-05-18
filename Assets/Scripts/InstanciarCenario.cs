using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] puzzlesColeta;
    public GameObject[] puzzlesMontagem;


    public Transform anchorPosTile; //pos do tile base
    public Transform posPuzzle; //pos puzzle

    public bool podeQueijo = true;
    public bool podeTomate = true;

    public int rand;
    public int randMax = 4;

    private void Start()
    {

        //apos instanciar 5 puzzles da fase de coleta, trocar a fase pra proxima


        if (GameController.instance.fase == 1)
        {
            instanciarPuzzlesFaseColeta();
        }

        else
        {
            instanciarPuzzlesFaseMontagem();
        }


    }

    //instancia o chao base, talvez a textura possa variar dependendo da fase
    public void instanciar()
    {
        if(GameController.instance.fase == 1) //fase1
        {
        GameObject cloneTile = Instantiate(tiles[0], anchorPosTile.position, Quaternion.identity, transform.parent);

        }else if (GameController.instance.fase == 2) //fase2
        {
            GameObject cloneTile = Instantiate(tiles[0], anchorPosTile.position, Quaternion.identity, transform.parent);
        }
        else //fase3
        {
            GameObject cloneTile = Instantiate(tiles[0], anchorPosTile.position, Quaternion.identity, transform.parent);
        }

    }


    //instancia puzzles da fase de coleta em cima
    public void instanciarPuzzlesFaseColeta()
    {
        
        if(podeQueijo && podeTomate)
        {
        rand = Random.Range(0, 2);
        }

        if(podeQueijo)
        {
        rand = Random.Range(0, 1);

        }
        if (podeTomate)
        {
        rand = Random.Range(1,2);

        }

        if(podeQueijo == true || podeTomate == true)
        {
        GameObject cloneTile = Instantiate(puzzlesColeta[rand], posPuzzle.position, Quaternion.identity, transform.parent);

        }

        if(rand == 0)
        {
            podeQueijo = false;
        }

        if(rand == 1)
        {
            podeTomate = false;
        }
        


    }

    public void instanciarPuzzlesFaseMontagem()
    {
        //rand = Random.Range(0, 4);
        GameObject cloneTile = Instantiate(puzzlesMontagem[0], posPuzzle.position, Quaternion.identity, transform.parent);
    }




}
