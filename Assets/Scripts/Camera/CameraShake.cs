using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShaveBehavior(duration, magnitude));
    }

    IEnumerator ShaveBehavior(float duration, float magnitude)
    {
        Vector3 originalPositon = transform.position;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPositon.x + Random.Range(-1f, 1f) * magnitude;
            float y = originalPositon.y + Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPositon.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPositon;
    }

    public static implicit operator CameraShake(GameObject v)
    {
        throw new System.NotImplementedException();
    }
}