using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {

    // Use this for initialization
    public Text field;
    public Button button;
	void Start () {
        if (PlayerPrefs.GetString("Loser") == "Player 1") field.text = "Player 2 Wins!";
        else field.text = "Player 1 Wins!";
        button.onClick.AddListener(LoadLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
