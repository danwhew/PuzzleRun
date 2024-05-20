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

    public TMP_Text[] scoreText;
    public TMP_Text faseText;
    public GameObject derrotaPainel;
    public GameObject menuPausa;
    public Player player;
    public Slider sliderBateria;

    public int contador = 0;
    public int fase = 1;
    public int round = 1;



    public Vector3 teste;

    public int score;

    public bool pausado = false;

    public int indexPuzzlesColeta;
    public int indexPuzzlesMontagem;



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
        faseText.text = "Fase " + fase.ToString();
    }

    public void atualizarRound()
    {

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
