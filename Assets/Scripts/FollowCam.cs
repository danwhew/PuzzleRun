using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    

    void Update()
    {

        
        transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);


    }
}
