using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    public float speed = 1f;
    public float offset;
    public Transform player;
    public Player playerScript;
    public Vector3 dir;
    public float minSpeed;
    public float maxSpeed;
    public float maxDistance;
    public float speedFase3 = 2.3f;


    // Update is called once per frame
    void Update()
    {

        if (GameController.instance.pausaInicial == false)
        {

            dir = player.transform.position - transform.position;

            if (playerScript.faseEuTo != 3)
            {
                if (dir.magnitude > maxDistance)
                {
                    speed = maxSpeed;
                }
                else
                {
                    speed = minSpeed;
                }

            }
            else
            {
                speed = speedFase3;
            }



            //transform.position  = new Vector3( player.position.x * offset, transform.position.y, transform.position.z);
            transform.Translate(0, 0, speed * Time.deltaTime, Space.World);


        }



    }


}
