using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproduce : GAction
{
    private FlockAgent flockAgent;
    private Vector3 rb;
    private GameObject closest;
    public override bool PrePerform()
    {
        completed = false;

        flockAgent = agent.GetComponent<FlockAgent>();
        target = flockAgent.CurrentAnemone;
        closest = flockAgent.CheckForOtherAgent();

        if (target == null || closest == null || flockAgent.HadChild == true || flockAgent.IsHungry == true)
        {
            return false;
        }
        
        rb = (target.transform.position - agent.transform.position).normalized;

       
        flockAgent.IsHungry = true;
        closest.GetComponent<FlockAgent>().IsHungry = true;
        flockAgent.HadChild = true;

        flockAgent.AgentFlock.AddAgent(flockAgent, flockAgent.AgentFlock);
        completed = true;
        return true;
    }

    public override void Tick()
    {
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position + rb);
        if(Vector3.Distance(agent.transform.position, target.transform.position + rb) <= 0.5f)
        {
            rb *= -1;
        }

    }

    public override bool PostPerform()
    {

        //flockAgent.IsHungry = false;

        return true;
    }

}
