using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Range(1f, 10f)]
    public float moveSpeed;
    float rotSpeed;

    [Range(1f, 10f)]
    public float rad = 1.5f;
    Collider2D playerCollider;

    private Vector3 camPos;
    private float camLeft;
    private float camRight;
    private float camTop;
    private float camBottom;
    public float vertical;
    public float horizontal;


    void Start()
    {
        moveSpeed = 5;
        rotSpeed = -1.5f;

        vertical = Camera.main.orthographicSize * 2;
        horizontal = vertical * Screen.width / Screen.height;

        camPos = Camera.main.WorldToViewportPoint(transform.position);

        camLeft = Camera.main.transform.position.x - (horizontal / 2);// - Camera.main.transform.position.x;
        camRight = Camera.main.transform.position.x + (horizontal / 2);// + Camera.main.transform.position.x;
        camTop = Camera.main.transform.position.y + (vertical / 2);// + Camera.main.transform.position.y;
        camBottom = Camera.main.transform.position.y - (vertical / 2);// - Camera.main.transform.position.y;

    }
    


    void Update()
    {
        Move();
        Sprint();

    }


    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        x *= rotSpeed;

        Vector3 movement = new Vector3(0, y, 0);
        transform.rotation *= Quaternion.Euler(0, 0, x);

        //transform.rotation *= 0.5f;

        Vector3 playerMovement = transform.rotation * movement;
        playerMovement *= StayInBounds();

        transform.position += playerMovement * moveSpeed * Time.deltaTime;


    }

    public void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed *= 2;
            rotSpeed -= 1f;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSpeed /= 2;
            rotSpeed += 1f;

        }

    }

    public List<FlockAgent> GetNearbyObjects()
    {
        List<FlockAgent> context = new List<FlockAgent>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, rad);
        foreach (Collider2D c in contextColliders)
        {
            if ((c != playerCollider) && (c.GetComponent<FlockAgent>() != null))
            {
                context.Add(c.GetComponent<FlockAgent>());

            }

        }

        return context;

    }

    public Vector2 StayInBounds()
    {
        camPos = transform.position;

        if (camPos.x <= camLeft)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            transform.position = new Vector3(transform.position.x + .1f, transform.position.y, 0);

            return new Vector2(-1, 0);

        }

        else if (camPos.x >= camRight)
        {
            //agent.transform.position = new Vector3(-agent.transform.position.x, agent.transform.position.y, 0);
            transform.position = new Vector3(transform.position.x - .1f, transform.position.y, 0);

            return new Vector2(-1, 0);

        }

        else if (camPos.y >= camTop)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y - .1f, 0);

            return new Vector2(0, -1);

        }

        else if (camPos.y <= camBottom)
        {
            //agent.transform.position = new Vector3(agent.transform.position.x, -agent.transform.position.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + .1f, 0);

            return new Vector2(0, -1);

        }

        else
        {
            return new Vector2(1, 1);

        }
        //return new Vector2(1, 1);

    }



}
