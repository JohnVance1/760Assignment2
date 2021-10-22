using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class Composite : FlockBehavior
{
    [SerializeField]
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // handle data mismatch
        if(weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        
        }

        // set up move
        Vector3 move = Vector3.zero;

        // iterate through behaviors
        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock);// * weights[i];

            if(partialMove != Vector3.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                
                }

                move += partialMove;

            }

        }

        return move;


    }


    public override void ChangeWeights(int weightNum, float setNum)
    {
        if (weightNum >= weights.Length)
        {
            Debug.Log("Weight accessed is outside of the weight array!");

        }

        else
        {
            weights[weightNum] = setNum;

        }

    }




}
