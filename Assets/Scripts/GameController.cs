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
    public GameObject Tasks;

    [Header("Player")]
    public Player player;
    public Slider sliderBateria;

    [Header("Jogo")]

    public bool pausado = false;
    public int score;
    public int contador = 0;
    public int fase = 1;
    public int round = 1;
    public bool passou = false; //pra atualizar a fase/round do player atraves do DectarEntrada
    public int passou2 = 1; //serve pra mudar o tipo de tile base instanciado

    //serve pra mudar o tipo de puzzle instanciado
    public int indexPuzzlesColeta;
    public int indexPuzzlesMontagem;
    public int indexPuzzlesEntrega;

    [Header("Bateria")]
    public float timerBateria;
    public bool podeAtivarTimerBateria;
    public float tempoLimiteBateria = 8f;
    public bool podeInstanciarBateria;



    [Header("Quantidade Puzzles")]
    public int quantidadePuzzlesF1R1 = 3;
    public int quantidadePuzzlesF2R1 = 4;
    public int quantidadePuzzlesF3R1 = 3;
    public int quantidadePuzzlesF1R2 = 4;
    public int quantidadePuzzlesF2R2 = 5;
    public int quantidadePuzzlesF3R2 = 3;
    public int quantidadePuzzlesF1R3 = 4;
    public int quantidadePuzzlesF2R3 = 5;
    public int quantidadePuzzlesF3R3 = 3;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] audios;

    public int contadorPizzasFeitas;



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


        if (podeAtivarTimerBateria == true)
        {
            timerBateria += Time.deltaTime;

        }
        else
        {
            timerBateria = 0;
        }


    }

    public void configFases()
    {
        if (round == 1)
        {

            if (fase == 1)
            {


                if (contador == quantidadePuzzlesF1R1)
                {
                    passou = true;
                    passou2 = 2;


                }

                if (contador > quantidadePuzzlesF1R1)
                {
                    fase = 2;
                    contador = 1;


                }

            }

            if (fase == 2)
            {

                if (contador == quantidadePuzzlesF2R1)
                {
                    passou = true;
                    passou2 = 3;


                }


                if (contador > quantidadePuzzlesF2R1)
                {

                    fase = 3;
                    contador = 1;
                }

            }
            if (fase == 3)
            {
                if (contador == quantidadePuzzlesF3R1)
                {
                    passou = true;
                    passou2 = 1;


                }
                if (contador > quantidadePuzzlesF3R1)
                {

                    Pool.poolerInstance.loadPuzzlesRound();
                    fase = 1;

                    indexPuzzlesColeta = 0;
                    indexPuzzlesMontagem = 0;
                    indexPuzzlesEntrega = 0;
                    round = 2;
                    contador = 1;
                }

            }
        }
        else if (round == 2)
        {
            if (fase == 1)
            {
                if (contador == quantidadePuzzlesF1R2)
                {
                    passou = true;
                    passou2 = 2;


                }


                if (contador > quantidadePuzzlesF1R2)
                {
                    fase = 2;
                    contador = 1;
                }

            }

            if (fase == 2)
            {
                if (contador == quantidadePuzzlesF2R2)
                {
                    passou = true;
                    passou2 = 3;


                }


                if (contador > quantidadePuzzlesF2R2)
                {
                    fase = 3;
                    contador = 1;

                }

            }
            if (fase == 3)
            {
                if (contador == quantidadePuzzlesF3R2)
                {
                    passou = true;
                    passou2 = 1;


                }

                if (contador > quantidadePuzzlesF3R2)
                {
                    Pool.poolerInstance.loadPuzzlesRound();
                    fase = 1;
                    indexPuzzlesColeta = 0;
                    indexPuzzlesMontagem = 0;
                    indexPuzzlesEntrega = 0;
                    round = 3;


                    contador = 1;
                }


            }
        }
        else if (round >= 3)
        {
            if (fase == 1)
            {
                if (contador == quantidadePuzzlesF1R3)
                {
                    passou = true;
                    passou2 = 2;

                }


                if (contador > quantidadePuzzlesF1R3)
                {
                    fase = 2;
                    contador = 1;
                }

            }

            if (fase == 2)
            {
                if (contador == quantidadePuzzlesF2R3)
                {
                    passou = true;
                    passou2 = 3;


                }

                if (contador > quantidadePuzzlesF2R3)
                {
                    fase = 3;
                    contador = 1;

                }


            }
            if (fase == 3)
            {
                if (contador == quantidadePuzzlesF3R3)
                {
                    passou = true;
                    passou2 = 1;
                }

                if(contador > quantidadePuzzlesF3R3)
                {
                    Pool.poolerInstance.loadPuzzlesRound();
                    fase = 1;
                    indexPuzzlesColeta = 0;
                    indexPuzzlesMontagem = 0;
                    indexPuzzlesEntrega = 0;
                    round++;

                    contador = 1;
                }
            }
        }
    }

    public void audioButtons()
    {
        audioSource.PlayOneShot(audios[1]);
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
        if (player.roundEuTo == 1)
        {
            if (player.faseEuTo == 1)
            {
                LimparTasks();
                Tasks.transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {
                LimparTasks();
                Tasks.transform.GetChild(3).gameObject.SetActive(true);
            }

        }
        else
        {

            if (player.faseEuTo == 1)
            {

                if (player.toPeperoni == true)
                {

                    LimparTasks();
                    Tasks.transform.GetChild(1).gameObject.SetActive(true);

                }
                else
                {
                    LimparTasks();
                    Tasks.transform.GetChild(2).gameObject.SetActive(true);

                }

            }
            else
            {
                if (player.toPeperoni == true)
                {
                    LimparTasks();
                    Tasks.transform.GetChild(4).gameObject.SetActive(true);

                }
                else
                {
                    LimparTasks();
                    Tasks.transform.GetChild(5).gameObject.SetActive(true);

                }
            }
        }

        faseText.text = "Fase " + player.faseEuTo.ToString();
    }

    public void atualizarRound()
    {
        roundText.text = "Round " + player.roundEuTo.ToString();


    }

    public void LimparTasks()
    {
        for (int i = 0; i < Tasks.transform.childCount; i++)
        {
            Tasks.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public static void resetar()
    {
        SceneManager.LoadScene(1);
    }

    public void derrota()
    {
        ScoreManager.instance.atualizarScore();
        derrotaPainel.SetActive(true);
        Debug.Log("perdi");
        Time.timeScale = 0;
    }

    public void pausar()
    {
        ScoreManager.instance.conquista[0] = true;
        Debug.Log("vc fez a conquista 0");
        ScoreManager.instance.atualizarConquistas();

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
