using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxIlumination : MonoBehaviour
{
    public GameObject BoxLight;

    public bool LightActive;
    // Start is called before the first frame update
    void Start()
    {
        BoxLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            
        {
            LightActive = !LightActive;
            if (LightActive)
            {
                BoxLightInactive();
            }

            if (!LightActive)
            {
                BoxLightActice();
            }
        }
    }

    void BoxLightActice()
    {
        BoxLight.SetActive(true);
    }

    void BoxLightInactive()
    {
        BoxLight.SetActive(false);   
    }
}