using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public TMP_Text[] scoreText;
    public GameObject derrotaPainel;
    public Player player;
    public Slider sliderBateria;

    public int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        derrotaPainel.SetActive(false);
    }

    void Update()
    {
        sliderBateria.value = player.bateria;
    }

    public void addScore()
    {
        score += 10;

        for (int i = 0; i < scoreText.Length; i++)
        {
        scoreText[i].text = "Score: " + score.ToString(); 

        }
    }

    public static void resetar()
    {
        SceneManager.LoadScene(0);
    }

    public  void derrota()
    {
        derrotaPainel.SetActive(true);
        Time.timeScale = 0;
    }
}
