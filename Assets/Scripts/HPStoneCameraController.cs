using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HPStoneCameraController : MonoBehaviour
{
    // TO-DO: Having some bug, the object can't rendering to renderTexture.
    // Now only using camera view to left-bottom

    public GameObject targetObject;
    public RenderTexture renderTexture;
    public RawImage rawImg_HP;
    private CommandBuffer commandBuffer;

    void OnEnable()
    {
        Renderer[] targetRenderer = targetObject.GetComponentsInChildren<Renderer>();
        commandBuffer = new CommandBuffer();
        commandBuffer.SetRenderTarget(renderTexture);
        commandBuffer.ClearRenderTarget(true, true, Color.clear);
        
        foreach (var tr in targetRenderer)
        {
            commandBuffer.DrawRenderer(tr, tr.sharedMaterial);
        }

        GetComponent<Camera>().targetTexture = renderTexture;
        rawImg_HP.texture = renderTexture;
        GetComponent<Camera>().AddCommandBuffer(CameraEvent.AfterEverything, commandBuffer);
    }

    void OnDisable()
    {
        GetComponent<Camera>().RemoveCommandBuffer(CameraEvent.AfterForwardOpaque, commandBuffer);
        commandBuffer.Clear();
        renderTexture.Release();
    }
    void OnPreRender()
    {
        Graphics.ExecuteCommandBuffer(commandBuffer);
    }
}
