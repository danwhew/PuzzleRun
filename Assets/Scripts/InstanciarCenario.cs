using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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
            instanciarPuzzlesFaseColeta();

        }
        else if (GameController.instance.fase == 2)
        {
            instanciarPuzzlesFaseMontagem();

        }


    }

    //instancia o chao base
    public void instanciar()
    {
        GameObject teste;

        teste = Pool.poolerInstance.getTileBase();

        teste.transform.position = anchorPosTile.position;

        teste.SetActive(true);

        //GameObject cloneTile = Instantiate(tiles[0], anchorPosTile.position, Quaternion.identity, transform.parent);

    }


    //instancia puzzles da fase de coleta 
    public void instanciarPuzzlesFaseColeta()
    {
        
        //GameObject cloneTile = Instantiate(puzzlesColeta[0], posPuzzle.position, Quaternion.identity, transform.parent);

        GameObject clone = Pool.poolerInstance.puzzlesColeta[GameController.instance.indexPuzzlesColeta];

        Instantiate(clone, posPuzzle.position, Quaternion.identity, transform.parent);

        GameController.instance.indexPuzzlesColeta++;
    }

    public void instanciarPuzzlesFaseMontagem()
    {
        //GameObject cloneTile = Instantiate(puzzlesMontagem[0], posPuzzle.position, Quaternion.identity, transform.parent);

        GameObject clone = Pool.poolerInstance.puzzlesMontagem[GameController.instance.indexPuzzlesMontagem];

        Instantiate(clone, posPuzzle.position, Quaternion.identity, transform.parent);

        GameController.instance.indexPuzzlesMontagem++;
      


    }
    public void instanciarPuzzlesFaseEntrega()
    {

        //instanciando puzzles da fase de entrega

    }
}
