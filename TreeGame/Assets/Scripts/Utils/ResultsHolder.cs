using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsHolder : MonoBehaviour
{
    public int score { get; set; }
    public int trunksDestroyed { get; set; }
    public int treesDestroyed { get; set; }

    #region Instance

    // This is an singleton script
    // Structure forces to only exists 1 off this type
    // This approach Remove the necessity to manual controll off DontDestroyOnLoad in diferent scenes 
    private static ResultsHolder _instance;
    public static ResultsHolder instance()
    {
        // if the instance is already created, we return the reference
        if (_instance != null)
            return _instance;

        // Check if there is no object off the same type in the scene to preserve singleton 
        _instance = FindObjectOfType<ResultsHolder>();
        if(_instance == null)
        {
            // Find the object holding this script stored in the resources folder and if sucessfull, instantiate in the scene
            GameObject resourceObject = Resources.Load("ResultsHolder") as GameObject;
            if (resourceObject != null)
            {
                GameObject instantiatedObject = Instantiate(resourceObject);
                _instance = instantiatedObject.GetComponent<ResultsHolder>();
                DontDestroyOnLoad(instantiatedObject);
            }
            else
                Debug.Log("ResultsHolder prefab not founded on resources");
        }

        return _instance;
    }

    #endregion
    
}
