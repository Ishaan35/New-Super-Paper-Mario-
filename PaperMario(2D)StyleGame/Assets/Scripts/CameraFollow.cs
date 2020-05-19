using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    public bool MoveWithPlayer = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(MoveWithPlayer)
        {
            offset.x = player.transform.position.x;
            offset.y = (player.transform.position.y + 2);
            offset.z = this.transform.position.z;




            this.transform.position = offset;
        }
        player = GameObject.FindGameObjectWithTag("Player");

        

    }
}
