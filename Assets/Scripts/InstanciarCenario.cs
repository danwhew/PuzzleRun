using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InstanciarCenario : MonoBehaviour
{



    public Transform anchorPosTile; //pos do tile base
    public Transform posPuzzle; //pos puzzle



    public void initTeste()
    {

        if (GameController.instance.fase == 1)
        {
            GameController.instance.indexPuzzlesMontagem = 0;
            GameController.instance.indexPuzzlesEntrega = 0;
            instanciarPuzzlesFaseColeta();

        }
        else if (GameController.instance.fase == 2)
        {
            GameController.instance.indexPuzzlesColeta = 0;
            GameController.instance.indexPuzzlesEntrega = 0;
            instanciarPuzzlesFaseMontagem();

        }
        else if(GameController.instance.fase == 3)
        {
            GameController.instance.indexPuzzlesMontagem = 0;
            GameController.instance.indexPuzzlesColeta = 0;
            instanciarPuzzlesFaseEntrega();
        }


    }

    //instancia o chao base
    public void instanciar()
    {

        if (GameController.instance.passou2 == 1)
        {
            GameObject teste;

            teste = Pool.poolerInstance.getTileBase();

            teste.transform.position = anchorPosTile.position;

            teste.SetActive(true);


        }
        else if(GameController.instance.passou2 == 2)
        {

            GameObject teste;

            teste = Pool.poolerInstance.getTileBaseF2();

            teste.transform.position = anchorPosTile.position;

            teste.SetActive(true);
        }
        else if (GameController.instance.passou2 == 3)
        {
            GameObject teste;

            teste = Pool.poolerInstance.getTileBaseF3();

            teste.transform.position = anchorPosTile.position;

            teste.SetActive(true);
        }

    }

    //instancia puzzles da fase de coleta 
    public void instanciarPuzzlesFaseColeta()
    {



        GameObject clone = Pool.poolerInstance.puzzlesColeta[GameController.instance.indexPuzzlesColeta];

        clone.transform.position = posPuzzle.transform.position;
        clone.SetActive(true);

        GameController.instance.indexPuzzlesColeta++;


    }

    public void instanciarPuzzlesFaseMontagem()
    {



        GameObject clone = Pool.poolerInstance.puzzlesMontagem[GameController.instance.indexPuzzlesMontagem];
        clone.transform.position = posPuzzle.transform.position;
        clone.SetActive(true);

        GameController.instance.indexPuzzlesMontagem++;



    }
    public void instanciarPuzzlesFaseEntrega()
    {
        GameObject clone = Pool.poolerInstance.puzzlesEntrega[GameController.instance.indexPuzzlesEntrega];
        clone.transform.position = posPuzzle.transform.position;
        clone.SetActive(true);
        GameController.instance.indexPuzzlesEntrega++;

    }
}
