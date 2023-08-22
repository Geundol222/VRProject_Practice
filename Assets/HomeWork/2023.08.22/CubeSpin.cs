using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpin : MonoBehaviour
{
    [SerializeField] float spinSpeed;

    public void SpinStart()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        while (true)
        {
            transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);

            yield return null;
        }
    }

    public void SpinEnd()
    {
        StopAllCoroutines();
    }
}
