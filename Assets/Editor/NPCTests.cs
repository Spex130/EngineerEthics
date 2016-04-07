using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class NPCTests {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject NPCObject = new GameObject();
		NPC theNPC = NPCObject.AddComponent<NPC> ();
		theNPC.timer = 5;
		theNPC.activatedTimer = false;


        //Act
        //Try to rename the GameObject
        int timerCheck = 5;
		theNPC.timerLoop ();
        //Assert
        //The object has a new name
		Assert.AreEqual(timerCheck, theNPC.timer);
    }
	[Test]
	public void TimerCheckTest()
	{

		//Arrange
		GameObject NPCObject = new GameObject();
		NPC theNPC = NPCObject.AddComponent<NPC> ();
			theNPC.timer = 5;
		theNPC.activatedTimer = true;


		//Act
		//Try to rename the GameObject
		int timerCheck = 5;
		theNPC.timerLoop ();
		//Assert
		//The object has a new name
		Assert.AreNotEqual(timerCheck, theNPC.timer);
	}
	[Test]
	[ExpectedException]

	public void RandomizeBehaviorTest()
	{

		//Arrange
		GameObject NPCObject = new GameObject();
		NPC theNPC = NPCObject.AddComponent<NPC> ();
		int state = 1;
		theNPC.activatedTimer = true;
		theNPC.randomizeBehavior ();
		Assert.AreNotEqual (state, theNPC.state);
	}
}
