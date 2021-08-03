using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//CustomRenderPipelineAsset是一种资源类型，CreateAssetMenu属性可以告诉unity自己是一种asset类型
[CreateAssetMenu]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return null;
    }

}
