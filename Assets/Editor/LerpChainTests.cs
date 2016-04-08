using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class LerpChainTests {

    [Test]
    public void EditorTest()
    {
        //Arrange
        GameObject gameObject = new GameObject();
		lerpChain chain = gameObject.AddComponent<lerpChain> ();

		GameObject[] objects = new GameObject[3];
		Transform[] transforms = new Transform[3];
		playerChar achar = gameObject.AddComponent<playerChar> ();
		chain.player = achar;
		Camera aCam = gameObject.AddComponent<Camera> ();
		aCam.transform.position = new Vector3 (0, 0);
		chain.myCamera = aCam;
        //Act
        //Try to rename the GameObject
		for (int i = 0; i < 3; i++) {
			objects [i] = new GameObject ();
			objects [i].transform.position = new Vector3 (i, 0);
			transforms [i] = objects [i].transform;
			transforms [i].position = new Vector3 (i, 0);
		}
		chain.locArray = transforms;
		chain.pointArray = objects;
		Debug.Log (chain.pointArray[2]);
		chain.init();
		chain.lerpLoop ();
        //Assert
        //The object has a new name
		Assert.AreEqual(chain.pointArray[0].transform.position.x, chain.pointArray[1].transform.position.x);
		chain.player.transform.position = new Vector3 (1f, 0);
		chain.resetLerp ();

		Assert.AreEqual(chain.pointArray[0].transform.position.x, chain.player.transform.position.x);

    }
}
