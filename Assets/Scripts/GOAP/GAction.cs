using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1;
    public GameObject target;
    public string targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public GameObject agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running = false;
    public bool completed = false;

    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = gameObject;

        if(preConditions != null)
        {
            foreach(WorldState w in preConditions)
            { 
                preconditions.Add(w.key, w.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }
    }

    public void LateUpdate()
    {
        if (running)
        {
            Tick();
        }
    }

    public bool IsAchievable()
    {
        return true;

    }

    public Vector3 Flee(Vector3 fish, Vector3 target)
    {
        Vector3 velAdd = (fish - target).normalized;
        velAdd *= 15f;
        if (velAdd.sqrMagnitude > 35f)
        {
            velAdd = velAdd.normalized * 5f;

        }

        return velAdd;
    }
    
    /// <summary>
    /// Allows for the player to seek something
    /// </summary>
    /// <param name="fish"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public Vector3 Seek(Vector3 fish, Vector3 target)
    {
        Vector3 velAdd = (target - fish).normalized;
        velAdd *= 15f;
        if (velAdd.sqrMagnitude > 35f)
        {
            velAdd = velAdd.normalized * 7f;

        }

        return velAdd;
    }


    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if(!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;

    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
    public abstract void Tick();



}
