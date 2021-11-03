using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A reference to the world
/// </summary>
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

    /// <summary>
    /// Adds an anemone
    /// </summary>
    /// <param name="a"></param>
    public void AddAnemone(GameObject a)
    {
        anemones.Add(a);
    }

    /// <summary>
    /// Removes an anemone
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAnemone(GameObject index)
    {
        anemones.Remove(index);
    }


    /// <summary>
    /// Gets the closest anemone by shortest distance and most food
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    public GameObject CheckClosestAnemone(GameObject agent)
    {
        float dist = 1000;
        GameObject mostFood = anemones[0];
        GameObject closest = null;
        Dictionary<GameObject, float> queue = new Dictionary<GameObject, float>();

        foreach(GameObject g in anemones)
        {
            // Checks to see if there are less than 10 fish in the possible anemone and 
            // if the it isn't the current anemone already
            if ((g.GetComponent<Anemone>().FishCount() <= 10) && agent.GetComponent<FlockAgent>().CurrentAnemone != g)
            {
                // Gets the distance between the agent and the anemone
                float localDist = Vector3.Distance(agent.transform.position, g.transform.position);

                // If the anemone has no food then dont include that anemone
                if (g.GetComponent<Anemone>().FoodCount() <= 0)
                {
                    closest = null;
                }
                else
                {
                    queue.Add(g, localDist);

                }                

                

                if (g != null)
                {
                    if (queue.TryGetValue(g, out float newClosest))
                    {
                        // Changes the priority to be based of of food count and not only the distance
                        queue[g] = ((newClosest / g.GetComponent<Anemone>().FoodCount()) + 10 - g.GetComponent<Anemone>().FoodCount());
                    }
                    

                }

            }

        }

        GameObject actualClosest = null;
        float priority = 1000;
        foreach (KeyValuePair<GameObject, float> g in queue)
        {
            // Sorts by the priority
            if (g.Value < priority)
            {
                priority = g.Value;
                actualClosest = g.Key;
            }

        }

        return actualClosest;

    }

    
    public static GWorld Instance { get { return instance; } }

    public WorldStates GetWorld()
    {
        return world;
    }

}
