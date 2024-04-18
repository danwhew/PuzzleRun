using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheat : MonoBehaviour
{
    Player player; 

    void Start()
    {
        player = GetComponent<Player>(); 
    }

    void Update()
    {
        // Ativando o cheat de bateria infinita quando cinco dedos tocarem na tela
        if (Input.touchCount == 5)
        {
            player.bateria = Mathf.Infinity; 
        }

        // Ativando o cheat de evitar atordoamento
        if (Input.touchCount == 5)
        {
            player.podeAndar = true; 
            player.podeAndarTimer = 0; 
        }
    }
}
