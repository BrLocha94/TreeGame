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

    List<Tree> trees = new List<Tree>();

    void Start()
    {
        CreateNewTree();
        CreateNewTree();
    }

    private void CreateNewTree()
    {
        int randomHeight = Random.Range(7, heightRange);

        trees.Add(Instantiate(treePrefab));
        trees[trees.Count - 1].SetTree(randomHeight, spawController.GetSpawPosition());
    }
}
