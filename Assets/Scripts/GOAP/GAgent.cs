using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(s, i);
        remove = r;
    }

}


public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    bool move = false;
    bool invoked = false;
    float time = 0;

    public void Start()
    {
        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
        {
            actions.Add(a);
        }
    }

    void LateUpdate()
    {
        if(currentAction != null && currentAction.running)
        {
            //if(currentAction.agent.hasPath && currentAction.agent.remainingDistance < 1f)
            if(currentAction.completed)
            {
                if(!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }

        if(planner == null || actionQueue == null)
        {
            planner = new GPlanner();
            IOrderedEnumerable<KeyValuePair<SubGoal, int>> sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach(KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sGoals, null);
                if(actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }

        if(actionQueue != null && actionQueue.Count == 0)
        {
            if(currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if(actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if(currentAction.PrePerform())
            {
                if(currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                }

                if(currentAction.target != null)
                {
                    currentAction.running = true;
                    //currentAction.agent.SetDestination(currentAction.target.transform.position);
                    //currentAction.GetComponent<FlockAgent>().Target = currentAction.target;

                    move = true;

                }
            }
            else
            {
                actionQueue = null;
            }
        }

    }

    private void Update()
    {
        
    }



    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.completed = false;
        currentAction.PostPerform();
        time = 0;
        invoked = false;
        currentAction.GetComponent<FlockAgent>().Target = null;

    }

    public float RemainingDistance()
    {
        return Vector3.Distance(currentAction.transform.position, currentAction.target.transform.position);

    }

    
}
