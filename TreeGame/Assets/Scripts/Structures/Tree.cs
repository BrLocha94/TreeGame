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

    [Header("General tree presets")]
    [SerializeField]
    private float animationTime = 1f;
    [SerializeField]
    private int baseScore;

    int height;
    List<Trunk> listTrunk = new List<Trunk>();

    Vector3 position;
    float prefabHeight = 0;

    bool canColapse = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetTree(int height, Vector3 position)
    {
        this.position = position;
        this.height = height;

        Vector3 lastPosition = gameObject.transform.position;

        for (int i = 0; i < height; i++)
        {
            listTrunk.Add(Pool.instance.GetAvaliablePrefab());

            if (prefabHeight == 0)
                prefabHeight = listTrunk[i].GetComponent<Renderer>().bounds.size.y;

            float diference = prefabHeight;
            if (i == 0)
            {
                diference = 0;
                transform.position = new Vector3(transform.position.x, prefabHeight / 2, transform.position.z);
                lastPosition = transform.position;
            }

            listTrunk[i].transform.position = new Vector3(lastPosition.x, lastPosition.y + diference, lastPosition.z);
            lastPosition = listTrunk[i].transform.position;

            listTrunk[i].gameObject.transform.SetParent(trunksParent);
        }

        RandomAnimation();
    }

    private void RandomAnimation()
    {
        // Set the object to an predefined scale for clean grow animation performace 
        gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        gameObject.transform.position = position;

        int random = Random.Range(0, 3);

        if (random == 0)
            anim.Play("Bounce");
        else if (random == 1)
            anim.Play("Smooth");
        else
            anim.Play("Pop");

        // Prevents Destroy trunk calls while tree is still on grow animation
        Invoke("StartTree", 1.1f);
    }

    private void StartTree()
    {
        canColapse = true;
    }

    public void DestroyTree(bool forceDestroy = false)
    {
        if (listTrunk.Count == 0 && forceDestroy == false)
        {
            GameController.instance.AddScore(baseScore);
            GameController.instance.CountTree();
        }

        while(listTrunk.Count > 0)
        {
            Trunk trunk = listTrunk[0];
            listTrunk.RemoveAt(0);
            trunk.RemoveFromTree(true);
        }

        Destroy(gameObject);
    }

    public bool DestroyTrunk()
    {
        //Wait for the coroutine to end for animate another process
        if (canColapse == false) return false;

        if (listTrunk.Count > 0)
        {
            listTrunk[0].RemoveFromTree();
            listTrunk.RemoveAt(0);
            canColapse = false;
            StartCoroutine(TreeColapsingRoutine());
        }

        return listTrunk.Count == 0 ? true : false;
    }

    IEnumerator TreeColapsingRoutine()
    {
        yield return null;

        float finalPosition = gameObject.transform.position.y - prefabHeight;

        while (gameObject.transform.position.y > finalPosition)
        {
            // Translate the tree with and speed based on the distance/time
            // Time is based on and 60 fps performace
            gameObject.transform.Translate(0f, - prefabHeight / (animationTime * 60), 0f);

            yield return null;
        }

        canColapse = true;
    }
}
