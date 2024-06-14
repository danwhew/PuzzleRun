using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class ScoreManager : MonoBehaviour
{

    public bool[] conquista = new bool[5];
    // 0 - daniel
    // 1 - pedro
    // 2 - leo
    // 3 - fellipe
    // 4 - yanka

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
        load();
    }

    public void load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/save.txt");

        j = JsonUtility.FromJson<JsonRank>(json);

        string teste = JsonUtility.ToJson(j, true);
        Debug.Log(teste);


    }

    public void atualizarConquistas()
    {


        for (int i = 0; i < 5; i++)
        {
            if (conquista[i] == true)
            {
                j.conquista[i] = true;
                break;
            }
        }

        string json = JsonUtility.ToJson(j, true);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }

    public void atualizarScore()
    {

        bool aindaTemZerado = true;



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
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }
}
