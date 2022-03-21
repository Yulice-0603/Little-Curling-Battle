Shader "Toon"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_RampThreshold("Ramp Threshold",Range(0,1)) = 0.24
		_RampSmooth("Smooth Threshold",Range(0,1)) = 0.1

		_Color("Color",Color) = (1,1,1,1)
		_HColor("Highlight Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SColor("Shadow Color", Color) = (0.639, 0.639, 0.639, 1.0)

		_SpecularColor("Specular Color", Color) = (0.2823, 0.2823, 0.2823, 1)
		_SpecThreshold("Specular Threshold", Range(0, 1)) = 0.75
		_SpecSmooth("Specular Smooth", Range(0, 1)) = 0.1
		_Shininess("Shininess", Range(0.001, 10)) = 0.2

		_RimColor("Rim Color", Color) = (0.8, 0.8, 0.8, 0.6)
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.5
		_RimSmooth("Rim Smooth", Range(0, 1)) = 0.1
		_OutlineCol("OutlineCol", Color) = (0,0,0,0)
		_OutlineFactor("OutlineFactor", Range(0,1)) = 0.003
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

				Pass
		{
				Cull Front

				CGPROGRAM
				#include "UnityCG.cginc"
				fixed4 _OutlineCol;
				float _OutlineFactor;

				struct v2f
				{
					float4 pos : SV_POSITION;
				};

				v2f vert(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					float3 vnormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
					float2 offset = TransformViewToProjection(vnormal.xy);
					o.pos.xy += offset * _OutlineFactor;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					return _OutlineCol;
				}

					#pragma vertex vert
					#pragma fragment frag
					ENDCG
				}

			Pass
			{
				Tags {"LightMode" = "ForwardBase"}

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "Lighting.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal:NORMAL;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float3 normal:TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _Color;
				float4 _SColor;
				float4 _HColor;
				float _RampThreshold;
				float _RampSmooth;

				float4 _SpecularColor;
				float _SpecThreshold;
				float _SpecSmooth;
				float _Shininess;

				float4 _RimColor;
				float _RimThreshold;
				float _RimSmooth;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					o.normal = mul(v.normal,(float3x3)unity_WorldToObject);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
					fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz);
					fixed3 halfDir = normalize(lightDir + viewDir);

					float3 normal = normalize(i.normal);
					float NdotL = saturate(dot(normal,lightDir));
					float NdotH = saturate(dot(normal,halfDir));
					float NdotV = saturate(dot(normal,viewDir));

					fixed3 ramp = smoothstep(_RampThreshold - _RampSmooth * 0.5,
					_RampThreshold + _RampSmooth * 0.5,NdotL);
					fixed4 albedo = tex2D(_MainTex, i.uv) * _Color;
					_SColor = lerp(_HColor,_SColor,_SColor.a);
					float3 rampColor = lerp(_SColor.rgb,_HColor.rgb,ramp);
					fixed3 diffuse = albedo.rgb * _LightColor0.rgb * rampColor;

					float gloss = albedo.a;
					float spec = pow(NdotH,_Shininess * 128) * gloss;
					spec = smoothstep(_SpecThreshold - _SpecSmooth * 0.5,
					_SpecThreshold + _SpecSmooth * 0.5,spec);
					fixed3 specular = _SpecularColor.rgb * _LightColor0.rgb * spec;

					float rim = (1 - NdotV) * NdotL;
					rim = smoothstep(_RimThreshold - _RimSmooth * 0.5,_RimThreshold + _RimSmooth * 0.5,rim);
					fixed3 rimColor = _RimColor.rgb * _LightColor0.rgb * _RimColor.a * rim;

					float4 finalColor = float4(diffuse + specular + rimColor,1);

					return finalColor;
				}
				ENDCG
			}
		}
}

