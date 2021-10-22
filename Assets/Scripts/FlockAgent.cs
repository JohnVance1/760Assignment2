using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : GAgent
{
    public enum FlockMovement
    {
        FLOCK,
        SEEK,
        STOP,
        FLEE

    }

    FlockMovement flockMovement = FlockMovement.FLOCK;
    float dist = 5f;

    [SerializeField]
    Flock agentFlock;
    

    public Flock AgentFlock { get { return agentFlock; } set { agentFlock = value; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    private bool canMove;

    public bool CanMove { get { return canMove; } set { canMove = value; } }

    private bool inBox;
    public bool InBox { get { return canMove; } set { canMove = value; } }


    private GameObject target;

    public GameObject Target { get { return target; } set { target = value; } }


    private Vector3 velAdd;
    public Vector3 VelAdd { get { return velAdd; } set { velAdd = value; } }

    public 

    Vector3 newPos;

    private bool inAnemone;
    public bool InAnemone { get { return inAnemone; } set { inAnemone = value; } }


    private bool isHungry;
    public bool IsHungry { get { return isHungry; } set { isHungry = value; } }

    private GameObject currentAnemone;
    public GameObject CurrentAnemone { get { return currentAnemone; } set { currentAnemone = value; } }


    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("HasBaby", 1, false);
        goals.Add(s1, 3);

        GWorld.Instance.GetWorld().ModifyState("SearchForAnemone", 1);


        inAnemone = false;
        currentAnemone = null;
        agentCollider = GetComponent<Collider>();
        canMove = true;
        inBox = false;
        velAdd = Vector3.zero;
        target = null;
        isHungry = true;

    }

    void Update()
    {
        //UpdateState();
                
    }

    public void Move(Vector3 velocity)
    {
        if (canMove)
        {
            //transform.up = Vector3.Lerp(transform.up, transform.up + (velocity + velAdd) * Time.deltaTime, 1f);
            transform.up += (velocity + velAdd) * Time.deltaTime;
            newPos = transform.position + (velocity + velAdd) * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newPos, 0.8f);

           

            //transform.rotation = Quaternion.LookRotation(velocity + velAdd);

        }
        //mice[index].targetPosition = mice[index].mousePosition += (separate * separationImpact) + (moveCloser * cohesionImpact) + (moveAligned * alignmentImpact); 
        //mouseOrientation = Game1.TurnToFace(mousePosition, mice[index].targetPosition, mouseOrientation, MouseTurnSpeed);

    }

    public void UpdateState()
    {
        //dist = (player.transform.position - transform.position).magnitude;
        
        switch (flockMovement)
        {
            case FlockMovement.FLOCK:
                if (dist <= 2f)
                {
                    flockMovement = FlockMovement.FLEE;

                }     
                if(target != null)
                {
                    flockMovement = FlockMovement.SEEK;
                }
                break;

            case FlockMovement.FLEE:
                if (dist > 2f)
                {
                    flockMovement = FlockMovement.FLOCK;

                }                
                break;

            case FlockMovement.SEEK:
                if(target == null)
                {
                    flockMovement = FlockMovement.FLOCK;
                }
                break;

        }

        DoState();

    }


    public void DoState()
    {
        switch (flockMovement)
        {
            case FlockMovement.FLOCK:
                canMove = true;
                velAdd = Vector3.zero;

                break;

            case FlockMovement.FLEE:
                velAdd = (transform.position - target.transform.position).normalized;
                velAdd *= 15f;
                if (velAdd.sqrMagnitude > 35f)
                {
                    velAdd = velAdd.normalized * 5f;

                }
                break;

            case FlockMovement.SEEK:
                velAdd = (target.transform.position - transform.position).normalized;
                velAdd *= 15f;
                if (velAdd.sqrMagnitude > 35f)
                {
                    velAdd = velAdd.normalized * 5f;

                }
                velAdd = Vector3.zero;
                break;
            

        }

    }


    public GameObject CheckForOtherAgent()
    {
        Collider[] agentList = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider c in agentList)
        {
            if (c.tag != "SafeSpace")
            {
                if (c != this.AgentCollider && c.gameObject.GetComponent<FlockAgent>().isHungry == false)
                {
                    return c.gameObject;
                }
            }

        }
        return null;

    }

}
