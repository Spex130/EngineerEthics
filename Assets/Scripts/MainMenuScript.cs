using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
	public string sceneName;
	public int SkinColor;
	public int Sex;
	// Use this for initialization
	void Start () {

	}
	public void interact()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
	public void ChangeSkinColor(){
		PlayerPrefs.SetInt ("Skin Color", SkinColor);
	}
	public void ChangeGender(){
		PlayerPrefs.SetInt ("Sex", Sex);

	}
	// Update is called once per frame
	void Update () {
	
	}

}
