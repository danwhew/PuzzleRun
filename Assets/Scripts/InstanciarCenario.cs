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
            instanciarPuzzlesFaseColeta();

        }
        else if (GameController.instance.fase == 2)
        {
            GameController.instance.indexPuzzlesColeta = 0;
            instanciarPuzzlesFaseMontagem();

        }


    }

    //instancia o chao base
    public void instanciar()
    {

        if (GameController.instance.passou2 == false)
        {
            GameObject teste;

            teste = Pool.poolerInstance.getTileBase();

            teste.transform.position = anchorPosTile.position;

            teste.SetActive(true);


        }
        else
        {

            GameObject teste;

            teste = Pool.poolerInstance.getTileBaseF2();

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

            //instanciando puzzles da fase de entrega

        }
    }
