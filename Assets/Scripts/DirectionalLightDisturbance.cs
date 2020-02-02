using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightDisturbance : MonoBehaviour
{
    public float speed = 0.1f;
    public float range = 0.1f;
    private float yOriginal;
    // Start is called before the first frame update
    void Start()
    {
        yOriginal = transform.rotation.y;
        StopAllCoroutines();
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    IEnumerator Flicker()
    {
        float yDir = transform.rotation.y;
        float random = Random.Range(-range, range);
        yDir = yOriginal + random;
        transform.rotation = Quaternion.Euler(transform.rotation.x, yDir, transform.rotation.z);
        yield return new WaitForSeconds(speed);
        StartCoroutine(Flicker());
    }

    private void OnApplicationQuit() {
        StopAllCoroutines();
    }
}
