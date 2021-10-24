using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAnemone : GAction
{
    FlockAgent flockAgent;

    public override bool PrePerform()
    {
        completed = false;
        flockAgent = agent.GetComponent<FlockAgent>();
        target = GWorld.Instance.CheckClosestAnemone(agent);

        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override void Tick()
    {
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position);
        float dist = Vector3.Distance(agent.transform.position, target.transform.position);
        if (dist <= 0.5f)
        {
            this.completed = true;
            Debug.Log("true");
        }
    }

    public override bool PostPerform()
    {
        //flockAgent.CurrentAnemone = target;
        //flockAgent.InAnemone = true;
        flockAgent.VelAdd = Vector3.zero;
        return true;
    }

}
