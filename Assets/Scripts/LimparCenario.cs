using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimparCenario : MonoBehaviour
{
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TileBase"))
        {
            other.gameObject.SetActive(false);
           

        }

        if (other.CompareTag("NovaFase"))
        {
            other.transform.parent.gameObject.SetActive(false);


        }

        if (other.CompareTag("Puzzle"))
        {
            Destroy(other.gameObject);


        }




    }
}
