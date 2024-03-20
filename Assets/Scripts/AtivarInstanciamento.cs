using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarInstanciamento : MonoBehaviour
{
    public InstanciarCenario pai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            pai.instanciar();
        }
    }
}
