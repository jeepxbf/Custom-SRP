Shader "Custom RP/Unlit"
{

	Properties{}

	SubShader
	{
		pass
		{
			HLSLPROGRAM
			#pragma vertex UnlitPassVertex
			#pragma fragment UnlitPassFragment
			#include "UnlitPass.hlsl"
			ENDHLSL

		}
	}


}

