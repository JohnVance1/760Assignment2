using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (a.FoodCount() > 0 && flockAgent.InAnemone)
        {
            a.RemoveFood(a.FoodCount() - 1);
            flockAgent.IsHungry = false;
        }
        else
        {
            //GWorld.Instance.GetWorld().ModifyState("NoFood", 1);
            return false;
        }
        rb = (target.transform.position - agent.transform.position).normalized;

        completed = true;
        return true;
    }

    public override void Tick()
    {
        flockAgent.VelAdd = Seek(agent.transform.position, target.transform.position + rb);
        if (Vector3.Distance(agent.transform.position, target.transform.position + rb) <= 0.5f)
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
