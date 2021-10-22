using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anemone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> foods;

    [SerializeField]
    private List<GameObject> fish;

    void Start()
    {
        GWorld.Instance.AddAnemone(gameObject);
        //foods = new List<GameObject>();
        fish = new List<GameObject>();
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

}
