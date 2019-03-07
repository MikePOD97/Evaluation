using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManageHealth : MonoBehaviour {

    // Use this for initialization
    public Image p1Bar;
    public Image p2Bar;
    private float p1Perc;
    private float p2Perc;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        p1Perc = (float)GameObject.Find("Player 1").GetComponent<RoboMove>().health / 100;
        p1Bar.fillAmount = p1Perc;
        p2Perc = (float)GameObject.Find("Player 2").GetComponent<RoboMove>().health / 100;
        p2Bar.fillAmount = p2Perc;

    }
}
