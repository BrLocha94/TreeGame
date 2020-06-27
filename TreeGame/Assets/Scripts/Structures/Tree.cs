using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Avaliable in inspector to debug proposes")]
    [SerializeField]
    private int height;
    [SerializeField]
    private List<Trunk> trunks = new List<Trunk>();

    private float prefabHeight = 0;

    public void SetTree(int height, Vector3 position)
    {
        Vector3 lastPosition = new Vector3(0f, 0f, 0f);

        this.height = height;

        for (int i = 0; i < height; i++)
        {
            trunks.Add(Pool.instance.GetAvaliablePrefab());

            if (prefabHeight == 0)
                prefabHeight = trunks[i].GetComponent<Renderer>().bounds.size.y;

            trunks[i].gameObject.transform.SetParent(gameObject.transform);

            trunks[i].transform.position = new Vector3(lastPosition.x, lastPosition.y + prefabHeight, lastPosition.z);
            lastPosition = trunks[i].transform.position;
        }

        gameObject.transform.position = position;
    }
}
