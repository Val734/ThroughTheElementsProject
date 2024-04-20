using UnityEngine;
using TMPro;

public class ShowFPS : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    float deltaTime = 0.0f;

    void Start()
    {
        fpsText.fontSize = 24;
        fpsText.color = Color.white;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void LateUpdate()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        fpsText.text = text;
    }
}
