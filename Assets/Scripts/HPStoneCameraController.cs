using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPStoneCameraController : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Camera>().targetTexture.Release();
    }
}
