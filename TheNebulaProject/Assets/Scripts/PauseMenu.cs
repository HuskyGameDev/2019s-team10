using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*public class PauseMenu : MonoBehaviour {
    private bool paused = false;
    private bool pausedPressed = false;
    private InputManager input;

    [SerializeField] private GameObject canvasObject;
    private static PauseMenu self;
	// Use this for initialization
	void Start () {
        input = GameObject.FindObjectOfType<InputManager>();
        self = this;
	}

    // Update is called once per frame
    void Update() {
        if (InputManager.GetButtonDown("Pause") && !pausedPressed)
        {
            pausedPressed = true;
            if (paused)
                Unpause();
            else
                Pause();
        } else if (InputManager.GetButtonUp("Pause"))
        {
            pausedPressed = false;
        }
        }	
	public bool IsPaused()
    {
        return paused;
    }

    public void Unpause()
    {   //TODO
        //need to make continue audio for unpuased music
        AkSoundEngine.PostEvent("Resume_All_SFX", gameObject);
        canvasObject.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }
    //pause scene
    public void Pause()
    {   
        //TODO 
        //still need to make stop audio for paused music
        AkSoundEngine.PostEvent("Pause_All_SFX", gameObject);
        paused = true;
        Time.timeScale = 0;
    }
    //unpuase scene
    public void UnpauseStatic()
    {
        self.Unpause();
    }
    //return the player to main menu if button is pressed
    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    //allows for game to restart on the scene that the pause occured on
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}*/
