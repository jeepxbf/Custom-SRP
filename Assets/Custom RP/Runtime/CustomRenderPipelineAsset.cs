using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//CustomRenderPipelineAsset是一种资源类型，CreateAssetMenu属性可以告诉unity自己是一种asset类型
//asset是一种配置，他的作用是持有Pipeline的引用，真正实现渲染的也是pipeline

[CreateAssetMenu(menuName = "Rendering/Custom Render Pipeline")]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new CustomRenderPipeline();
    }

}
