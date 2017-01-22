using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        float expectedAspectRatio = 16f / 9f;
        var camera = GetComponent<Camera>();
        float expectedWidth = camera.orthographicSize * expectedAspectRatio;
        float newHeight = (1 / camera.aspect) * expectedWidth;
        camera.orthographicSize = newHeight;
    }
}
