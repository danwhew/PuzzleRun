using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public bool[] conquista = new bool[5];
    // 0 - daniel
    // 1 - pedro
    // 2 - leo
    // 3 - fellipe
    // 4 - yanka

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        loadScore();
    }

    public void loadScore()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/save.txt");

        JsonRank j = JsonUtility.FromJson<JsonRank>(json);

        string teste = JsonUtility.ToJson(j,true);
        Debug.Log(teste);
        
        
    }

    public void atualizarScore()
    {
        JsonRank j = new JsonRank();

        for (int i = 0; i < 3; i++)
        {
            if (j.score[i] < GameController.instance.score)
            {

                j.score[i] = GameController.instance.score;
                break;
            }
        }

     


        

        string json = JsonUtility.ToJson(j, true);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }
}
