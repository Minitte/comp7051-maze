Shader "Unlit/Ozma"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_EmissionMap("Emission Map", 2D) = "black" {}
		[HDR]_EmissionColor ("Emission Color", Color) = (0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		Alphatest Greater 0
		ZWrite Off
		Blend SrcAlpha One
		ColorMask RGB
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _EmissionMap;
			float3 _EmissionColor;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				
				float4 emission = tex2D(_EmissionMap, i.uv) * float4(_EmissionColor, 1);
				col.rgb += emission.rgb;
				col.a = 0.3;
				return col;
			}
			ENDCG
		}
	}
}
