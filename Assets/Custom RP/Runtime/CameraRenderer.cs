using UnityEngine;
using UnityEngine.Rendering;

public partial class CameraRenderer
{
    ScriptableRenderContext context;
    Camera camera;

    const string bufferName = "Renderer Camera";
    //CommandBuffer携带一系列的渲染命令，依赖相机，用来拓展渲染管线的渲染效果。
   // 而且可以指定在相机渲染的某个点执行本身的拓展渲染。    
    CommandBuffer buffer = new CommandBuffer { name = bufferName };

    //视锥剪裁中能看到的物体
    CullingResults cullingResults;

    //支持unlitshader passes
    static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    
    //绘制相机看到的所有几何图形
    public void Render(ScriptableRenderContext context,Camera camera)
    {
        this.context = context;
        this.camera = camera;


        PrepareBuffer();
        PrepareForSceneWindow(); //把UI添加到scene窗口下
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
        //不透明排序
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
        buffer.EndSample(SampleName);
        ExecuteBuffer();
        context.Submit();
    }

    void Setup()
    {
        /*
         使用SetupCameraProperties会执行一系列步骤，比如将相机的RenderTarget设置为当前的渲染目标，
        设置相机的参数（FOV、远近裁剪平面等），设置Shader里常用的Model-View-Proj变换矩阵等。当然我
        们也可以不使用这个函数而手动设置参数，但是一般情况下，使用这个函数进行前期的准备工作可以为我们
        节省很多代码量。
         */
        context.SetupCameraProperties(camera);
        //CameraClearFlag枚举定义了四个值。从1到4，它们是Skybox，Color，Depth和Nothing\
        //它实际上不是一个独立的标志值，但表示清除量递减。除最后一种情况外，其他情况都必须清除深度缓冲区，因此，标志值最多的设置是Depth。
        CameraClearFlags flags = camera.clearFlags;
        //参数 是否清除深度值 是否清除颜色值 清除后的颜色 清除后的深度 默认是1
        //
        buffer.ClearRenderTarget(flags<=CameraClearFlags.Depth, 
            flags==CameraClearFlags.Color, 
            flags==CameraClearFlags.Color?camera.backgroundColor:Color.clear);
        buffer.BeginSample(SampleName); //用于性能分析采样        
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

