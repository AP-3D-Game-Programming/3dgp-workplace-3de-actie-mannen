using UnityEngine;

public class VictoryCheck : MonoBehaviour
{
    public GameObject prize;
    public GameObject start;
    private bool hasPrize = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(start) && hasPrize)
        {
            Debug.Log("You win!");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(prize) && Input.GetKeyDown(KeyCode.E))
        {
            hasPrize = true;
            prize.SetActive(false);
        }
    }
}
