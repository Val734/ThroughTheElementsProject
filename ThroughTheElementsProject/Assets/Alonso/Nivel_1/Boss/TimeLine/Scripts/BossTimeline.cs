using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class BossTimeline : MonoBehaviour
{
    public TimelineAsset timeLine;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("AHORA EMPIEZA LA CINEMATICA");
        }
    }
}
