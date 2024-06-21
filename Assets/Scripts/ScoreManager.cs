using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public bool[] conquista = new bool[5];
    // 0 - daniel
    // 1 - pedro
    // 2 - leo
    // 3 - fellipe
    // 4 - yanka

    public GameObject[] verificadores;
    public TMP_Text[] scoreText;


    public static ScoreManager instance;

    JsonRank j = new JsonRank();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/save.json") == false)
        {
            File.WriteAllText(Application.persistentDataPath + "/save.json", JsonUtility.ToJson(j));

        }


        load();

        Scene teste = SceneManager.GetActiveScene();
        if (teste.buildIndex == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                scoreText[i].text = j.score[i].ToString();
            }

            for (int i = 0; i < 5; i++)
            {
                conquista[i] = j.conquista[i];
            }


            Image imgtemp;
            for (int i = 0; i < 5; i++)
            {
                imgtemp = verificadores[i].GetComponent<Image>();

                if (conquista[i] == true)
                {
                    imgtemp.color = Color.green;


                }
                else
                {
                    imgtemp.color = Color.red;
                }

            }
        }
    }

    public void load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/save.json");

        j = JsonUtility.FromJson<JsonRank>(json);

        string teste = JsonUtility.ToJson(j, true);
        //Debug.Log(teste);


    }

    public void atualizarConquistas()
    {


        for (int i = 0; i < 5; i++)
        {
            if (conquista[i] == true)
            {
                j.conquista[i] = true;

            }
        }

        string json = JsonUtility.ToJson(j, true);
       // Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void atualizarScore()
    {


        if (j.score[2] == 0)
        {
            for (int i = 0; i < 3; i++)
            {

                if (j.score[i] == 0)
                {
                    j.score[i] = GameController.instance.score;
                    break;
                }



            }
        }
        else
        {
            for (int i = 2; i >= 0; i--)
            {
                if (j.score[i] < GameController.instance.score)
                {
                    j.score[i] = GameController.instance.score;
                    break;
                }


            }

        }



        for (int i = 0; i < 3; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                if (k < 2)
                {
                    if (j.score[k] < j.score[k + 1])
                    {
                        int aux;
                        aux = j.score[k];
                        j.score[k] = j.score[k + 1];
                        j.score[k + 1] = aux;

                    }

                }
            }
        }

        string json = JsonUtility.ToJson(j, true);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }
}
