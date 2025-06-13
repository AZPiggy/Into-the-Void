using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public GameObject playerObject;

    public float cameraOffset;

    void Update()
    {
        Vector3 playerPosition = playerObject.transform.position;
        Vector3 cameraPosition = transform.position;

        cameraPosition.x = playerPosition.x + cameraOffset;

        transform.position = cameraPosition;
    }
}


//uges25
