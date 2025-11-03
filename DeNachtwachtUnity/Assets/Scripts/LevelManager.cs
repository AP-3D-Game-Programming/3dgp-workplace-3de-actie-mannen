using UnityEditor.SearchService;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject startPosition;
    [SerializeField] GameObject cubePosition;

    public void prepareLevel(GameObject player, GameObject cube, GameObject start)
    {
        player.transform.position = startPosition.transform.position;
        cube.transform.position = cubePosition.transform.position;
        start.transform.position = startPosition.transform.position;
    }
}
