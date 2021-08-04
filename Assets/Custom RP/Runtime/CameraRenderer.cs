using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    ScriptableRenderContext context;
    Camera camera;

    const string bufferName = "Renderer Camera";
    //CommandBufferЯ��һϵ�е���Ⱦ������������������չ��Ⱦ���ߵ���ȾЧ����
   // ���ҿ���ָ���������Ⱦ��ĳ����ִ�б������չ��Ⱦ��    
    CommandBuffer buffer = new CommandBuffer { name = bufferName };

    //��׶�������ܿ���������
    CullingResults cullingResults;

    //֧��unlitshader passes
    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    
    //����������������м���ͼ��
    public void Render(ScriptableRenderContext context,Camera camera)
    {
        this.context = context;
        this.camera = camera;

        PrepareForSceneWindow(); //��UI��ӵ�scene������
        if(!Cull())
        {
            return;
        }

        Setup();
        DrawVisibleGeometry();
        DrawUnsupportedShaders();
        DrawGizmos();
        Submit();
    }
    
    void DrawVisibleGeometry()
    {
        //��͸������
        var sortingSetting = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
        var drawingSetting = new DrawingSettings(unlitShaderTagId,sortingSetting);
        var filteringSetting = new FilteringSettings(RenderQueueRange.opaque);

        context.DrawRenderers(cullingResults, ref drawingSetting, ref filteringSetting);
        context.DrawSkybox(camera);

        sortingSetting.criteria = SortingCriteria.CommonTransparent;
        drawingSetting.sortingSettings = sortingSetting;
        filteringSetting.renderQueueRange = RenderQueueRange.transparent;
        context.DrawRenderers(cullingResults, ref drawingSetting, ref filteringSetting);
    }

    void Submit()
    {
        buffer.EndSample(bufferName);
        ExecuteBuffer();
        context.Submit();
    }

    void Setup()
    {
        /*
         ʹ��SetupCameraProperties��ִ��һϵ�в��裬���罫�����RenderTarget����Ϊ��ǰ����ȾĿ�꣬
        ��������Ĳ�����FOV��Զ���ü�ƽ��ȣ�������Shader�ﳣ�õ�Model-View-Proj�任����ȡ���Ȼ��
        ��Ҳ���Բ�ʹ������������ֶ����ò���������һ������£�ʹ�������������ǰ�ڵ�׼����������Ϊ����
        ��ʡ�ܶ��������
         */
        context.SetupCameraProperties(camera);
        buffer.ClearRenderTarget(true, true, Color.clear);
        buffer.BeginSample(bufferName); //�������ܷ�������        
        ExecuteBuffer();
        
    }

    void ExecuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    bool Cull()
    {
        if(camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }
        return false;
    }


}

