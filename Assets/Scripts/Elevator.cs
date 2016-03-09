using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Elevator : Door {

    public string sceneName;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {

	}



    public override void interact()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
