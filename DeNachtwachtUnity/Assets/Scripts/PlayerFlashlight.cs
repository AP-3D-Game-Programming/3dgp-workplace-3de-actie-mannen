using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerFlashlight : MonoBehaviour
{
    //the flashlight here is not the flashlight prefab as a whole, but only the light object itself
    [SerializeField] GameObject light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }
    }

    void ToggleLight()
    {
        if (light.activeSelf)
        {
            light.SetActive(false);
        } 
        else
        {
            light.SetActive(true);
        }
    }
}
