using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//这个是CustomRenderPipelineAsset 的返回
public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach(Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }
}
