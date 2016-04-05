using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class DoorTest {
	[SetUp]
	public void Init(){
		playerChar theChar = new playerChar ();
		Door testDoor = new Door ();

	}
    [Test]
    public void EditorTest()
    {
        //Arrange

		playerChar theChar = new playerChar ();
		Door testDoor = new Door ();
		Door testPart = new Door ();
		testDoor.partner = testPart;
        //Act
        //Try to rename the GameObject
		bool hittingDoor = true;
		testDoor.player = theChar;
		testDoor.interact ();

        //Assert
        //The object has a new name
        Assert.AreEqual(hittingDoor, theChar.isTouchingDoor);
    }
}
