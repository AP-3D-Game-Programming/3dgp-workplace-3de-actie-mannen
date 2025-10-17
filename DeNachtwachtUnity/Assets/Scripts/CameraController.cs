using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    //Offset values
    [SerializeField] float xOffset = -0.5f;
    [SerializeField] float yOffset = 1;
    [SerializeField] float zOffset = -3;

    // Update is called once per frame
    void LateUpdate()
    {
        //Have camera rotate in the same direction as player
        transform.rotation = player.transform.rotation;

        //Have camera remain behind player
        Vector3 camPos = player.transform.right * xOffset + player.transform.up * yOffset + player.transform.forward * zOffset;
        transform.position = player.transform.position + camPos;
    }
}
