using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> foods;

    [SerializeField]
    private List<GameObject> fish;

    [SerializeField]
    private GameObject foodPrefab;

    private float timer = 0;
    private float timerMax = 2f;
    private bool startTimer = false;


    void Start()
    {
        GWorld.Instance.AddAnemone(gameObject);
        foods = new List<GameObject>();
        fish = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            AddFood(transform.GetChild(i).gameObject);
        }

    }

    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
            if(timer >= timerMax)
            {
                GameObject food = Instantiate(foodPrefab,
                                new Vector3(Random.Range(0f, 0.5f) + transform.position.x, Random.Range(0f, 0.5f) + transform.position.y, Random.Range(0f, 0.5f) + transform.position.z),
                                Quaternion.identity,
                                transform);

                AddFood(food);
                timer = 0;
                startTimer = false;

            }
        }
    }

    public void AddFood(GameObject food)
    {
        foods.Add(food);
    }

    public void RemoveFood(int index)
    {
        GameObject temp = foods[index];
        foods.RemoveAt(index);
        //foods.Sort();
        Destroy(temp);
        startTimer = true;
    }

    public int FoodCount()
    {
        return foods.Count;
    }

    public void AddFish(GameObject food)
    {
        fish.Add(food);
    }

    public void RemoveFish(GameObject index)
    {
        //GameObject temp = fish[index];
        fish.Remove(index);
        //foods.Sort();
        //return temp;
    }

    public int FishCount()
    {
        return fish.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            AddFish(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fish"))
        {
            RemoveFish(other.gameObject);

        }
    }

}
