using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;
    public Transform posAlteradorDeFase;


    private void OnTriggerEnter(Collider other)
    {



        if (other.CompareTag("End"))
        {


            pai.instanciar();
            pai.initTeste();




            GameController.instance.contador++;



            if (GameController.instance.round == 1)
            {

                if (GameController.instance.fase == 1)
                {
                    

                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF1R1)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = true;


                    }

                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R1)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;


                    }

                }

                if (GameController.instance.fase == 2)
                {

                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF2R1)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = false;


                    }


                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R1)
                    {
                        //cesta.Esvaziar();


                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;

                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 2;
                        GameController.instance.contador = 1;
                    }

                }
            }

            else if (GameController.instance.round == 2)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF1R2)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = true;


                    }


                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R2)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF2R2)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = false;


                    }


                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R2)
                    {
                        //  cesta.Esvaziar();

                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 3;


                        GameController.instance.contador = 1;

                    }

                }
            }

            else if (GameController.instance.round >= 3)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF1R3)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = true;

                    }


                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R3)
                    {
                        GameController.instance.fase = 2;

                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador == GameController.instance.quantidadePuzzlesF2R3)
                    {
                        GameController.instance.passou = true;
                        GameController.instance.passou2 = false;


                    }

                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R3)
                    {
                        /* cesta.Esvaziar();
                         pizza.Esvaziar();*/

                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round++;

                        GameController.instance.contador = 1;
                    }

                }
            }

        }

    }


}
