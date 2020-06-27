using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Scene external references")]
    [SerializeField]
    private SpawController spawController;

    [Header("Tree spaw params")]
    [SerializeField]
    private Tree treePrefab;
    [SerializeField]
    [Range(7, 20)]
    private int heightRange;

    bool hasSettedTarget = false;

    List<Tree> trees = new List<Tree>();

    void Start()
    {
        CreateNewTree();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            bool result = trees[0].DestroyTrunk();
            if (result == true)
            {
                trees.RemoveAt(0);
                CreateNewTree();
            }
        }
    }

    private void CreateNewTree()
    {
        int randomHeight = Random.Range(7, heightRange);

        Vector3 newPosition = spawController.GetSpawPosition();
        Tree newTree = Instantiate(treePrefab);

        trees.Add(newTree);
        trees[trees.Count - 1].SetTree(randomHeight, spawController.GetSpawPosition());

        if (hasSettedTarget == false)
        {
            CameraController.instance.SetInitialTarget(newTree.transform.position);
            hasSettedTarget = true;
        }
        else
            CameraController.instance.SetNextTarget(newTree.transform.position);

    }
}
