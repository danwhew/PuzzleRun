using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] puzzles;
    public Transform anchor;
    public Transform empty;

    public int rand;

    private void OnEnable()
    {
        instanciarPuzzles();

    }

    public void instanciar()
    {
        GameObject cloneTile = Instantiate(tiles[0],anchor.position,Quaternion.identity,transform.parent);
    }

    public void instanciarPuzzles()
    {
        rand = Random.Range(0,2);

        if (rand == 0)
        {
        GameObject cloneTile = Instantiate(puzzles[0], empty.position, Quaternion.identity, transform.parent);

        }
        else
        {
            GameObject cloneTile = Instantiate(puzzles[1], empty.position, Quaternion.identity, transform.parent);

        }
    }

 
}
