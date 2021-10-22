using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public enum FlockMovement
    {
        FLOCK,
        FLEE,
        STOP

    }

    FlockMovement flockMovement = FlockMovement.FLOCK;

    [SerializeField]
    private GameObject bound1;
    [SerializeField]
    private GameObject bound2;

    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    public FlockBehavior avoid;

    
    [Range(0, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private Vector3 agentPos;
    private float camLeft;
    private float camRight;
    private float camTop;
    private float camBottom;
    public float vertical;
    public float horizontal;

    private Vector3 bound1Vec;
    private Vector3 bound2Vec;

    float dist;

    public bool insidePen;

    void Start()
    {

        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        vertical = Camera.main.orthographicSize * 2;
        horizontal = vertical * Screen.width / Screen.height;

        bound1Vec = bound1.transform.position;
        bound2Vec = bound2.transform.position;

        
        dist = 1000;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitSphere * startingCount * AgentDensity / 2,
                Quaternion.Euler(Vector3.forward * Random.Range(180f, 270f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.AgentFlock = this;
            //newAgent.Initialize(this);
            agents.Add(newAgent);

        } 

    }

    void Update()
    {
        Flocking();

    }


    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider && context.Count < 20)
            {
                context.Add(c.transform);

            }

        }

        return context;

    }


    #region Behaviors
    

    public void RemoveAgent(FlockAgent agent)
    {
        agents.Remove(agent);
    }

    public void AddAgent(FlockAgent agent, Flock flock)
    {
        FlockAgent newAgent = Instantiate(
                agentPrefab,
                agent.transform.position,
                agent.transform.rotation,
                flock.transform
                );
        newAgent.name = agent.name;
        newAgent.AgentFlock = flock;
        //newAgent.tag = null;
        //newAgent.Initialize(this);
        agents.Add(newAgent);

    }



    public void Flocking()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector3 move = behavior.CalculateMove(agent, context, this);
            
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;

            }

            move = Vector3.Scale(StayInBounds(agent), move);

            agent.Move(move);            

        }

    }

    public Vector3 StayInBounds(FlockAgent agent)
    {
        agentPos = agent.transform.position;

        if (agentPos.x <= bound1Vec.x)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x + .1f, agent.transform.position.y, agent.transform.position.z);

            return new Vector3(-1, 0, 0);

        }

        else if (agentPos.x >= bound2Vec.x)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x - .1f, agent.transform.position.y, agent.transform.position.z);

            return new Vector3(-1, 0, 0);

        }

        else if(agentPos.y <= bound1Vec.y)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y + .1f, agent.transform.position.z);

            return new Vector3(0, -1, 0);

        }

        else if(agentPos.y >= bound2Vec.y)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y - .1f, agent.transform.position.z);

            return new Vector3(0, -1, 0);

        }

        else if (agentPos.z <= bound1Vec.z)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z + .1f);

            return new Vector3(0, 0, -1);

        }

        else if (agentPos.z >= bound2Vec.z)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z - .1f);

            return new Vector3(0, 0, -1);

        }

        else
        {
            return new Vector3(1, 1, 1);

        }
        //return new Vector3(1, 1);

    }

    public Vector3 StayInPen(FlockAgent agent)
    {
        agentPos = agent.transform.position;

        if (agentPos.x <= camLeft)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x + .1f, agent.transform.position.y, 0);

            return new Vector3(-1, 0, 0);

        }

        else if (agentPos.x >= camRight)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x - .1f, agent.transform.position.y, 0);

            return new Vector3(-1, 0, 0);

        }

        else if (agentPos.y >= camTop)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y - .1f, 0);

            return new Vector3(0, -1);

        }

        else if (agentPos.y <= 5.5f)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y + .1f, 0);

            return new Vector3(0, -1, 0);

        }

        else
        {
            return new Vector3(1, 1, 1);

        }
        //return new Vector3(1, 1);

    }

    
    #endregion


}
