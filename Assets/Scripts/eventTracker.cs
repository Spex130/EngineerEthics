﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Okay, the long and short of it is that this class listens for "Events"
/// When an event happens, The event class contains a hashtable with Name keys and ConvoNode values.
/// The eventTracker finds matches in names, then passes each NPC its new ConvoNode based on the event.
/// 
/// For example, if you've already talked to your Boss, then everyone else should know that, right? This handles that.
/// 
/// This also handles "Timed Events." These are events where talking to people will subtract from your "Total Time Allotment."
/// If you have 15 "Minutes" to talk to 5 people and each conversation costs 5 "minutes" 
/// </summary>

public class eventTracker : MonoBehaviour {


    [Tooltip("This is the list of LITERALLY EVERYTHING in the scene that can generate a convonode. If it's an NPC relevant to the plot, put it here.")]
    public NPC[] sceneInteractables;
    public Hashtable hashList;
    //We're going to convert all of the NPCs in the scene into a hashtable with the Keys based on the NPC names.
    //Unfortunately, this means that no NPC can share a name with another NPC. 

    [Tooltip("This is our 'Time Countdown' currency holder. Whenever we do something that costs Time, we remove it from this value.")]
    public int timeLeft = 60;
    [Tooltip("This is the eventScript that gets activated when the timer hits 0.")]
    public eventScript timeEvent;
    [Tooltip("If the timeLeft variable hits zero while this bool is active, whatever's in our TimeEvent variable ")]
    public bool timerActivated = false;

