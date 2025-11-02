using UnityEditor.SearchService;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject startPosition;
    [SerializeField] GameObject cubePosition;

    private GameObject cube;
    private GameObject player;
    void Start()
    {
        cube = GameObject.Find("Cube");
        player = GameObject.Find("Player");
    }
    public void prepareLevel()
    {
        player.transform.position = startPosition.transform.position;
        cube.transform.position = cubePosition.transform.position;
    }
}
