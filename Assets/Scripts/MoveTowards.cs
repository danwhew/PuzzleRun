using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 1f;
    public float offset;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position  = new Vector3( player.position.x * offset, transform.position.y, transform.position.z);
        transform.Translate(0,0,speed * Time.deltaTime,Space.World);
    }

    
}
