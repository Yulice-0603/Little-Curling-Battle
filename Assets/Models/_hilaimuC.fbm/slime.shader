// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "slime"
{
	Properties
	{
		_MatCap("MatCap", 2D) = "white" {}
		_BaseMap("BaseMap", 2D) = "white" {}
		_SlimeNormal("SlimeNormal", 2D) = "bump" {}
		_RimBias("RimBias  ", Float) = 0.17
		_Contrast("Contrast", Float) = 5
		_RimScale("RimScale", Float) = 0.71
		_RimPower("RimPower", Float) = 1.4
		[HDR]_Fre_Color("Fre_Color", Color) = (0.571125,0.9734455,1,1)
		[HDR]_MatColor("MatColor", Color) = (1.147078,1.77524,5.216475,1)
		_SlimeTiling("SlimeTiling", Vector) = (2,2,2,0)
		_VertexTiling("VertexTiling", Vector) = (1,1,2,0)
		_NoiseSpeed("NoiseSpeed", Vector) = (0,0.3,-0.2,0)
		_VertexNoiseSpeed("VertexNoiseSpeed", Vector) = (0,0.3,-0.2,0)
		_SlimeFlowMap("SlimeFlowMap", 2D) = "white" {}
		_AnimIntensity("AnimIntensity", Float) = 0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _SlimeFlowMap;
		uniform float3 _VertexTiling;
		uniform float3 _VertexNoiseSpeed;
		uniform float _AnimIntensity;
		uniform sampler2D _BaseMap;
		uniform float4 _BaseMap_ST;
		uniform sampler2D _MatCap;
		uniform float _Contrast;
		uniform sampler2D _SlimeNormal;
		uniform float3 _SlimeTiling;
		uniform float3 _NoiseSpeed;
		uniform float4 _MatColor;
		uniform float _RimBias;
		uniform float _RimScale;
		uniform float _RimPower;
		uniform float4 _Fre_Color;


		inline float4 TriplanarSampling104( sampler2D topTexMap, float3 worldPos, float3 worldNormal, float falloff, float2 tiling, float3 normalScale, float3 index )
		{
			float3 projNormal = ( pow( abs( worldNormal ), falloff ) );
			projNormal /= ( projNormal.x + projNormal.y + projNormal.z ) + 0.00001;
			float3 nsign = sign( worldNormal );
			half4 xNorm; half4 yNorm; half4 zNorm;
			xNorm = tex2Dlod( topTexMap, float4(tiling * worldPos.zy * float2(  nsign.x, 1.0 ), 0, 0) );
			yNorm = tex2Dlod( topTexMap, float4(tiling * worldPos.xz * float2(  nsign.y, 1.0 ), 0, 0) );
			zNorm = tex2Dlod( topTexMap, float4(tiling * worldPos.xy * float2( -nsign.z, 1.0 ), 0, 0) );
			return xNorm * projNormal.x + yNorm * projNormal.y + zNorm * projNormal.z;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 objToWorld106 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float4 triplanar104 = TriplanarSampling104( _SlimeFlowMap, ( ( ( ase_worldPos - objToWorld106 ) * _VertexTiling ) + ( _Time.y * _VertexNoiseSpeed ) ), ase_worldNormal, 5.0, float2( 1,1 ), 1.0, 0 );
			float4 VertexNoise114 = triplanar104;
			float4 VertexOffset121 = ( ( VertexNoise114 * float4( ase_worldNormal , 0.0 ) ) * _AnimIntensity );
			v.vertex.xyz += VertexOffset121.xyz;
			v.vertex.w = 1;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_BaseMap = i.uv_texcoord * _BaseMap_ST.xy + _BaseMap_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 saferPower66 = max( abs( ase_worldNormal ) , 0.0001 );
			float3 temp_cast_0 = (_Contrast).xxx;
			float3 temp_output_66_0 = pow( saferPower66 , temp_cast_0 );
			float3 break69 = temp_output_66_0;
			float3 break82 = ( temp_output_66_0 / ( break69.x + break69.y + break69.z ) );
			float3 ase_worldPos = i.worldPos;
			float3 objToWorld67 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
			float3 temp_output_101_0 = ( ( ( ase_worldPos - objToWorld67 ) * _SlimeTiling ) + ( _Time.y * _NoiseSpeed ) );
			float3 normalizeResult89 = normalize( ( ( break82.z * UnpackNormal( tex2D( _SlimeNormal, (temp_output_101_0).xy ) ) ) + ( break82.x * UnpackNormal( tex2D( _SlimeNormal, (temp_output_101_0).yz ) ) ) + ( break82.y * UnpackNormal( tex2D( _SlimeNormal, (temp_output_101_0).xz ) ) ) ) );
			float3 break90 = normalizeResult89;
			float3 appendResult51 = (float3(( ase_worldNormal.x + break90.x ) , ( ase_worldNormal.y + break90.y ) , ase_worldNormal.z));
			float3 normalizeResult55 = normalize( appendResult51 );
			float3 TriPlanar58 = normalizeResult55;
			float3 desaturateInitialColor39 = tex2D( _MatCap, ((mul( UNITY_MATRIX_V, float4( TriPlanar58 , 0.0 ) ).xyz).xyzz*0.5 + 0.5).xy ).rgb;
			float desaturateDot39 = dot( desaturateInitialColor39, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar39 = lerp( desaturateInitialColor39, desaturateDot39.xxx, 1.0 );
			float4 MatCapColor21 = ( float4( desaturateVar39 , 0.0 ) * _MatColor );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV9 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode9 = ( _RimBias + _RimScale * pow( max( 1.0 - fresnelNdotV9 , 0.0001 ), _RimPower ) );
			float4 RimColor23 = ( fresnelNode9 * _Fre_Color );
			o.Emission = ( ( tex2D( _BaseMap, uv_BaseMap ) * MatCapColor21 ) + RimColor23 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;16.8;1536;826.2;2688.373;-733.2119;1.332064;True;False
Node;AmplifyShaderEditor.CommentaryNode;91;-6596.264,-1050.818;Inherit;False;3483.023;1820.459;TriPlanar;36;97;95;94;53;58;55;51;52;49;90;89;87;86;84;85;82;83;80;81;78;75;88;71;74;72;70;68;69;66;67;65;64;63;62;100;101;TriPlanar;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;62;-5950.1,-1000.818;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.AbsOpNode;63;-5740.243,-935.4919;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-5697.518,-818.0587;Inherit;False;Property;_Contrast;Contrast;4;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;65;-6518.264,-493.8594;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TransformPositionNode;67;-6546.264,-331.8593;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;97;-6269.829,398.5497;Inherit;False;Property;_NoiseSpeed;NoiseSpeed;11;0;Create;True;0;0;0;False;0;False;0,0.3,-0.2;0,0.3,-0.2;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;88;-6401.319,-180.8044;Inherit;False;Property;_SlimeTiling;SlimeTiling;9;0;Create;True;0;0;0;False;0;False;2,2,2;2,2,2;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;94;-6253.432,312.814;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;66;-5517.507,-891.0756;Inherit;False;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;68;-6278.263,-450.8593;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-6139.302,-310.7726;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;69;-5295.019,-900.4964;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-6049.283,344.6253;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;101;-5991.893,-336.6027;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;72;-5173.917,-929.9141;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;70;-5928.799,-196.6912;Inherit;False;FLOAT2;1;2;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;75;-5796.469,-696.5675;Inherit;True;Property;_SlimeNormal;SlimeNormal;2;0;Create;True;0;0;0;False;0;False;06b148d566eb1d0408608f35caa10bf4;06b148d566eb1d0408608f35caa10bf4;False;bump;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.SwizzleNode;74;-5916.481,-456.3367;Inherit;False;FLOAT2;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;71;-5929.573,-46.57551;Inherit;False;FLOAT2;0;2;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;78;-5058.251,-767.4355;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BreakToComponentsNode;82;-4863.98,-757.6791;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SamplerNode;80;-5345.877,-656.9278;Inherit;True;Property;_TextureSample2;Texture Sample 2;11;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;83;-5410.793,-80.35436;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;81;-5392.983,-386.8299;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-4629.239,-690.7704;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-4631.634,-407.1555;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;-4669.639,-871.8701;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-4345.433,-743.3703;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;89;-4199.705,-668.2872;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;49;-4219.978,-920.2232;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;90;-4030.879,-690.6414;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-3685.694,-878.0793;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-3854.972,-812.8182;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;51;-3580.662,-757.0313;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;55;-3414.639,-753.1037;Inherit;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;58;-3437.726,-603.3272;Inherit;False;TriPlanar;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;115;-2381.447,1114.085;Inherit;False;2071.548;1314.823;VertexNoise;18;103;105;106;108;109;110;111;113;107;112;104;114;118;117;116;119;120;121;VertexNoise;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;19;-2429.606,-506.7163;Inherit;False;1719.6;669.8305;MatCapColor;10;21;1;6;5;3;4;39;42;44;59;MatCapColor;1,1,1,1;0;0
Node;AmplifyShaderEditor.TransformPositionNode;106;-2331.447,1690.78;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;59;-2369.154,-379.0311;Inherit;False;58;TriPlanar;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ViewMatrixNode;4;-2312.347,-456.7163;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.WorldPosInputsNode;105;-2303.447,1528.78;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;112;-1998.254,2111.68;Inherit;False;Property;_VertexNoiseSpeed;VertexNoiseSpeed;12;0;Create;True;0;0;0;False;0;False;0,0.3,-0.2;0,0.3,-0.2;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;108;-2063.446,1571.78;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;111;-1972.539,1946.737;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;107;-2186.502,1841.835;Inherit;False;Property;_VertexTiling;VertexTiling;10;0;Create;True;0;0;0;False;0;False;1,1,2;1,1,2;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-2153.887,-448.7363;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;5;-2025.067,-453.2964;Inherit;False;FLOAT4;0;1;2;3;1;0;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;-1924.485,1711.867;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-1752.082,2004.175;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;110;-1777.076,1686.037;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TexturePropertyNode;103;-1916.994,1164.085;Inherit;True;Property;_SlimeFlowMap;SlimeFlowMap;13;0;Create;True;0;0;0;False;0;False;7e7ccc89707747a439b50eb11dca0cd8;7e7ccc89707747a439b50eb11dca0cd8;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ScaleAndOffsetNode;6;-1841.822,-403.7165;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.5;False;2;FLOAT;0.5;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;22;-2126.317,306.9583;Inherit;False;1113.099;585.123;RimColor;9;11;10;13;14;12;18;9;16;23;RimColor;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1339.119,-362.3619;Inherit;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TriplanarNode;104;-1568.077,1446.237;Inherit;True;Spherical;World;False;Top Texture 0;_TopTexture0;white;-1;None;Mid Texture 0;_MidTexture0;white;-1;None;Bot Texture 0;_BotTexture0;white;-1;None;Triplanar Sampler;Tangent;10;0;SAMPLER2D;;False;5;FLOAT;1;False;1;SAMPLER2D;;False;6;FLOAT;0;False;2;SAMPLER2D;;False;7;FLOAT;0;False;9;FLOAT3;0,0,0;False;8;FLOAT;1;False;3;FLOAT2;1,1;False;4;FLOAT;5;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1654.31,-432.1284;Inherit;True;Property;_MatCap;MatCap;0;0;Create;True;0;0;0;False;0;False;-1;f55acbad4900b6847ba03961a858cfef;f55acbad4900b6847ba03961a858cfef;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;114;-1123.033,1481.794;Inherit;False;VertexNoise;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;11;-2058.804,488.6813;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;12;-1845.804,605.6812;Inherit;False;Property;_RimBias;RimBias  ;3;0;Create;True;0;0;0;False;0;False;0.17;0.17;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;10;-2076.317,356.9583;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;14;-1843.804,776.6812;Inherit;False;Property;_RimPower;RimPower;6;0;Create;True;0;0;0;False;0;False;1.4;1.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;39;-1148.119,-447.3619;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;42;-1421.119,-198.3619;Inherit;False;Property;_MatColor;MatColor;8;1;[HDR];Create;True;0;0;0;False;0;False;1.147078,1.77524,5.216475,1;1.070341,1.692397,5.065006,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-1847.804,692.6812;Inherit;False;Property;_RimScale;RimScale;5;0;Create;True;0;0;0;False;0;False;0.71;0.71;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;116;-1430.122,2119.03;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;18;-1512.617,627.4486;Inherit;False;Property;_Fre_Color;Fre_Color;7;1;[HDR];Create;True;0;0;0;False;0;False;0.571125,0.9734455,1,1;0.571125,0.9734455,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;118;-1375.975,1999.608;Inherit;False;114;VertexNoise;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1076.119,-218.3619;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;9;-1533.482,461.0608;Inherit;False;Standard;WorldNormal;ViewDir;False;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;117;-1165.453,2031.07;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;120;-1138.152,2299.476;Inherit;False;Property;_AnimIntensity;AnimIntensity;14;0;Create;True;0;0;0;False;0;False;0.01;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-876.316,-464.6681;Inherit;False;MatCapColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1175.617,461.4487;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;7;-609.4575,-323.435;Inherit;True;Property;_BaseMap;BaseMap;1;0;Create;True;0;0;0;False;0;False;-1;05d437b15dfd1bb41b3c0580c1782622;05d437b15dfd1bb41b3c0580c1782622;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;23;-1211.652,597.8694;Inherit;False;RimColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;25;-383.5861,-114.4501;Inherit;False;21;MatCapColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;119;-930.1528,2075.476;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;24;-40.9768,-25.191;Inherit;False;23;RimColor;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;121;-774.8564,2084.71;Inherit;False;VertexOffset;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;85.72266,-237.6634;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;209.9249,-41.09631;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;122;222.74,127.6693;Inherit;False;121;VertexOffset;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;671.9333,-112.4462;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;slime;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;63;0;62;0
WireConnection;66;0;63;0
WireConnection;66;1;64;0
WireConnection;68;0;65;0
WireConnection;68;1;67;0
WireConnection;100;0;68;0
WireConnection;100;1;88;0
WireConnection;69;0;66;0
WireConnection;95;0;94;0
WireConnection;95;1;97;0
WireConnection;101;0;100;0
WireConnection;101;1;95;0
WireConnection;72;0;69;0
WireConnection;72;1;69;1
WireConnection;72;2;69;2
WireConnection;70;0;101;0
WireConnection;74;0;101;0
WireConnection;71;0;101;0
WireConnection;78;0;66;0
WireConnection;78;1;72;0
WireConnection;82;0;78;0
WireConnection;80;0;75;0
WireConnection;80;1;74;0
WireConnection;83;0;75;0
WireConnection;83;1;71;0
WireConnection;81;0;75;0
WireConnection;81;1;70;0
WireConnection;86;0;82;0
WireConnection;86;1;81;0
WireConnection;84;0;82;1
WireConnection;84;1;83;0
WireConnection;85;0;82;2
WireConnection;85;1;80;0
WireConnection;87;0;85;0
WireConnection;87;1;86;0
WireConnection;87;2;84;0
WireConnection;89;0;87;0
WireConnection;90;0;89;0
WireConnection;52;0;49;1
WireConnection;52;1;90;0
WireConnection;53;0;49;2
WireConnection;53;1;90;1
WireConnection;51;0;52;0
WireConnection;51;1;53;0
WireConnection;51;2;49;3
WireConnection;55;0;51;0
WireConnection;58;0;55;0
WireConnection;108;0;105;0
WireConnection;108;1;106;0
WireConnection;3;0;4;0
WireConnection;3;1;59;0
WireConnection;5;0;3;0
WireConnection;109;0;108;0
WireConnection;109;1;107;0
WireConnection;113;0;111;0
WireConnection;113;1;112;0
WireConnection;110;0;109;0
WireConnection;110;1;113;0
WireConnection;6;0;5;0
WireConnection;104;0;103;0
WireConnection;104;9;110;0
WireConnection;1;1;6;0
WireConnection;114;0;104;0
WireConnection;39;0;1;0
WireConnection;39;1;44;0
WireConnection;40;0;39;0
WireConnection;40;1;42;0
WireConnection;9;0;10;0
WireConnection;9;4;11;0
WireConnection;9;1;12;0
WireConnection;9;2;13;0
WireConnection;9;3;14;0
WireConnection;117;0;118;0
WireConnection;117;1;116;0
WireConnection;21;0;40;0
WireConnection;16;0;9;0
WireConnection;16;1;18;0
WireConnection;23;0;16;0
WireConnection;119;0;117;0
WireConnection;119;1;120;0
WireConnection;121;0;119;0
WireConnection;8;0;7;0
WireConnection;8;1;25;0
WireConnection;15;0;8;0
WireConnection;15;1;24;0
WireConnection;0;2;15;0
WireConnection;0;11;122;0
ASEEND*/
//CHKSM=5996C836CF7988DFA0763BE003618071D598E098