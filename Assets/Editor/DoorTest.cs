using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class DoorTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject Holder = new GameObject();
		playerChar theChar = Holder.AddComponent<playerChar> ();
		Door testDoor = Holder.AddComponent<Door> ();
		Door testPart =  Holder.AddComponent<Door> ();
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
