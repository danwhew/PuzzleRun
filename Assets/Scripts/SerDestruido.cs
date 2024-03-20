using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerDestruido : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer"))
        {
            Destroy(gameObject);
        }
    }
}
