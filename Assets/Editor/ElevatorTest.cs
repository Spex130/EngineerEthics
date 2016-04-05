using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class ElevatorTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
        var gameObject = new Elevator();
        //Act
        //Try to rename the GameObject
        var newGameObjectName = "Larry'sTestScene";

        gameObject.sceneName = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.sceneName);
    }


}
