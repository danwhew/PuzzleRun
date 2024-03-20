using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public Transform empty;
    public bool condicao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (condicao == true)
        {
            transform.position = Vector3.Lerp(transform.position, empty.position, 1f * Time.deltaTime);

        }

    }
}
