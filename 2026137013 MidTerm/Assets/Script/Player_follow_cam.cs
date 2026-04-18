using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player_follow_cam : MonoBehaviour
{

    public Transform player;
    public float cameraOffset = -10.0f;
    public float carmeraHeight = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y + carmeraHeight, cameraOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, 2*Time.deltaTime); 
    }
}
