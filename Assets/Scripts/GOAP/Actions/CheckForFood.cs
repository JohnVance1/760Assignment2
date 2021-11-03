using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The action for checking if there is food in the 
/// anemone the agent is currently in
/// </summary>
public class CheckForFood : GAction
{
    FlockAgent flockAgent;
    Vector3 rb;
    Anemone a;
    public override bool PrePerform()
    {
        completed = false;

        flockAgent = agent.GetComponent<FlockAgent>();
        target = flockAgent.CurrentAnemone;
        if(target == null)
        {
            return false;
        }
        
        a = flockAgent.CurrentAnemone.GetComponent<Anemone>();

               

        return true;
    }

    public override void Tick()
    {
        // Checks to see if there is food in the current anemone,
        // if it is in an anemone,
        // if it is hungry, and
        // if it hasen't had a child
        if (a.FoodCount() > 0 && 
            flockAgent.InAnemone && 
            flockAgent.IsHungry == true && 
            flockAgent.HadChild == false)
        {
            // Remove a food from the anemone
            a.RemoveFood(a.FoodCount() - 1);
            // The fish isn't hungry
            flockAgent.IsHungry = false;
        }

        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
        
        if(flockAgent.IsHungry == false)
        {
            completed = true;

        }

    }

    public override bool PostPerform()
    {
        return true;
    }

}
