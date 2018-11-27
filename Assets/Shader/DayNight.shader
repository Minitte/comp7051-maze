// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/DayNight" {
// Adapted from tutorials on Unity website

	

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _AmbientLightColor("Ambient Light Color", Color) = (1,1,1,1)
        _AmbientLighIntensity("Ambient Light Intensity", Range(0.0, 1.0)) = 1.0

		_DiffuseDirection("Diffuse Light Direction", Vector) = (0,0,0,0)
		_DiffuseLightColor("Diffuse Light Color", Color) = (1,1,1,1)
		_DiffuseLightIntensity("Diffuse Light Intensity", Range(0.0, 1.0)) = 1.0
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
                //fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : NORMAL;
            };
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); 
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.texcoord;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                //half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                //o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(o.worldNormal,1));
                // compute shadows data
                TRANSFER_SHADOW(o)
                return o;
            }

            sampler2D _MainTex;

			// Flashlight global properties
			Vector _FlashlightPoint;
			Vector _FlashlightDirection;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed shadow = SHADOW_ATTENUATION(i);

				// Calculate ambient light
				fixed3 ambient = (unity_AmbientSky * _AmbientLighIntensity) * (_AmbientLightColor * _AmbientLighIntensity);

				// Calculate diffuse light
				fixed3 diffuse = _DiffuseLightColor * dot(-_DiffuseDirection, i.worldNormal);

                float3 playerToThis = i.worldPos - _WorldSpaceCameraPos;
                float3 playerToPoint = _FlashlightPoint - _WorldSpaceCameraPos;

                float3 projection = dot(playerToThis, playerToPoint) / pow(length(playerToPoint), 2) * playerToPoint;

                

                

                //float4 int1 = lerp(col, 0, saturate(normalize(dot(_FlashlightDirection, i.worldNormal))));
                //float4 int2 = lerp(saturate(int1 + 0.5), int1, saturate(distance(_WorldSpaceCameraPos, i.worldPos)));

                //return int2;
                
				// Calculate final lighting
				fixed3 lighting = saturate(diffuse) * shadow + ambient;
                col.rgb *= lighting;

				// Pixels closer to the flashlight point become brighter
				float flashlight = lerp(1, 0, distance(_FlashlightPoint, i.worldPos));

                if (distance(i.worldPos, _WorldSpaceCameraPos + projection) < 0.5) {
                    return saturate(col + 0.1);
                } else {
                    return col;
                }
                return lerp(col, saturate(col + 1), saturate(flashlight));
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
