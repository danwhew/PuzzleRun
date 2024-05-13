using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            pai.instanciar();
            GameController.instance.contador++; //isso deixa aqui


            // essa logica posso voltar pro instanciar cenario
            if (GameController.instance.fase == 1)
            {
                if (GameController.instance.contador == 1)
                {
                    GameController.instance.fase = 2;
                    GameController.instance.contador = 0;
                }
                // talvez colocar um else instanciando o detector de mudanca de fase

            }

            if (GameController.instance.fase == 2)
            {
                if(GameController.instance.contador == 1)
                {
                    GameController.instance.fase = 1;
                    GameController.instance.contador= 0;
                }
            }
        }
    }
}
