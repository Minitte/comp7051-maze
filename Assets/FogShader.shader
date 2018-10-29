/*
	References:
	http://willychyr.com/2013/11/unity-shaders-depth-and-normal-textures/
*/

Shader "Custom/FogShader" {
	Properties
	{
		_MainTex("White Texture", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{

		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		sampler2D _CameraDepthTexture;
		
		// Container for information that is passed from vertex to fragment
		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 pos : SV_POSITION;
			float4 scrPos : TEXCOORD1;
		};

		sampler2D _MainTex;

		// Vertex Shader
		v2f vert(appdata_full v)
		{
			v2f output;
			output.uv = v.texcoord.xy; // Texture map
			output.pos = UnityObjectToClipPos(v.vertex); // Vertex's world position
			output.scrPos = ComputeScreenPos(output.pos); // Vertex's screen position
			return output;
		}

		// Fragment Shader
		float4 frag(v2f input) : COLOR
		{
			// Value that increases depending on how far away the object is
			float depthValue = Linear01Depth(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(input.scrPos)).r);
			float4 texColour = tex2D(_MainTex, input.uv); // The actual colours of the texture

			// Calculate final colours, further objects will appear more white
			float4 depthColour;
			depthColour.r = (texColour.r * 0.5) + depthValue;
			depthColour.g = (texColour.g * 0.5) + depthValue;
			depthColour.b = (texColour.b  * 0.5) + depthValue;
			depthColour.a = 1;

			return depthColour;
		}
		ENDCG
		}
	}
		FallBack "Diffuse"
}
