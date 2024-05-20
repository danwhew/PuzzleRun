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
                    if (GameController.instance.contador > 3)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;

                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > 4)
                    {
                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 2;
                        GameController.instance.contador = 1;
                      
                    }

                }
            }

            if(GameController.instance.round  == 2)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador > 4)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > 5)
                    {
                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        GameController.instance.round = 3;
                        GameController.instance.contador = 1;
                    }

                }
            }

            if (GameController.instance.round == 3)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador > 4)
                    {
                        GameController.instance.fase = 2;
                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > 5)
                    {
                        Pool.poolerInstance.loadPuzzlesRound();
                        GameController.instance.fase = 1;
                        GameController.instance.indexPuzzlesColeta = 0;
                        GameController.instance.indexPuzzlesMontagem = 0;
                        
                        GameController.instance.contador = 1;
                    }

                }
            }

        }

    }
    public void instanciarAlteradorDeFases()
    {
        GameObject cloneTile = Instantiate(alteraFase, posAlteradorDeFase.position, Quaternion.identity, transform.parent);
    }
}
