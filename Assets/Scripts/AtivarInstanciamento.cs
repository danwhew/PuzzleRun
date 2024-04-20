using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            pai.instanciar();
        }
    }
}
