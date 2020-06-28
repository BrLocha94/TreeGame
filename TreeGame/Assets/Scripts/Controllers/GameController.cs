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
    [Range(10, 20)]
    private int heightRange;
    [SerializeField]
    private int spawLimit;

    [Header("Round controll params")]
    [SerializeField]
    private int treeLimitPerRound;

    List<Tree> listTrees = new List<Tree>();
    Tree targetTree;

    int countDown;

    void Start()
    {
        RestartRound();
    }

    public void RestartRound()
    {
        VisualController.instance.ResetUI();

        if(listTrees.Count > 0)
        {
            for(int i = 0; i < listTrees.Count; i++)
            {
                Tree tree = listTrees[0];
                listTrees.RemoveAt(0);
                tree.DestroyTree();
            }

            targetTree = null;
        }

        countDown = 3;

        CreateNewTree();
        InvokeRepeating("CountDown", 0, 1f);
    }

    private void CountDown()
    {
        if(countDown == 0)
        {
            CancelInvoke("CountDown");
            VisualController.instance.StartRound();
        }
        VisualController.instance.SetCountDownTimer(countDown);

        countDown--;
    }

    public void DestroyTrunk()
    {
        if (targetTree != null)
        {
            bool result = targetTree.DestroyTrunk();
            if (result == true)
            {
                Tree tree = listTrees[0];
                listTrees.RemoveAt(0);
                tree.DestroyTree();
                CreateNewTree();
            }
        }
    }

    public void CreateNewTree()
    {
        int randomHeight = Random.Range(7, heightRange);

        targetTree = Instantiate(treePrefab);
        listTrees.Add(targetTree);
        listTrees[listTrees.Count - 1].SetTree(randomHeight, spawController.GetSpawPosition());

        CameraController.instance.SetNextTarget(targetTree.transform.position);

        if (listTrees.Count > spawLimit)
        {
            Tree tree = listTrees[0];
            listTrees.RemoveAt(0);
            tree.DestroyTree();
        }
    }
}
