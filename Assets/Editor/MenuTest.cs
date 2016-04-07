using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MenuTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject menuButton = new GameObject();
		MainMenuScript menuScript = menuButton.AddComponent<MainMenuScript> ();
		//Act
		//Try to rename the GameObject
		var newGameObjectName = "Larry'sTestScene";

		menuScript.sceneName = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, menuScript.sceneName);
    }
}
