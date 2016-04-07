using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections;

public class EventScriptTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject eventObject = new GameObject();
		eventScript evScript = eventObject.AddComponent<eventScript> ();

        //Act
        //Try to rename the GameObject
		string[] newGameObjectName =  {"oah", "hah", "test"};
		evScript.keyList = new string[3] {"oah", "hah", "test"};

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, evScript.keyList);
    }
	public void hashTableTest(){
		GameObject eventObject = new GameObject();
		eventScript evScript = eventObject.AddComponent<eventScript> ();
		Hashtable aList = new Hashtable();
		evScript.changeList = aList;
		evScript.populateHashtable ();
		Assert.AreEqual (evScript.changeList, aList);
	}
	public void actuallyFilledHashTableTest(){

		GameObject eventObject = new GameObject();
		eventScript evScript = eventObject.AddComponent<eventScript> ();
		Hashtable aList = new Hashtable();
		evScript.changeList = aList;

		evScript.keyList = new string[3] {"oah", "hah", "test"};
		evScript.valueList = new convoNode[]{ null, null, null };
		evScript.populateHashtable ();
		Assert.AreNotEqual (evScript.changeList, aList);
	}
}
