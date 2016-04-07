using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;

public class EventTrackerTests {

    [Test]
    public void CheckHashStuff()
    {
        //Arrange
		GameObject tracker = new GameObject();
		eventTracker trackerScript = tracker.AddComponent<eventTracker> ();
		trackerScript.sceneInteractables = new NPC[3];
		trackerScript.hashList = new System.Collections.Hashtable ();
		Hashtable atable =  new System.Collections.Hashtable ();
        //Act
        //Try to rename the GameObject
		for (int i = 0; i < 3; i++) {
			trackerScript.sceneInteractables [i] = tracker.AddComponent<NPC> ();
			trackerScript.sceneInteractables [i].name = i + "";
		}
		trackerScript.populateHashtable ();
        //Assert
        //The object has a new name
		Assert.AreNotEqual(trackerScript.hashList, atable);
		Assert.IsNotEmpty (trackerScript.hashList);
		Assert.AreEqual (trackerScript.hashList.Contains ("1"), true);
    }
	[Test]
	public void CheckEventLoadsNPC(){
		GameObject tracker = new GameObject();
		eventTracker trackerScript = tracker.AddComponent<eventTracker> ();
		trackerScript.sceneInteractables = new NPC[3];
		trackerScript.hashList = new System.Collections.Hashtable ();
		Hashtable atable =  new System.Collections.Hashtable ();
		//Act
		//Try to rename the GameObject
		for (int i = 0; i < 3; i++) {
			trackerScript.sceneInteractables [i] = tracker.AddComponent<NPC> ();
			trackerScript.sceneInteractables [i].name = i + "";
		}
		trackerScript.populateHashtable ();
		GameObject otherEvent = new GameObject ();
		eventScript newEvent = otherEvent.AddComponent<eventScript> ();
		newEvent.eventType = eventNodeType.NPCEvent;
		newEvent.shouldActivateTimer = false;
		trackerScript.loadEvent (newEvent);
		Assert.IsFalse (trackerScript.timerActivated);
	}
	[Test]
	public void CheckEventLoadsTimer(){
		GameObject tracker = new GameObject();
		eventTracker trackerScript = tracker.AddComponent<eventTracker> ();
		trackerScript.sceneInteractables = new NPC[3];
		trackerScript.hashList = new System.Collections.Hashtable ();
		Hashtable atable =  new System.Collections.Hashtable ();
		//Act
		//Try to rename the GameObject
		for (int i = 0; i < 3; i++) {
			trackerScript.sceneInteractables [i] = tracker.AddComponent<NPC> ();
			trackerScript.sceneInteractables [i].name = i + "";
		}
		trackerScript.populateHashtable ();
		GameObject otherEvent = new GameObject ();
		eventScript newEvent = otherEvent.AddComponent<eventScript> ();
		newEvent.eventType = eventNodeType.TimerEvent;
		newEvent.shouldActivateTimer = true;
		trackerScript.loadEvent (newEvent);
		Assert.IsTrue (trackerScript.timerActivated);
	}
}
