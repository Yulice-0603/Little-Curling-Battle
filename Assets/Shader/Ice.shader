Shader "ice" {
    Properties {
        _Maincolor ("Maincolor", Color) = (0,0.6720357,1,1)
        _Fre_Exp ("Fre_Exp", Float ) = 1.5
        _Fre_Col ("Fre_Col", Color) = (0.495283,0.744531,1,1)
        _Cube ("Cube", Cube) = "_Skybox" {}
        _HighLight_St ("HighLight_St", Float ) = 0.3
                    _OutlineCol("OutlineCol", Color) = (0,0,0,0)
        _OutlineFactor("OutlineFactor", Range(0,2)) = 1.52
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Maincolor;
            uniform float _Fre_Exp;
            uniform float4 _Fre_Col;
            uniform samplerCUBE _Cube;
            uniform float _HighLight_St;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 emissive = ((_Fre_Col.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fre_Exp))+_Maincolor.rgb+(texCUBE(_Cube,viewReflectDirection).rgb*_HighLight_St));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}
