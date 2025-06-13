using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public GameObject playerObject;
    public float cameraSmoothTime;
    public float cameraMoveThreshold = 0.05f;
    public float xVelocity;

    public float yVelocity;
    public float cameraYThreshold = 20f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerObject == null)
        {
            // Try to find the player again if it's missing
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerObject = player;
            }
            return;
        }

        Vector3 playerPosition = playerObject.transform.position;
        Vector3 cameraPosition = transform.position;

        // check to see if the camera needs to move (x value)
        if (playerPosition.x > cameraPosition.x + cameraMoveThreshold || playerPosition.x < cameraPosition.x - cameraMoveThreshold)
        {
            // camera and player are far enough away, move the camera

            // set the x value
            cameraPosition.x = Mathf.SmoothDamp(cameraPosition.x, playerPosition.x, ref xVelocity, cameraSmoothTime);

        }

        // check y value
        if (playerPosition.y > cameraPosition.y + cameraYThreshold || playerPosition.y < cameraPosition.y - cameraYThreshold)
        {
            cameraPosition.y = Mathf.SmoothDamp(cameraPosition.y, playerPosition.y, ref yVelocity, cameraSmoothTime);
        }

        transform.position = cameraPosition;

    }
}
