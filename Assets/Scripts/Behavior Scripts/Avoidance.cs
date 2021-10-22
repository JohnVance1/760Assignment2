using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AvoidanceBehavior")]
public class Avoidance : FilterFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;

        }

        // add all points together and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;
        // When used for avoiding obstacles this will return only the obstacles
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // item is the thing that is being avoided
        foreach (Transform item in filteredContext)
        {
            if(Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (agent.transform.position - item.position);

            }

        }

        if(nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
            
        }

        return avoidanceMove;


    }

    public override void ChangeWeights(int weightNum, float setNum)
    {

    }


}
