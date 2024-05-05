using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyOrbBehaviour : MonoBehaviour
{
    [SerializeField] float scaleTime = 5f;
    [SerializeField] float scaleIncrement = 5f;

    float elapsedTime;
    Vector3 initialScale;

    AudioSource bodyOrbSound;

    private void Awake()
    {
        bodyOrbSound = GetComponent<AudioSource>();
        initialScale = transform.localScale;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float scaleFactor = Mathf.Lerp(0f, scaleIncrement, elapsedTime / scaleTime);

        Vector3 newScale = initialScale + Vector3.one * scaleFactor;

        transform.localScale = newScale;

        if (elapsedTime >= scaleTime)
        {
            //Instanciar Partiuclas De Explisión De Agua
            Destroy(gameObject);
        }
    }
}
