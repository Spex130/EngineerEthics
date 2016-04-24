using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class convoNodeTests {

    [Test]
    public void EditorTest()
    {
        //Arrange
		GameObject aNode = new GameObject();
		convoNode theConvoNode =  aNode.AddComponent<convoNode>();
		theConvoNode.endAnswerArray = new string[1];
		theConvoNode.endEventArray = new eventScript[1];
        //Act
        //Try to rename the GameObject
		string tryToadd = "blah";

		theConvoNode.addQuestion (tryToadd);

        //Assert
        //The object has a new name
		Assert.AreEqual(theConvoNode.endAnswerArray[1], tryToadd);
    }
	[Test]
	public void TestWithEventScript()
	{
		GameObject aNode = new GameObject();
		convoNode theConvoNode =  aNode.AddComponent<convoNode>();
		theConvoNode.endAnswerArray = new string[1];
		eventScript theScript = aNode.AddComponent<eventScript> ();
		theConvoNode.endEventArray = new eventScript[1];

		string tryToadd = "blah";

		theConvoNode.addQuestion (tryToadd, theScript);

		Assert.AreEqual(theConvoNode.endAnswerArray[1], tryToadd);

	}
	[Test]
	public void getNextNodeTests()
	{
		GameObject aNode = new GameObject();
		convoNode theConvoNode =  aNode.AddComponent<convoNode>();
		convoNode otherConvoNode =  aNode.AddComponent<convoNode>();

		convoNode[] nextNodeArray = new convoNode[3];
		nextNodeArray [0] = otherConvoNode;
		theConvoNode.nextNodeArray = nextNodeArray;
		Assert.AreEqual (theConvoNode.getNextNode (), otherConvoNode);
		Assert.AreEqual (theConvoNode.getNextNode (0), otherConvoNode);
		Assert.AreNotEqual (theConvoNode.getNextNode (1), otherConvoNode);


	}
}
