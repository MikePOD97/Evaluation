// This code heavily relies upon http://wiki.unity3d.com/index.php/Silhouette-Outlined_Diffuse as a resource.
//I edited some things to fit this program better, but I simply did not have the time to make my own shader from the ground up
//with my limited shader experience. I'm sorry for any inconvenience this causes.

Shader "Unlit/Outline"
{
	Properties
	{
		//Set the three main properties
		_Color ("Main Color", Color) = (0.5, 0.5, 0.5, 1)
		_Outline ("Outline width", Range(0.0, 0.03)) = .005
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
	}

CGINCLUDE
#include "UnityCG.cginc"

	//Handles model data
	struct appdata
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	//Converts vertex to fragment
	struct v2f
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	//Controls the outline width and color
	uniform float _Outline;
	uniform float4 _OutlineColor;

	//Handles the model's vertices and creates an outline from that.
	v2f vert(appdata v)
	{
		//Sets a variable for the outline and makes its position proportional to the vertices
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		//Gets the normals and offsets the outline based off of them
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		//Sets the position and the color
		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
ENDCG

	//All of this handles the diffuse and ambient shading in ways I'm not super clear on.
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

	
}
