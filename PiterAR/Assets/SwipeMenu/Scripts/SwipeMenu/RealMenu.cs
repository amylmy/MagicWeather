using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwipeMenu;
using UnityEngine.SceneManagement;

public class RealMenu : MonoBehaviour {
	void Start () {
		
	}
	void Update () {
		
	}
    string menuname;
    public void NextMenu(MenuItem item)
    {
        menuname = item.gameObject.name;
        SceneManager.LoadScene(menuname);
    }
}
