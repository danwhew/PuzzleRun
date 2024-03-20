using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciarCenario : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] itens;
    public Transform anchor;
    public Transform[] ondeItens;

    private void OnEnable()
    {
        
        GameObject cloneItem = Instantiate(itens[0], ondeItens[0].position, Quaternion.identity);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instanciar()
    {
        GameObject cloneTile = Instantiate(tiles[0],anchor.position,Quaternion.identity,transform.parent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer"))
        {
            Destroy(gameObject);
        }
    }
}
