using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    [SerializeField]
    private GameObject target;

    public GameObject Target { get { return target; } set { target = value; } }


    private Vector3 velAdd;
    public Vector3 VelAdd { get { return velAdd; } set { velAdd = value; } }

    public 

    Vector3 newPos;

    [SerializeField]
    private bool inAnemone;
    public bool InAnemone { get { return inAnemone; } set { inAnemone = value; } }

    [SerializeField]
    private bool isHungry;
    public bool IsHungry { get { return isHungry; } set { isHungry = value; } }

    [SerializeField]
    private GameObject currentAnemone;
    public GameObject CurrentAnemone { get { return currentAnemone; } set { currentAnemone = value; } }

    [SerializeField]
    private bool hadChild;
    public bool HadChild { get { return hadChild; } set { hadChild = value; } }

    private float hungryTimer;
    private float hungryTimerMax;

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
        hungryTimer = 0;
        hungryTimerMax = UnityEngine.Random.Range(4f, 7f);

    }

    void Update()
    {
        //UpdateState();
        if(IsHungry == true)
        {
            hungryTimer += Time.deltaTime;
            if(hungryTimer >= hungryTimerMax)
            {
                if (currentAnemone != null)
                {
                    currentAnemone.GetComponent<Anemone>().RemoveFish(gameObject);
                }
                agentFlock.RemoveAgent(this);
                Destroy(gameObject);
            }

        }
        else
        {
            hungryTimer = 0;
        }
                
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

    


    public GameObject CheckForOtherAgent()
    {
        Collider[] agentList = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider c in agentList)
        {
            if (c.gameObject.tag != "SafeSpace" && c.gameObject.tag != "Food")
            {
                if (c != this.AgentCollider && c.gameObject.GetComponent<FlockAgent>().IsHungry == false)
                {
                    return c.gameObject;
                }
                
            }

        }
        return null;

    }

}
