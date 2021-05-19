using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public GameObject Light;

    public bool LightActive;
    // Start is called before the first frame update
    void Start()
    {
        Light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            
        {
            FindObjectOfType<AudioManager>().Play("FlashlightClick");
            LightActive = !LightActive;
            if (LightActive)
            {
                FlashlightActice();
            }

            if (!LightActive)
            {
                FlashlightInactive();
            }
        }
    }

    void FlashlightActice()
    {
        Light.SetActive(true);
    }

    void FlashlightInactive()
    {
        Light.SetActive(false);   
    }
}
