using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The action for searching and going towards an anemone
/// </summary>
public class GoToAnemone : GAction
{
    FlockAgent flockAgent;

    public override bool PrePerform()
    {
        completed = false;
        flockAgent = agent.GetComponent<FlockAgent>();

        // Resets the current anemone in the agent
        if (flockAgent.CurrentAnemone != null)
        {
            flockAgent.CurrentAnemone.GetComponent<Anemone>().RemoveFish(flockAgent.gameObject);
        }
        flockAgent.CurrentAnemone = null;
        flockAgent.InAnemone = false;

        // Checks to see which one is the closest
        target = GWorld.Instance.CheckClosestAnemone(agent);

        if (target == null)
        {
            return false;
        }

        

        flockAgent.HadChild = false;
        return true;
    }

    public override void Tick()
    {
        // Seeks the closest anemone that has the most food
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
        float dist = Vector3.Distance(agent.transform.position, target.transform.position);
        if (dist <= 1.5f)
        {            
            completed = true;
        }
    }

    public override bool PostPerform()
    {
        // Sets the anemone the agent is at and adds the agent to it
        flockAgent.CurrentAnemone = target;
        flockAgent.InAnemone = true;
        target.GetComponent<Anemone>().AddFish(flockAgent.gameObject);

        flockAgent.VelAdd = Vector3.zero;
        return true;
    }

}
