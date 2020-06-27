using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Tree : MonoBehaviour
{
    [Header("Externals references to use in object")]
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Transform trunksParent;

    [SerializeField]
    private float animationTime = 1f;

    [Header("Avaliable in inspector to debug proposes")]
    [SerializeField]
    private int height;
    [SerializeField]
    private List<Trunk> trunks = new List<Trunk>();

    private float prefabHeight = 0;

    bool canColapse = true;

    public void SetTree(int height, Vector3 position)
    {
        Vector3 lastPosition = new Vector3(0f, 0f, 0f);

        this.height = height;

        for (int i = 0; i < height; i++)
        {
            trunks.Add(Pool.instance.GetAvaliablePrefab());

            if (prefabHeight == 0)
                prefabHeight = trunks[i].GetComponent<Renderer>().bounds.size.y;

            float diference = prefabHeight;
            if (i == 0)
                diference = 0;

            trunks[i].transform.position = new Vector3(lastPosition.x, lastPosition.y + diference, lastPosition.z);
            lastPosition = trunks[i].transform.position;

            trunks[i].gameObject.transform.SetParent(trunksParent);
        }

        gameObject.transform.position = position;

        //gameObject.transform.Translate(0f, 5.5f, 0f);
    }

    public bool DestroyTrunk()
    {
        //Wait for the coroutine to end for animate another process
        if (canColapse == false) return false;

        if (trunks.Count > 0)
        {
            trunks[0].RemoveFromTree();
            trunks.RemoveAt(0);
            canColapse = false;
            StartCoroutine(TreeColapsingRoutine());
        }

        return trunks.Count == 0 ? true : false;
    }

    IEnumerator TreeColapsingRoutine()
    {
        yield return null;

        float finalPosition = gameObject.transform.position.y - prefabHeight;

        while (gameObject.transform.position.y > finalPosition)
        {
            gameObject.transform.Translate(0f, - prefabHeight / (animationTime * 60), 0f);

            yield return null;
        }

        canColapse = true;

        yield return null;
    }
}
