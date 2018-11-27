// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/DayNight" {
// Adapted from tutorials on Unity website

	

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0
    }

	SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            #include "AutoLight.cginc"

			// Ambient light properties
            float3 _AmbientLightColor;
            float _AmbientLighIntensity;

			// Diffuse light properties
			float4 _DiffuseDirection;
			float4 _DiffuseLightColor;
			float _DiffuseLightIntensity;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				//float3 worldNormal : NORMAL;
            };
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); 
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.texcoord;
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				o.screenPos = ComputeScreenPos(o.pos);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));
                // compute shadows data
                TRANSFER_SHADOW(o)
                return o;
            }

            sampler2D _MainTex;

			// Flashlight global properties
			Vector _FlashlightPoint;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed shadow = SHADOW_ATTENUATION(i);

				// Calculate ambient light
				fixed3 ambient = (unity_AmbientSky * _AmbientLighIntensity) * (_AmbientLightColor * _AmbientLighIntensity);

				// Calculate diffuse light
				fixed3 diffuse = i.diff;
                
				// Calculate final lighting
				fixed3 lighting = saturate(diffuse) * shadow + ambient;
                col.rgb *= lighting;

				// Position in screen coordinates (0 to 1)
				float2 screenPosition = (i.screenPos.xy / i.screenPos.w);

				// Additional lighting if the flashlight is toggled on
				if (length(_FlashlightPoint) > 0) {
					// Project this pixel onto the closest point on the line
					float3 playerToThis = i.worldPos - _WorldSpaceCameraPos;
					float3 playerToPoint = _FlashlightPoint - _WorldSpaceCameraPos;
					float3 projection = dot(playerToThis, playerToPoint) / pow(length(playerToPoint), 2) * playerToPoint;
					float dist = distance(screenPosition, float2(0.5, 0.5)) * 5; // Distance to center of screen position
					col = saturate(lerp(col + 0.2, col, saturate(dist))); // Interpolate so that pixels closer to the center of the screen are brighter
					return saturate(lerp(col + 0.3 , col, saturate((distance(i.worldPos, _WorldSpaceCameraPos + projection))))); // Interpolate so that pixels closer to the ray are brighter
				}

				return col;
            }
            ENDCG
        }

        Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
