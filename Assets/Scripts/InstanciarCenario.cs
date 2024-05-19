using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InstanciarCenario : MonoBehaviour
{



    public GameObject[] tiles;

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


    public Transform anchorPosTile; //pos do tile base
    public Transform posPuzzle; //pos puzzle



    public bool jaDefiniuOsDefinitivos = false;

    public int indexPuzzlesColeta = 0;
    public int indexPuzzlesMontagem = 0;



    public  void initTeste()
    {

        if (jaDefiniuOsDefinitivos == false)
        {

            puzzlesColeta[0] = puzzlesQueijo[Random.Range(0, puzzlesQueijo.Length)];
            puzzlesColeta[1] = puzzlesTomate[Random.Range(0, puzzlesTomate.Length)];
            puzzlesColeta[2] = puzzlesFarinha[Random.Range(0, puzzlesTomate.Length)];
            puzzlesColeta[3] = puzzlesPeperoni[Random.Range(0, puzzlesPeperoni.Length)];
            puzzlesColeta[4] = puzzlesCogumelo[Random.Range(0, puzzlesCogumelo.Length)];


            puzzlesMontagem[0] = puzzlesAbrirMassa[Random.Range(0, puzzlesAbrirMassa.Length)];
            puzzlesMontagem[1] = puzzlesAssarPizza[Random.Range(0, puzzlesAssarPizza.Length)];
            puzzlesMontagem[2] = puzzlesCortarTomate[Random.Range(0, puzzlesCortarTomate.Length)];
            puzzlesMontagem[3] = puzzlesCortarQueijo[Random.Range(0, puzzlesCortarQueijo.Length)];
            puzzlesMontagem[4] = puzzlesCortarPeperoni[Random.Range(0, puzzlesCortarPeperoni.Length)];
            puzzlesMontagem[5] = puzzlesCortarCogumelo[Random.Range(0, puzzlesCortarCogumelo.Length)];

            jaDefiniuOsDefinitivos = true;
        }

       

            if (GameController.instance.fase == 1)
            {
                instanciarPuzzlesFaseColeta();

            }
            else if (GameController.instance.fase == 2)
            {
                instanciarPuzzlesFaseMontagem();

            }
        

    }

    //instancia o chao base
    public void instanciar()
    {
        GameObject teste;

        teste = Pool.poolerInstance.getTileBase();

        teste.transform.position = anchorPosTile.position;

        teste.SetActive(true);

        //GameObject cloneTile = Instantiate(tiles[0], anchorPosTile.position, Quaternion.identity, transform.parent);

    }


    //instancia puzzles da fase de coleta 
    public void instanciarPuzzlesFaseColeta()
    {

        GameObject cloneTile = Instantiate(puzzlesColeta[0], posPuzzle.position, Quaternion.identity, transform.parent);


        //  indexPuzzlesColeta++;


    }

    public void instanciarPuzzlesFaseMontagem()
    {

        GameObject cloneTile = Instantiate(puzzlesMontagem[0], posPuzzle.position, Quaternion.identity, transform.parent);

        // indexPuzzlesMontagem++;



    }
    public void instanciarPuzzlesFaseEntrega()
    {

        //instanciando puzzles da fase de entrega

    }
}
