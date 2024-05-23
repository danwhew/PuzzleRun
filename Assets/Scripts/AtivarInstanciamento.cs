using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;
    public Transform posAlteradorDeFase;
    public GameObject alteraFase;


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
                    
                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R1)
                    {
                        
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;

                        
                        instanciarAlteradorDeFases(2,1);

                    }

                }

                if (GameController.instance.fase == 2)
                {


                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R1)
                    {
                        //resetando
                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 2;
                        GameController.instance.contador = 1;

                        instanciarAlteradorDeFases(1,2);

                    }

                }
            }

            else if (GameController.instance.round == 2)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R2)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;
                        instanciarAlteradorDeFases(2,2);
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R2)
                    {
                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 3;
                        

                        GameController.instance.contador = 1;

                        instanciarAlteradorDeFases(1, 3);
                    }

                }
            }

            else if (GameController.instance.round >= 3)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF1R3)
                    {
                        instanciarAlteradorDeFases(2, 3);
                        GameController.instance.fase = 2;

                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > GameController.instance.quantidadePuzzlesF2R3)
                    {
                        
                        instanciarAlteradorDeFases(1,3);
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
    public void instanciarAlteradorDeFases(int fase, int round)
    {


        

        GameObject altTemp;
        altTemp = Pool.poolerInstance.getAlteradorDeFases();



        if (altTemp != null)
        {
            altTemp.transform.position = posAlteradorDeFase.position;
            AlteraFase altFaseScript = altTemp.GetComponent<AlteraFase>();
            altFaseScript.fasePlayer = fase;
            altFaseScript.roundPlayer = round;
            
            altTemp.SetActive(true);
        }

        


    }

}
