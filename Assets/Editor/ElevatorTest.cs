using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ElevatorTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject Elevator = new GameObject();
		Elevator elevatorscript = Elevator.AddComponent<Elevator> ();
        //Act
        //Try to rename the GameObject
        var newGameObjectName = "Larry'sTestScene";

        elevatorscript.sceneName = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, elevatorscript.sceneName);
    }


}
