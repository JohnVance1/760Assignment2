using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/AlignmentBehavior")]
public class Alignment : FilterFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbors, return no adjustment
        if (context.Count == 0)
        {
            return agent.transform.up;

        }

        // add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += item.transform.up;

        }

        alignmentMove /= context.Count;

        return alignmentMove;


    }

    public override void ChangeWeights(int weightNum, float setNum)
    {

    }


}
