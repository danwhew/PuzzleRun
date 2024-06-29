using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Pool : MonoBehaviour
{

    public static Pool poolerInstance;

    public GameObject tileBase;
    public GameObject tileBaseF2;
    public GameObject tileBaseF3;
    GameObject tmp;
    public int maxTilesBase = 6;
    public int maxTilesBaseF2 = 6;
    public int maxTilesBaseF3 = 6;
    public bool ehPeperoni;

    public GameObject bateria;


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

    [Header("Fase 3")]
    public GameObject[] puzzlesPegarPizza;
    public GameObject[] puzzlesCaminhoEntrega;
    public GameObject[] puzzlesCaminhoEntrega2;
    public GameObject[] puzzlesEntregasFinais;

    [Header("Puzzles Definitivos")]

    [Header("Fase 1")]
    public GameObject[] puzzlesColeta;
    [Header("Fase 2")]
    public GameObject[] puzzlesMontagem;
    [Header("Fase 3")]
    public GameObject[] puzzlesEntrega;


    public List<GameObject> listaTilesBases = new List<GameObject>();
    public List<GameObject> listaTilesBasesF2 = new List<GameObject>();
    public List<GameObject> listaTilesBasesF3 = new List<GameObject>();

    public GameObject[] pizzas;
    public GameObject[] baterias;

    public Totem totemtemp;

    void Awake()
    {


        poolerInstance = this;

        for (int i = 0; i < maxTilesBase; i++)
        {

            tmp = Instantiate(tileBase);
            tmp.SetActive(false);
            listaTilesBases.Add(tmp);

        }
        for (int i = 0; i < maxTilesBaseF2; i++)
        {

            tmp = Instantiate(tileBaseF2);
            tmp.SetActive(false);
            listaTilesBasesF2.Add(tmp);

        }
        for (int i = 0; i < maxTilesBaseF3; i++)
        {

            tmp = Instantiate(tileBaseF3);
            tmp.SetActive(false);
            listaTilesBasesF3.Add(tmp);

        }

        for (int i = 0; i < baterias.Length; i++)
        {
            baterias[i] = bateria;
        }

        preInstanciar(baterias);

        preInstanciar(pizzas);

        preInstanciar(puzzlesQueijo);
        preInstanciar(puzzlesTomate);
        preInstanciar(puzzlesFarinha);
        preInstanciar(puzzlesPeperoni);
        preInstanciar(puzzlesCogumelo);

        preInstanciar(puzzlesAbrirMassa);
        preInstanciar(puzzlesAssarPizza);
        preInstanciar(puzzlesCortarTomate);
        preInstanciar(puzzlesCortarQueijo);
        preInstanciar(puzzlesCortarPeperoni);
        preInstanciar(puzzlesCortarCogumelo);

        preInstanciar(puzzlesPegarPizza);
        preInstanciar(puzzlesCaminhoEntrega);
        preInstanciar(puzzlesCaminhoEntrega2);
        preInstanciar(puzzlesEntregasFinais);

    }



    private void Start()
    {
        if (GameController.instance.round == 1)
        {
            loadPuzzlesRound1();

        }
        else
        {
            loadPuzzlesRound();
        }
    }

    //configurar os puzzles do segundo round pra cima
    public void loadPuzzlesRound()
    {
        int temp = Random.Range(0, 2);

        if (temp == 0)
        {
            ehPeperoni = false;
        }
        else
        {
            ehPeperoni = true;
        }

        if (totemtemp != null)
        {
            totemtemp.peperoni = ehPeperoni;
            totemtemp = null;

        }

        int[] jaGerados = new int[4] { -1, -1, -1, -1 };
        bool ehIgual = false;

        int[] jaGerados2 = new int[3] { -1, -1, -1 };
        bool ehIgual2 = false;

        for (int i = 0; i < jaGerados.Length; i++)
        {
            ehIgual = false;
            int rand;
            rand = Random.Range(0, 4);

            for (int j = 0; j < jaGerados.Length; j++)
            {

                if (rand == jaGerados[j])
                {
                    ehIgual = true;
                }



            }
            if (ehIgual == true)
            {
                i--;

            }
            else
            {
                jaGerados[i] = rand;
            }
        }

        for (int i = 0; i < jaGerados2.Length; i++)
        {
            ehIgual2 = false;
            int rand;
            rand = Random.Range(1, 4);

            for (int j = 0; j < jaGerados2.Length; j++)
            {

                if (rand == jaGerados2[j])
                {
                    ehIgual2 = true;
                }



            }
            if (ehIgual2 == true)
            {
                i--;

            }
            else
            {
                jaGerados2[i] = rand;
            }
        }

        puzzlesColeta[jaGerados[0]] = puzzlesQueijo[Random.Range(0, puzzlesQueijo.Length)];
        puzzlesColeta[jaGerados[1]] = puzzlesTomate[Random.Range(0, puzzlesTomate.Length)];
        puzzlesColeta[jaGerados[2]] = puzzlesFarinha[Random.Range(0, puzzlesFarinha.Length)];

        if (ehPeperoni)
        {
            puzzlesColeta[jaGerados[3]] = puzzlesPeperoni[Random.Range(0, puzzlesPeperoni.Length)];
            puzzlesMontagem[jaGerados2[2]] = puzzlesCortarPeperoni[Random.Range(0, puzzlesCortarPeperoni.Length)];
        }
        else
        {
            puzzlesColeta[jaGerados[3]] = puzzlesCogumelo[Random.Range(0, puzzlesCogumelo.Length)];
            puzzlesMontagem[jaGerados2[2]] = puzzlesCortarCogumelo[Random.Range(0, puzzlesCortarCogumelo.Length)];
        }

        puzzlesMontagem[0] = puzzlesAbrirMassa[Random.Range(0, puzzlesAbrirMassa.Length)];
        puzzlesMontagem[jaGerados2[0]] = puzzlesCortarTomate[Random.Range(0, puzzlesCortarTomate.Length)];
        puzzlesMontagem[jaGerados2[1]] = puzzlesCortarQueijo[Random.Range(0, puzzlesCortarQueijo.Length)];
        puzzlesMontagem[4] = puzzlesAssarPizza[Random.Range(0, puzzlesAssarPizza.Length)];

        puzzlesEntrega[0] = puzzlesPegarPizza[Random.Range(0, puzzlesPegarPizza.Length)];
        puzzlesEntrega[1] = puzzlesCaminhoEntrega[Random.Range(0, puzzlesCaminhoEntrega.Length)];
        puzzlesEntrega[2] = puzzlesCaminhoEntrega2[Random.Range(0, puzzlesCaminhoEntrega2.Length)];
        puzzlesEntrega[3] = puzzlesEntregasFinais[Random.Range(0, puzzlesEntregasFinais.Length)];
    }

    //configurar os puzzles do primeiro round, considerando que apresenta menos fases
    public void loadPuzzlesRound1()
    {

        int[] jaGerados = new int[3] { -1, -1, -1 };
        bool ehIgual = false;

        int[] jaGerados2 = new int[2] { -1, -1 };
        bool ehIgual2 = false;


        for (int i = 0; i < jaGerados.Length; i++)
        {
            ehIgual = false;
            int rand;
            rand = Random.Range(0, 3);

            for (int j = 0; j < jaGerados.Length; j++)
            {

                if (rand == jaGerados[j])
                {
                    ehIgual = true;
                    //talvez botar um break aqui e nos outros for's que fazem a mesma coisa
                }



            }
            if (ehIgual == true)
            {
                i--;

            }
            else
            {
                jaGerados[i] = rand;
            }
        }

        for (int i = 0; i < jaGerados2.Length; i++)
        {
            ehIgual2 = false;
            int rand;
            rand = Random.Range(1, 3);

            for (int j = 0; j < jaGerados2.Length; j++)
            {

                if (rand == jaGerados2[j])
                {
                    ehIgual2 = true;
                }



            }
            if (ehIgual2 == true)
            {
                i--;

            }
            else
            {
                jaGerados2[i] = rand;
            }
        }

        puzzlesColeta[jaGerados[0]] = puzzlesQueijo[Random.Range(0, puzzlesQueijo.Length)];
        puzzlesColeta[jaGerados[1]] = puzzlesTomate[Random.Range(0, puzzlesTomate.Length)];
        puzzlesColeta[jaGerados[2]] = puzzlesFarinha[Random.Range(0, puzzlesFarinha.Length)];

        puzzlesMontagem[0] = puzzlesAbrirMassa[Random.Range(0, puzzlesAbrirMassa.Length)];
        puzzlesMontagem[jaGerados2[0]] = puzzlesCortarTomate[Random.Range(0, puzzlesCortarTomate.Length)];
        puzzlesMontagem[jaGerados2[1]] = puzzlesCortarQueijo[Random.Range(0, puzzlesCortarQueijo.Length)];
        puzzlesMontagem[3] = puzzlesAssarPizza[Random.Range(0, puzzlesAssarPizza.Length)];

        puzzlesEntrega[0] = puzzlesPegarPizza[Random.Range(0, puzzlesPegarPizza.Length)];
        puzzlesEntrega[1] = puzzlesCaminhoEntrega[Random.Range(0, puzzlesCaminhoEntrega.Length)];
        puzzlesEntrega[2] = puzzlesEntregasFinais[Random.Range(0, puzzlesEntregasFinais.Length)];

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

    public GameObject getTileBaseF2()
    {
        for (int i = 0; i < 6; i++)
        {

            if (!listaTilesBasesF2[i].activeInHierarchy)
            {
                return listaTilesBasesF2[i];
            }

        }
        return null;
    }

    public GameObject getTileBaseF3()
    {
        for (int i = 0; i < 6; i++)
        {

            if (!listaTilesBasesF3[i].activeInHierarchy)
            {
                return listaTilesBasesF3[i];
            }

        }
        return null;
    }

    public GameObject getBaterias()
    {
        for (int i = 0; i < baterias.Length; i++)
        {

            if (!baterias[i].activeInHierarchy)
            {
                return baterias[i];
            }

        }
        return null;
    }

    public void preInstanciar(GameObject[] objects)
    {

        for (int i = 0; i < objects.Length; i++)
        {

            tmp = Instantiate(objects[i]);
            tmp.SetActive(false);
            objects[i] = tmp;

        }

    }

}
