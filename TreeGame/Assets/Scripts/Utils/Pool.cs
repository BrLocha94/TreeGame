using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        for(int i = 0; i < poolDepth; i++)
        {
            pool.Add(Instantiate(prefab));
            pool[i].gameObject.SetActive(false);
        }
    }

    public Trunk GetAvaliablePrefab()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (pool[i].gameObject.activeInHierarchy == false)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        pool.Add(Instantiate(prefab));

        return pool[pool.Count - 1];
    }
}
