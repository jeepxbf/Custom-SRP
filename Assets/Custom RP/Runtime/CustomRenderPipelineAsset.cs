using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//CustomRenderPipelineAsset��һ����Դ���ͣ�CreateAssetMenu���Կ��Ը���unity�Լ���һ��asset����
[CreateAssetMenu]
public class CustomRenderPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return null;
    }

}
