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
                    if (GameController.instance.contador > 2)
                    {
                        GameController.instance.fase = 2;
                        instanciarAlteradorDeFases();
                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > 2)
                    {
                        GameController.instance.fase = 1;
                        instanciarAlteradorDeFases();
                        GameController.instance.round = 2;
                        GameController.instance.contador = 1;
                    }

                }
            }

            if(GameController.instance.round  == 2)
            {
                if (GameController.instance.fase == 1)
                {
                    if (GameController.instance.contador > 2)
                    {
                        GameController.instance.fase = 2;
                        instanciarAlteradorDeFases();
                        GameController.instance.contador = 1;
                    }

                }

                if (GameController.instance.fase == 2)
                {
                    if (GameController.instance.contador > 2)
                    {
                        GameController.instance.fase = 1;
                        instanciarAlteradorDeFases();
                        GameController.instance.round = 1;
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
