using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsController : MonoBehaviour
{
    [Header("Ui general Text references")]
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private Text textTrunks;
    [SerializeField]
    private Text textTrees;

    //Cached reference to singleton
    ResultsHolder holder;

    void Awake()
    {
        holder = ResultsHolder.instance();
    }

    void Start()
    {
        textScore.text = "Score: " + holder.score.ToString();
        textTrunks.text = "Trunks: " + holder.trunksDestroyed.ToString();
        textTrees.text = "Trees: " + holder.treesDestroyed.ToString();
    }
}
