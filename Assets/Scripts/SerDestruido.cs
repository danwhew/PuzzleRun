using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerDestruido : MonoBehaviour
{
    //script pro tile ser destruido apos sair da camera

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer"))
        {
            Destroy(gameObject);
        }


    }

}
