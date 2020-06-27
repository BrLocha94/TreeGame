using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float movimentSpeed;

    [SerializeField]
    private float distanceFromTraget;

    Vector3 initialTarget;

    Coroutine lastRoutine;
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetInitialTarget(Vector3 target)
    {
        initialTarget = target;
    }

    public void SetNextTarget(Vector3 target)
    {
        if(initialTarget == null)
        {
            Debug.Log("Initial target not setted");
            return;
        }

        if (lastRoutine != null)
            StopCoroutine(lastRoutine);

        float newX, newZ;

        newX = target.x - transform.position.x;
        newZ = target.z - initialTarget.z;

        Vector3 newPosition = new Vector3(newX, transform.position.y, newZ);

        lastRoutine = StartCoroutine(CameraMoveRoutine(newPosition));
    }

    IEnumerator CameraMoveRoutine(Vector3 newPosition)
    {
        yield return null;

        while (gameObject.transform.position.z < newPosition.z - distanceFromTraget)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, newPosition, movimentSpeed * Time.deltaTime);

            yield return null;
        }

        yield return null;
    }
}
