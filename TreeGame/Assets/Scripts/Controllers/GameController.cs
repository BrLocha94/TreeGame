using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Scene external references")]
    [SerializeField]
    private SpawController spawController;
    [SerializeField]
    private SceneController sceneController;

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
    [SerializeField]
    private int roundTime;

    List<Tree> listTrees = new List<Tree>();
    Tree targetTree;

    int score;
    int trunksDestroyed;
    int treesDestroyed;

    int countDown;
    float time;

    public static GameController instance;

    void Awake()
    {
        instance = this;

        //Force creation off an resultsholder object
        ResultsHolder.instance();
    }

    void Start()
    {
        RestartRound();
    }

    public void RestartRound()
    {
        CancelInvoke("CountTime");

        if (listTrees.Count > 0)
        {
            for (int i = 0; i < listTrees.Count; i++)
            {
                Tree tree = listTrees[0];
                listTrees.RemoveAt(0);
                tree.DestroyTree(true);
            }

            targetTree = null;
        }

        VisualController.instance.ResetUI(roundTime);

        countDown = 3;
        time = (float)roundTime;

        score = 0;
        trunksDestroyed = 0;
        treesDestroyed = 0;

        CreateNewTree();
        InvokeRepeating("CountDown", 0, 1f);
    }

    private void CountDown()
    {
        if(countDown == 0)
        {
            CancelInvoke("CountDown");
            VisualController.instance.StartRound();

            // To create and continuous countdown effect, repeat this call every 0.01 seconds
            InvokeRepeating("CountTime", 0f, 0.01f);
        }
        VisualController.instance.SetCountDownTimer(countDown);

        countDown--;
    }

    private void CountTime()
    {
        time -= 0.01f;

        // Game over
        if(time <= 0f)
        {
            // Fill the object with important data and change scene
            ResultsHolder.instance().score = score;
            ResultsHolder.instance().treesDestroyed = treesDestroyed;
            ResultsHolder.instance().trunksDestroyed = trunksDestroyed;

            sceneController.LoadResuls();
        }
        else
            VisualController.instance.UpdateTimeValue(time);
    }

    public void AddScore(int value)
    {
        score += value;
        VisualController.instance.UpdateScoreValue(score);
    }

    public void CountTrunk()
    {
        trunksDestroyed++;
    }

    public void CountTree()
    {
        treesDestroyed++;
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
            tree.DestroyTree(true);
        }
    }
}
