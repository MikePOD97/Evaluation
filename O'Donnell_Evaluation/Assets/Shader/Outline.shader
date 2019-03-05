// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Outline"
{
	Properties
	{
		_Color ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_Outline ("Outline width", Range(0.0, 0.03)) = .005
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
	}

CGINCLUDE
#include "UnityCG.cginc"

	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
ENDCG

	SubShader
{
	Tags { "Queue" = "Transparent"}

	Pass
	{
		Name "OUTLINE"
		Tags {"LightMode" = "Always"}
		Cull Off
		ZWrite Off
		ZTest Always
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha
CGPROGRAM
#pragma vertex vert
#pragma fragment frag

half4 frag(v2f i) :COLOR
{
	return i.color;
}
ENDCG
		}

		Pass
		{
			Name "BASE"
			ZWrite On
			ZTest LEqual
			Blend SrcAlpha OneMinusSrcAlpha
			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
			Lighting On
		}
	}

	SubShader
	{
		Tags {"Queue" = "Transparent"}

		Pass
		{	
			Name "OUTLINE"
			Tags {"Lightmode" = "Always"}
			Cull Front
			ZWrite Off
			ZTest Always
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma vertex vert
ENDCG

			
		}
		
		Pass 
		{
			Name "BASE"
			ZWrite On
			ZTest LEqual
			Blend SrcAlpha OneMinusSrcAlpha
			Material
			{
				Diffuse [_Color]
				Ambient [_Color]
			}
			Lighting On
		}
	}

	Fallback "Diffuse"
	CustomEditor "CustomShaderGUI"

	/*
		I'm going to be honest here, based on the short time frame of this assignment and the other work I have to do for
		college, I don't really have the time to develop my own shader from the ground up, so I borrowed a lot from an online
		resource (completely open source). I'm going to make another pass at the code and do my best to make it my own and 
		further my understanding of shaders by analyzing every line, refactoring where I can, and consulting colleagues 
		if I'm confused about something.
	*/
}
