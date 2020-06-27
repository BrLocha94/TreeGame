using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float movimentSpeed;

    [SerializeField]
    private float distanceFromTarget;

    Vector3 target;

    Coroutine lastRoutine;
    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetNextTarget(Vector3 target)
    {
        if (lastRoutine != null)
            StopCoroutine(lastRoutine);

        float newX, newZ;

        newX = target.x;
        newZ = target.z - distanceFromTarget;

        Vector3 newPosition = new Vector3(newX, transform.position.y, newZ);

        lastRoutine = StartCoroutine(CameraMoveRoutine(newPosition));
    }

    IEnumerator CameraMoveRoutine(Vector3 newPosition)
    {
        yield return null;

        while (gameObject.transform.position.z < newPosition.z)
        {
            gameObject.transform.position = Vector3.Lerp(transform.position, newPosition, movimentSpeed * Time.deltaTime);

            yield return null;
        }

        yield return null;
    }
}
