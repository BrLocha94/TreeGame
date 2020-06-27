using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawController : MonoBehaviour
{
    [Header("Range in Z axis on vector used to spaw new trees")]
    [SerializeField]
    [Range(1f, 10f)]
    private float radius;
    [Header("Range in X axis on vector used to spaw new trees")]
    [Range(1f, 4f)]
    [SerializeField]
    private float range;

    private Vector3 baseposition;

    void Awake()
    {
        baseposition = gameObject.transform.position;
    }

    public Vector3 GetSpawPosition()
    {
        Vector3 position = baseposition;

        SetNextSpawPosition();

        return position;
    }

    private void SetNextSpawPosition()
    {
        //Unity range exclude last param
        int random = Random.Range(-1, 2);

        baseposition = new Vector3(baseposition.x + (range / 2) * random, baseposition.y, baseposition.z + radius);
    }
}
