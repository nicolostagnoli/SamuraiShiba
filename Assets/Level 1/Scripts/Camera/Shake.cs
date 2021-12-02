using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //To test from the inspector
    public bool start = false;

    public CinemachineBrain cinemachineBrain;
    public AnimationCurve curve; 
    public float duration = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            StartCoroutine(Shaking());
        }
        /*To test from the inspector
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }*/
    }
    
    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            cinemachineBrain.enabled = false;
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
        cinemachineBrain.enabled = true;
    }
}
