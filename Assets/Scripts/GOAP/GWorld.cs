using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static List<GameObject> anemones;
    
    static GWorld()
    {
        world = new WorldStates();
        anemones = new List<GameObject>();
    }

    private GWorld()
    {

    }

    public void AddAnemone(GameObject a)
    {
        anemones.Add(a);
    }

    public GameObject CheckClosestAnemone(GameObject agent)
    {
        float dist = 1000;
        GameObject closest = null;

        foreach(GameObject g in anemones)
        {
            if ((g.GetComponent<Anemone>().FishCount() < 10) || agent.GetComponent<FlockAgent>().CurrentAnemone != g)
            {
                float localDist = Vector3.Distance(agent.transform.position, g.transform.position);
                if (localDist < dist)
                {
                    dist = localDist;
                    closest = g;

                }
            }

        }

        return closest;

    }

    public void RemoveAnemone(GameObject index)
    {
        anemones.Remove(index);
    }

    public static GWorld Instance { get { return instance; } }

    public WorldStates GetWorld()
    {
        return world;
    }

}
