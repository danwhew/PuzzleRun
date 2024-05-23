using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("UI")]
    public TMP_Text[] scoreText;
    public TMP_Text faseText;
    public TMP_Text roundText;
    public GameObject derrotaPainel;
    public GameObject menuPausa;

    [Header("Player")]
    public Player player;
    public Slider sliderBateria;

    [Header("Jogo")]

    public bool pausado = false;
    public int score;
    public int contador = 0;
    public int fase = 1;
    public int round = 1;


    public int quantidadePuzzlesF1R1 = 3;
    public int quantidadePuzzlesF2R1 = 4;
    public int quantidadePuzzlesF1R2 = 4;
    public int quantidadePuzzlesF2R2 = 5;
    public int quantidadePuzzlesF1R3 = 4;
    public int quantidadePuzzlesF2R3 = 5;
    //
    

    public int indexPuzzlesColeta;
    public int indexPuzzlesMontagem;

    public int extras;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }



    }

    void Start()
    {
        Time.timeScale = 1.0f;




        if (faseText != null)
        {
            atualizarFase();
            atualizarRound();
        }




    }

    void Update()
    {
        if (sliderBateria != null)
        {
            sliderBateria.value = player.bateria;
        }

       

    }



    public void addScore(int quantidade)
    {

        score += quantidade;

        if (score < 0)
        {
            score = 0;
        }


        for (int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = "Score: " + score.ToString();

        }
    }

    public void atualizarFase()
    {

        faseText.text = "Fase " +  player.faseEuTo.ToString();
    }

    public void atualizarRound()
    {
        if (player.roundEuTo <= 2)
        {
            roundText.text = "Round " + player.roundEuTo.ToString();
            

        }
        else
        {
            roundText.text = "Round Bonus " + (player.roundEuTo + extras/2).ToString();
            extras++;
            
        }
    }

    public static void resetar()
    {
        SceneManager.LoadScene(1);
    }

    public void derrota()
    {
        derrotaPainel.SetActive(true);
        Time.timeScale = 0;
    }

    public void pausar()
    {
        if (pausado == false)
        {
            menuPausa.SetActive(true);
            pausado = true;
            Time.timeScale = 0;
        }
        else
        {
            menuPausa.SetActive(false);
            pausado = false;
            Time.timeScale = 1;
        }

    }

    public void MenuInicial()
    {
        SceneManager.LoadScene(0);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(1);
    }

}
