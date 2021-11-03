using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The action that allows for fish to reproduce
/// </summary>
public class Reproduce : GAction
{
    // The current agent
    private FlockAgent flockAgent;
    private Flock flock;
    private Vector3 rb;

    // The list of the closest objects next to the agent
    private List<Transform> closest;

    // The closest object to the agent that is full
    private GameObject closestGO;
    public override bool PrePerform()
    {
        completed = false;

        flockAgent = agent.GetComponent<FlockAgent>();
        flock = flockAgent.AgentFlock;
        target = flockAgent.CurrentAnemone;
        closest = flock.GetNearbyObjects(flockAgent);

        if(closest == null)
        {
            return false;
        }

        // checks to see if there is a fish that is full next to the current agent
        foreach (Transform t in closest)
        {
            if (t.tag != "SafeSpace" && t.tag != "Food")
            {
                if (t.gameObject.GetComponent<FlockAgent>().IsHungry == false)
                {
                    closestGO = t.gameObject;
                }
            }
        }
        

        if (closestGO == null)
        {
            return false;
        }

        if (target == null ||
            closest == null ||
            flockAgent.HadChild == true ||
            flockAgent.IsHungry == true ||
            target.GetComponent<Anemone>().FoodCount() == 0 ||
            closestGO.GetComponent<FlockAgent>().IsHungry == true ||
            closestGO.GetComponent<FlockAgent>().HadChild == true)
        {
            return false;
        }

        rb = agent.GetComponent<Rigidbody>().velocity.normalized;
        flockAgent.CurrentAnemone.GetComponent<Anemone>().RemoveFish(flockAgent.gameObject);
        flockAgent.CurrentAnemone = null;

        flockAgent.IsHungry = true;
        flockAgent.HadChild = true;

        // Creates a child of the current agent and spawns it into the world
        flockAgent.AgentFlock.AddAgent(flockAgent, flockAgent.AgentFlock);
        
        return true;
    }

    public override void Tick()
    {
        // Adds a seek vector to the flocking behavior
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
        
        if(flockAgent.HadChild)
        {
            completed = true;

        }

    }

    public override bool PostPerform()
    {
        flockAgent.IsHungry = true;

        return true;
    }

}
