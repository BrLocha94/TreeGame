using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField]
    private float destructionTime = 0.15f;


    public void RemoveFromTree()
    {
        gameObject.transform.SetParent(null);
        //Return to an out off frustrum position to wait for pool call
        gameObject.transform.position = new Vector3(0f, -50f, 0f);
        gameObject.SetActive(false);
    }
}
