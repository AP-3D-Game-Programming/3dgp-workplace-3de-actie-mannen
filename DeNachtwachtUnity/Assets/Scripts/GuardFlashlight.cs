using UnityEngine;

public class GuardFlashlight : MonoBehaviour
{
    public Light spotlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Patrol>().PlayerInSight)
        {
            spotlight.color = Color.red;
        }
        else
        {
            spotlight.color = Color.white;
        }
    }

    //bool PlayerInSpotlight()
    //{
    //    Vector3 lightToPlayer = player.position - spotlight.transform.position;

    //    if (lightToPlayer.magnitude > spotlight.range)
    //    {
    //        return false;
    //    }

    //    float angleToPlayer = Vector3.Angle(spotlight.transform.forward, lightToPlayer);
    //    if (angleToPlayer > spotlight.spotAngle / 2f)
    //    {
    //        return false;
    //    }

    //    if (Physics.Raycast(spotlight.transform.position, lightToPlayer.normalized, out RaycastHit hit, spotlight.range))
    //    {
    //        if (!hit.collider.CompareTag("Player"))
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}
}
