using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public string sceneName;

	// Use this for initialization
	void Start () {

	}
	public void interact()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
	// Update is called once per frame
	void Update () {
	
	}

}
