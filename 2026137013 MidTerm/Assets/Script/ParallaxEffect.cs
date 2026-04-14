using Unity.Mathematics;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera Camera;
    public Transform followTarget;
    


    // 시작지점
    Vector2 startPos;

    // 배경 z
    float startingZ;


    Vector2 camMoveStart => (Vector2)Camera.transform.position - startPos;

    float z_DisFromTarget => transform.position.z - followTarget.transform.position.z;


    float clippingPlane => (Camera.transform.position.z + (z_DisFromTarget > 0 ? Camera.farClipPlane : Camera.nearClipPlane ));

    float parallaxFactor => Mathf.Abs(z_DisFromTarget) / clippingPlane;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startPos + camMoveStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x,newPosition.y, startingZ);


    }
}
