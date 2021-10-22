using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAnemone : GAction
{
    FlockAgent flockAgent;

    public override bool PrePerform()
    {
        flockAgent = agent.GetComponent<FlockAgent>();
        target = flockAgent.CurrentAnemone;

        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override void Tick()
    {
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
        if(Vector3.Distance(agent.transform.position, target.transform.position) <= 1f)
        {
            this.completed = true;
            Debug.Log("true");
        }
    }

    public override bool PostPerform()
    {
        flockAgent.CurrentAnemone = target;
        flockAgent.InAnemone = true;
        flockAgent.VelAdd = Vector3.zero;
        return true;
    }

}
