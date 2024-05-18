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
            GameController.instance.contador++; //isso deixa aqui

            if (GameController.instance.contador == 4) //quantidade de tiles fase1
            {
                GameController.instance.fase = 2;
                instanciarAlteradorDeFases();
            }
        }

    }
        public void instanciarAlteradorDeFases()
        {
            GameObject cloneTile = Instantiate(alteraFase, posAlteradorDeFase.position, Quaternion.identity, transform.parent);
        }
}
