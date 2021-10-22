using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatherBox : MonoBehaviour
{
    public Flock blueFlock;
    public Flock mainFlock;


    void Start()
    {
        
    }

    void Update()
    {
        if(mainFlock.agents.Count == 0)
        {
            SceneManager.LoadScene(1);

        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "agent")
        {
            other.gameObject.GetComponent<FlockAgent>().InBox = true;
            other.gameObject.GetComponent<FlockAgent>().AgentFlock = blueFlock;

            other.transform.SetParent(blueFlock.gameObject.transform);
            blueFlock.AddAgent(other.gameObject.GetComponent<FlockAgent>(), blueFlock);
            Destroy(other.gameObject);
            //3other.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }


}
