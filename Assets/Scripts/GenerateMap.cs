using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject[] mapType;
    public int[] typeCount;
    private List<List<GameObject>> floorList = new List<List<GameObject>>();
    // public GameObject normalFloor;

    // Start is called before the first frame update
    void Start()
    {
        // Test instantiate
        // for (int i = 0; i < mapLength; i++)
        // {
        //     Instantiate(normalFloor, new Vector3(0, 0, 6 * i), Quaternion.identity);
        // }

        // Map variant
        foreach (GameObject obj in mapType)
        {
            List<GameObject> newList = new List<GameObject>();
            foreach (Transform child in obj.transform)
            {
                newList.Add(child.gameObject);
            }
            floorList.Add(newList);
        }

        // Bias random
        int floorCount = 0;
        List<int> biasRandom = new List<int>();
        for (int i = 0; i < typeCount.Length; i++)
        {
            floorCount += typeCount[i];
            for (int j = 0; j < typeCount[i]; j++)
            {
                biasRandom.Add(i);
            }
        }

        // Shuffle biasRandom
        Shuffle<int>(ref biasRandom);
        Debug.Log(biasRandom);

        // Generate floor
        for (int i = 0; i < biasRandom.Count; i++)
        {
            List<GameObject> floorVariant = floorList[biasRandom[i]];
            int k = Random.Range(0, floorVariant.Count);
            Instantiate(floorVariant[k], new Vector3(0, 0, 6 * i), Quaternion.identity);
        }
    }

    // Code reference: Project 5
    private void Shuffle<T>(ref List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
