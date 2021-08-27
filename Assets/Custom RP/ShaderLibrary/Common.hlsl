#ifndef COSTOM_COMMON_INCLUDE
#define COSTOM_COMMON_INCLUDE

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

#include "UnityInput.hlsl"

#define UNITY_MATRIX_M unity_ObjectToWorld
#define UNITY_MATRIX_I_M unity_WorldToObject
#define UNITY_MATRIX_V unity_MatrixV
#define UNITY_MATRIX_VP unity_MatrixVP
#define UNITY_MATRIX_P glstate_matrix_projection

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/SpaceTransforms.hlsl"

////object space => world space
//float3 TransformObjectToWorld(float3 positionOS)
//{
//	return mul(unity_ObjectToWorld,float4(positionOS,1.0).xyz);
//}

////world space => clip space
//float4 TransformWorldToHClip(float3 positionWS)
//{
//	return mul(unity_MatrixVP,float4(positionWS,1.0));
//}

#endif