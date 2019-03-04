Shader "Unlit/Outline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline color", Color) = (0,0,0,1)
	    _OutlineWidth("Outline width", Range(1.0, 10.0)) = 2.5
	}
	
	GCINCLUDE
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
		float3 normal : NORMAL;
	};

	float4 _OutlineColor;
	float _OutlineWidth;

	v2f vert(appdata v)
	{
		v.vertex.xyz *= _Outline;

		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.color = _OutlineColor;
		return o;
	}

	ENDCG

	SubShader
	{
		
	}
}
