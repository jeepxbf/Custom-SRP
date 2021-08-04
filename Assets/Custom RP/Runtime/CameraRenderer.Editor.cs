using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

//作为CameraRenderer的分类 ，release版本的时候不打进包里
//对于错误的材质，我们只在编辑器中调试
partial class CameraRenderer
{
    partial void DrawUnsupportedShaders();
    partial void DrawGizmos();
    partial void PrepareForSceneWindow();
#if UNITY_EDITOR
    //其他的shader passes 也需要被支持 否则会有物体绘制不出来
    static ShaderTagId[] legacyShaderTagIds =
    {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM")
    };
    //错误的材质
    static Material errorMaterial;


    partial void DrawUnsupportedShaders()
    {
        if(errorMaterial == null)
        {
            errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));//使用默认的品红色错误材质
        }

        var drawingSettings = new DrawingSettings(legacyShaderTagIds[0], new SortingSettings(camera))
        {
            overrideMaterial = errorMaterial
        };
        for(int i = 1;i<legacyShaderTagIds.Length;i++)
        {
            drawingSettings.SetShaderPassName(i, legacyShaderTagIds[i]);
        }
        var filteringSettings = FilteringSettings.defaultValue;
        context.DrawRenderers(cullingResults,ref drawingSettings,ref filteringSettings);
    }

    partial void DrawGizmos()
    {
        if(Handles.ShouldRenderGizmos())
        {
            context.DrawGizmos(camera, GizmoSubset.PreImageEffects);
            context.DrawGizmos(camera, GizmoSubset.PostImageEffects);
        }
    }

    partial void PrepareForSceneWindow()
    {
        if(camera.cameraType == CameraType.SceneView)
        {
            ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
        }
    }

#endif
}

