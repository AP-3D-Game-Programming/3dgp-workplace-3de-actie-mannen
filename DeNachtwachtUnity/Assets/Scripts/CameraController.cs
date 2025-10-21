using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    //Offset values
    [SerializeField] float upOffset = -15;

    // Update is called once per frame
    void LateUpdate()
    {
        //Have camera rotate in the same direction as player
        

        //Have camera remain behind player
        transform.position = player.transform.position + transform.forward * upOffset;
    }
}
