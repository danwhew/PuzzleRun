using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Pool : MonoBehaviour
{

    public static Pool poolerInstance;

    public GameObject tileBase;
    public GameObject trocadorFases;
    GameObject tmp;
    public int maxTilesBase = 6;


    [Header("Possiveis Puzzles")]
    [Header("Fase 1")]
    [Header("Round 1")]

    public GameObject[] puzzlesQueijo;
    public GameObject[] puzzlesTomate;
    public GameObject[] puzzlesFarinha;
    [Header("Round 2")]
    public GameObject[] puzzlesPeperoni;
    public GameObject[] puzzlesCogumelo;

    [Header("Fase 2")]
    [Header("Round 1")]
    public GameObject[] puzzlesAbrirMassa;
    public GameObject[] puzzlesAssarPizza;
    public GameObject[] puzzlesCortarTomate;
    public GameObject[] puzzlesCortarQueijo;
    [Header("Round 2")]
    public GameObject[] puzzlesCortarPeperoni;
    public GameObject[] puzzlesCortarCogumelo;

    [Header("Puzzles Definitivos")]
    [Header("Fase 1")]
    public GameObject[] puzzlesColeta = new GameObject[5];
    [Header("Fase 2")]
    public GameObject[] puzzlesMontagem = new GameObject[6];
    [Header("Fase 3")]


    public bool ehPeperoni;

    public List<GameObject> listaTilesBases = new List<GameObject>();
    public GameObject[] trocadoresFases = new GameObject[6];

    void Awake()
    {


        poolerInstance = this;

        for (int i = 0; i < maxTilesBase; i++)
        {

            tmp = Instantiate(tileBase);
            tmp.SetActive(false);
            listaTilesBases.Add(tmp);

        }

        for (int i = 0; i < trocadoresFases.Length; i++)
        {

            tmp = Instantiate(trocadorFases);
            tmp.SetActive(false);
            trocadoresFases[i] = tmp;

        }

    }


    private void Start()
    {

        loadPuzzlesRound();
    }

    public void loadPuzzlesRound()
    {

        if (GameController.instance.round == 1)
        {
            puzzlesColeta[0] = puzzlesQueijo[Random.Range(0, puzzlesQueijo.Length)];
            puzzlesColeta[1] = puzzlesTomate[Random.Range(0, puzzlesTomate.Length)];
            puzzlesColeta[2] = puzzlesFarinha[Random.Range(0, puzzlesTomate.Length)];

            if (ehPeperoni)
            {
                puzzlesColeta[3] = puzzlesPeperoni[Random.Range(0, puzzlesPeperoni.Length)];
                puzzlesMontagem[4] = puzzlesCortarPeperoni[Random.Range(0, puzzlesCortarPeperoni.Length)];
            }
            else
            {
                puzzlesColeta[3] = puzzlesCogumelo[Random.Range(0, puzzlesCogumelo.Length)];
                puzzlesMontagem[4] = puzzlesCortarCogumelo[Random.Range(0, puzzlesCortarCogumelo.Length)];
            }

            puzzlesMontagem[0] = puzzlesAbrirMassa[Random.Range(0, puzzlesAbrirMassa.Length)];
            puzzlesMontagem[1] = puzzlesAssarPizza[Random.Range(0, puzzlesAssarPizza.Length)];
            puzzlesMontagem[2] = puzzlesCortarTomate[Random.Range(0, puzzlesCortarTomate.Length)];
            puzzlesMontagem[3] = puzzlesCortarQueijo[Random.Range(0, puzzlesCortarQueijo.Length)];
        }

    }

    public GameObject getTileBase()
    {
        for (int i = 0; i < 6; i++)
        {

            if (!listaTilesBases[i].activeInHierarchy)
            {
                return listaTilesBases[i];
            }

        }
        return null;
    }

    public GameObject getAlteradorDeFases()
    {
        for (int i = 0; i < trocadoresFases.Length; i++)
        {

            if (!trocadoresFases[i].activeInHierarchy)
            {
                return trocadoresFases[i];
            }

        }
        return null;
    }

    public GameObject getPuzzleColeta(int index)
    {
        return puzzlesColeta[index];
    }


    public void preInstanciar(GameObject[] objects)
    {

        for (int i = 0; i < objects.Length; i++)
        {
            GameObject tmpTeste;

            tmpTeste = Instantiate(objects[i]);
            tmpTeste.SetActive(false);
        }

    }

}
