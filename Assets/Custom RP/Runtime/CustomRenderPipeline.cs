using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//�����CustomRenderPipelineAsset �ķ���
//����ʵ����Ⱦ����
public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer = new CameraRenderer();

    //������һ����Ⱦ�������� ��һ��������� ÿһ֡�������
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach(Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }
}
