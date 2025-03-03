using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalDoor : MonoBehaviour
{
    [SerializeField] private float moveDuration;
    private Vector3 startPos;
    private Vector3 targetPos;

    public void Open()
    {
        startPos = transform.position;
        float height = GetComponent<Renderer>().bounds.size.y;
        targetPos = startPos + new Vector3(0, height * 0.9f, 0);
    }

    public void StartOpen()
    {
        StartCoroutine(LerpPosition());
    }

    private IEnumerator LerpPosition()
    {
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }

}
