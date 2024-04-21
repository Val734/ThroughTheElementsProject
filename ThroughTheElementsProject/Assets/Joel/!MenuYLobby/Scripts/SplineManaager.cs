using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Spline : MonoBehaviour
{
    [SerializeField] GameObject rutas;
    [SerializeField] SplineContainer splineContainer;
    [SerializeField] Vector3[] pathpoints;
    [SerializeField] float maxvalue;
    [SerializeField] float pathLength;
    [SerializeField] float speed;
    public int currentIndex;
    private float distancebetweenpoints = 2f;

    private void Awake()
    {
            splineContainer = rutas.GetComponentInChildren<SplineContainer>();
    }
    void Start()
    {
       ObtainPoints();

    }

    private void Update()
    {
        ChangePathPoint();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathpoints[currentIndex],speed * Time.deltaTime);
    }

    private void ChangePathPoint()
    {
        if (pathpoints[currentIndex] == gameObject.transform.position)
        {
             currentIndex++;
        }
        if (currentIndex == maxvalue)
        {
            currentIndex = 0;
        }

    }

    // Update is called once per frame

    void ObtainPoints()
    {
        pathLength = splineContainer.CalculateLength();
        int pathpointsLength = Mathf.FloorToInt(pathLength / distancebetweenpoints);
        pathpoints = new Vector3[pathpointsLength];

        for(int i = 0;i< pathpointsLength; i++) 
        {
            float n = (float)i / (float)pathpointsLength;
            pathpoints[i] = splineContainer.EvaluatePosition(n);
        }
    }
}
