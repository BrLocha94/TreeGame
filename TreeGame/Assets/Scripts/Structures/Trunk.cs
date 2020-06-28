using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [Header("Score based values")]
    [SerializeField]
    private int baseScore;

    public void RemoveFromTree(bool forceRemove = false)
    {
        if (forceRemove == false)
        {
            GameController.instance.AddScore(baseScore);
            GameController.instance.CountTrunk();
        }

        gameObject.transform.SetParent(null);
        //Return to an out off frustrum position to wait for pool call
        gameObject.transform.position = new Vector3(0f, -50f, 0f);
        gameObject.SetActive(false);
    }
}
