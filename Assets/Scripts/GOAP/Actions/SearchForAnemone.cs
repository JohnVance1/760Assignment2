using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForAnemone : GAction
{
    FlockAgent flockAgent;

    public override bool PrePerform()
    {
        target = GWorld.Instance.CheckClosestAnemone(agent);
        //GWorld.Instance.RemoveAnemone(target);
        flockAgent = agent.GetComponent<FlockAgent>();
        if (target == null)
        {
            return false;
        }
        completed = true;

        return true;
    }

    public override void Tick()
    {
        //flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
    }

    public override bool PostPerform()
    {
        flockAgent.CurrentAnemone = target;
        flockAgent.InAnemone = true;
        flockAgent.VelAdd = Vector3.zero;
        return true;
    }

}
