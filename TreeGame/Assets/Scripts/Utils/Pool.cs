using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pooling strategy prevents unnecessary Instantiate and Destory Calls routines along the code
// Those routines has an heavy cost in unity pipeline (Garbage collection operations) 
public class Pool : MonoBehaviour
{
    [SerializeField]
    [Range(20, 40)]
    public int poolDepth;

    [Header("Prefab base to spaw")]
    [SerializeField]
    private Trunk prefab;

    private List<Trunk> pool = new List<Trunk>();

    public static Pool instance;

    void Awake()
    {
        instance = this;

        // Create an prefab reservation in scene
        for(int i = 0; i < poolDepth; i++)
        {
            pool.Add(Instantiate(prefab));
            pool[i].gameObject.SetActive(false);
        }
    }

    public Trunk GetAvaliablePrefab()
    {
        // Find if there is an unused prefab in reserve
        for(int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject.activeInHierarchy == false)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        // if not, expands the pool
        pool.Add(Instantiate(prefab));

        return pool[pool.Count - 1];
    }
}
