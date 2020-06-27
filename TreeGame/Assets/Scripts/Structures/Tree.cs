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

    private Vector3 position;
    private float prefabHeight = 0;

    bool canColapse = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetTree(int height, Vector3 position)
    {
        this.position = position;

        //gameObject.transform.position = position;

        Vector3 lastPosition = gameObject.transform.position;

        this.height = height;

        for (int i = 0; i < height; i++)
        {
            trunks.Add(Pool.instance.GetAvaliablePrefab());

            if (prefabHeight == 0)
                prefabHeight = trunks[i].GetComponent<Renderer>().bounds.size.y;

            float diference = prefabHeight;
            if (i == 0)
            {
                diference = 0;
                transform.position = new Vector3(transform.position.x, prefabHeight / 2, transform.position.z);
                lastPosition = transform.position;
            }

            trunks[i].transform.position = new Vector3(lastPosition.x, lastPosition.y + diference, lastPosition.z);
            lastPosition = trunks[i].transform.position;

            trunks[i].gameObject.transform.SetParent(trunksParent);
        }

        RandomAnimation();
    }

    private void RandomAnimation()
    {
        gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);

        gameObject.transform.position = position;

        int random = Random.Range(0, 3);

        if (random == 0)
            anim.Play("Bounce");
        else if (random == 1)
            anim.Play("Smooth");
        else
            anim.Play("Pop");
    }

    IEnumerator DebugRoutine(int height, Vector3 position)
    {
        gameObject.transform.position = position;

        Debug.Log(gameObject.transform.position);

        Vector3 lastPosition = gameObject.transform.position;

        this.height = height;

        for (int i = 0; i < height; i++)
        {
            yield return new WaitForSeconds(0.5f);

            trunks.Add(Pool.instance.GetAvaliablePrefab());

            if (prefabHeight == 0)
                prefabHeight = trunks[i].GetComponent<Renderer>().bounds.size.y;

            float diference = prefabHeight;
            if (i == 0)
            {
                diference = 0;
                transform.position = new Vector3(transform.position.x, prefabHeight/2, transform.position.z);
                lastPosition = transform.position;
            }

            trunks[i].transform.position = new Vector3(lastPosition.x, lastPosition.y + diference, lastPosition.z);
            lastPosition = trunks[i].transform.position;

            yield return new WaitForSeconds(0.5f);

            trunks[i].gameObject.transform.SetParent(trunksParent);
        }
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
