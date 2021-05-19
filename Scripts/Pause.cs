using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour

{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUi;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                IsPaused();
            }
        }
    }

  public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void IsPaused()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

   public void QuitGame()
   {
       Debug.Log("Quitting");
   }
   
}



