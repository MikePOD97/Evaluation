using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
    public Button button;
	void Start ()
    {
        button.onClick.AddListener(LoadLevel);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
