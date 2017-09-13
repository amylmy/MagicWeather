using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SwipeMenu;

public class ChooseMenu : MonoBehaviour {
    public static string Filter;
    string menuname;


	public void FilterApply(string FilterValue)
    {
        Filter = FilterValue;
        SceneManager.LoadScene("API");
    }

    public void LoadScene1(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void NextMenu(MenuItem item)
    {
        menuname = item.gameObject.name;
        SceneManager.LoadScene(menuname);
    }


}