	// Use this for initialization
	void Start () {
        populateHashtable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void populateHashtable()
    {
        if (hashList == null)
        {
            hashList = new Hashtable();
        }
        for (int i = 0; i < sceneInteractables.Length; i++)
        {
            hashList.Add(sceneInteractables[i].name, sceneInteractables[i]);
            //We add in each NPC to the list so it can be searched for by name later.
        }           
    }

    //This takes in an event and then applies the changes from that event to every NPC in the scene.
    public void loadEvent(eventScript newEvent)
    {

        

        switch (newEvent.eventType)
        {
            case eventNodeType.NPCEvent://NPCEvents change the NPC's speeches, and they change the eventTracker's timer settings.

                doNPCEvent(newEvent);
                
                break;

            case eventNodeType.TimerEvent://TimerEvents only set up Events for the Timer to run later!

                doTimerEvent(newEvent);

                break;

            case eventNodeType.addQuestionEvent:

                doAddQuestionEvent(newEvent);
                
                break;

            case eventNodeType.addQuestionOneshotEvent:
                doAddQuestionOneshotEvent(newEvent);
                break;


            case eventNodeType.MultiEvent://TURNS OUT WE WANT TO DO MULTIPLE THINGS ALL AT ONCE.

                //Check the boxes you want to run. It'll skip the ones that aren't run.
                if (newEvent.Multi_AddQuestionEvent) { doAddQuestionEvent(newEvent); }
                if (newEvent.Multi_AddQuestionOneShotEvent) { doAddQuestionOneshotEvent(newEvent); }
                if (newEvent.Multi_NPCEvent) { doNPCEvent(newEvent); }
                if (newEvent.Multi_TimerEvent) { doTimerEvent(newEvent); }

                break;

            case eventNodeType.EndEvent:
                SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
                break;

            default:

            break;
        }

        eventCheck(newEvent);        
       
    }

    public void doNPCEvent(eventScript newEvent)
    {
        string tempName;
        for (int i = 0; i < sceneInteractables.Length; i++)//We check every NPC in the scene to see if it should be changed.
        {
            tempName = sceneInteractables[i].name;//We take the name of our NPC
            if (newEvent.changeList.Contains(tempName) && ((convoNode)newEvent.changeList[tempName]) != null)//If the name is found in the Event's list of NPCs who need stuff changed...
            {
                sceneInteractables[i].myConvo = (convoNode)newEvent.changeList[tempName];//...Then we change that NPC's convoNode.
            }
        }
        timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
        if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
        timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
    }

    public void doTimerEvent(eventScript newEvent)
    {
        timeEvent = newEvent;
        timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
        if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
        timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
		Debug.Log(timeLeft);
    }


    public void doAddQuestionEvent(eventScript newEvent)
    {
        NPC tempNPC;
        //If our keyList has Names AND all of its associated Arrays are of proper length, then do the checks.
        if (newEvent.keyList.Length > 0 && newEvent.answersToAdd.Length >= newEvent.keyList.Length && newEvent.eventsToAdd.Length >= newEvent.keyList.Length)
        {
            for (int i = 0; i < newEvent.keyList.Length; i++)//We check every NPC in the scene to see if it should be changed.
            {
                print(i);
                if (hashList.ContainsKey(newEvent.keyList[i]))//If the name in our keyList (which can contain multiples of the same name) is in the scene, add the answer to that node.
                {
                    tempNPC = (NPC)(hashList[newEvent.keyList[i]]);//Make a TempNPC so we can edit it.
                    if (newEvent.eventsToAdd[i] == null) //If there's not a corresponding event to our answer, just add the answer.
                    {
                        tempNPC.myConvo.addQuestion(newEvent.answersToAdd[i], newEvent.eventsToAdd[i], newEvent.nextNodesToAdd[i]);
                        tempNPC.myConvo.hasNext = true;
                    }
                    else
                    {
                        tempNPC.myConvo.addQuestion(newEvent.answersToAdd[i], newEvent.eventsToAdd[i], newEvent.nextNodesToAdd[i]);//Otherwise add the answer and the event.
                        tempNPC.myConvo.hasNext = true;
                    }
                    hashList[newEvent.keyList[i]] = tempNPC;//Then we make sure to put the tempNode back where it came from.
                }
            }
            timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
            if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
            timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
        }
        else//Else fail and tell us why.
        {
            print("Node [" + this.name + "] KeyList[]/answersToAdd[]/eventsToAdd[] array size mismatch. The array lengthss must be >= KeyList.Length!");
        }
    }

    public void doAddQuestionOneshotEvent(eventScript newEvent)
    {
        NPC tempNPC;

        //If our keyList has Names AND all of its associated Arrays are of proper length, then do the checks.
        if (newEvent.keyList.Length > 0 && newEvent.answersToAdd.Length >= newEvent.keyList.Length && newEvent.eventsToAdd.Length >= newEvent.keyList.Length)
        {
            for (int i = 0; i < newEvent.keyList.Length; i++)//We check every NPC in the scene to see if it should be changed.
            {
                print(i);
                if (hashList.ContainsKey(newEvent.keyList[i]))//If the name in our keyList (which can contain multiples of the same name) is in the scene, add the answer to that node.
                {
                    tempNPC = (NPC)(hashList[newEvent.keyList[i]]);//Make a TempNPC so we can edit it.
                    if (newEvent.eventsToAdd[i] == null) //If there's not a corresponding event to our answer, just add the answer.
                    {
                        tempNPC.myConvo.addQuestion(newEvent.answersToAdd[i], newEvent.eventsToAdd[i], newEvent.nextNodesToAdd[i]);
                        tempNPC.myConvo.hasNext = true;
                    }
                    else
                    {
                        tempNPC.myConvo.addQuestion(newEvent.answersToAdd[i], newEvent.eventsToAdd[i], newEvent.nextNodesToAdd[i]);//Otherwise add the answer and the event.
                        tempNPC.myConvo.hasNext = true;
                    }
                    hashList[newEvent.keyList[i]] = tempNPC;//Then we make sure to put the tempNode back where it came from.
                }
            }
        }
        else//Else fail and tell us why.
        {
            print("Node [" + this.name + "] KeyList[]/answersToAdd[]/eventsToAdd[] array size mismatch. The array lengthss must be >= KeyList.Length!");
        }
        timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
        if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
        timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.


        newEvent.oneshotClear();//Since we're passed a pointer, this makes the node passed to us INCAPABLE of adding answers again.
    }

    public void eventCheck( eventScript newEvent)
    {
        string tempName;
        //After we've done our Timer Math, check if our Event should be run!
        if (timeLeft <= 0 && timerActivated)
        {
            timerActivated = false;

            for (int i = 0; i < sceneInteractables.Length; i++)//We check every NPC in the scene to see if it should be changed.
            {
                tempName = sceneInteractables[i].name;//We take the name of our NPC
                if (newEvent.changeList.Contains(tempName))//If the name is found in the Event's list of NPCs who need stuff changed...
                {
                    sceneInteractables[i].myConvo = (convoNode)newEvent.changeList[tempName];//...Then we change that NPC's convoNode.
                }
            }
            timeLeft += newEvent.adjustAmount;//We add/subtract any minutes the event gives us.
            if (timeLeft < 0) { timeLeft = 0; }//If the time subtracted was larger than the amount of time we had left, just set it to 0.
            timerActivated = newEvent.shouldActivateTimer;//Activate the timer, if we're told.
        }
    }


}
