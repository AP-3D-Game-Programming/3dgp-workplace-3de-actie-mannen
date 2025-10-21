using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    //Offset values
    [SerializeField] float upOffset = 15;

    // Update is called once per frame
    void LateUpdate()
    {
        //Have camera look down at player
        transform.rotation = Quaternion.Euler(90, 90, 0);

        //Have camera remain above player
        transform.position = player.transform.position + player.transform.up * upOffset;
    }
}
