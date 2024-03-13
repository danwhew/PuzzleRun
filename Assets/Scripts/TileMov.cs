using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMov : MonoBehaviour
{
    public Transform anchor;
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            instanciarTiles();
        }
    }

    void instanciarTiles()
    {
        GameObject clone = Instantiate(gameObject, anchor.position, Quaternion.identity, transform.parent);
        Destroy(clone, 10f);
    }
}
