Shader "Unlit/My_YuanShen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor("Main Color", Color) = (1,1,1,1)
        _ShadowColor("Shadow Color", Color) = (1,1,1,1)
        _ShadowRange("Shadow Range", Range(0,1)) = 0.5
        _ShadowSmooth("Shadow Smooth", Range(0,1)) = 0.2
        _RampTex ("Ramp Tex", 2D) = "white"{}

        [Space(10)]
        _OutlineWidth ("Outline Width", Range(0,1)) = 0.1 
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        // 基础色
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            half3 _MainColor;
            half3 _ShadowColor;
            float _ShadowRange;
            float _ShadowSmooth;
            sampler2D _RampTex;

            v2f vert (a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture

                half4 mainTex = tex2D(_MainTex, i.uv);

                half3 worldNormal = i.worldNormal;
                half3 worldPos = i.worldPos;
                half3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                half3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));

                half halfLambert = dot(worldNormal, lightDir) * 0.5 + 0.5;
                
                float ramp = tex2D(_RampTex, float2(saturate(halfLambert - _ShadowRange), 0.5)).r;
                //float ramp = smoothstep(0, _ShadowSmooth, halfLambert - _ShadowRange);

                half3 diffuse = lerp(_ShadowColor, _MainColor, ramp);
                diffuse *= mainTex;
                fixed4 col = 1;
                col.rgb = diffuse * _LightColor0;
                UNITY_APPLY_FOG(i.fogCoord, baseColor);
                return col;
            }
            ENDCG
        }
        //描边
        Pass {
            NAME "OUTLINE"
            Tags {"LightMode" = "ForwardBase"}

            Cull Front 

            CGPROGRAM

            #pragma vertex vert 
            #pragma fragment frag 

            #include "UnityCG.cginc"

            struct a2v{
                float4 vertex  : POSITION;
                float3 normal  : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f{
                float4 pos : SV_POSITION;
            };

            float _OutlineWidth;
            float4 _OutlineColor;

            v2f vert(a2v v){
                v2f o;
                float4 pos = UnityObjectToClipPos(v.vertex);
                float3 normal = normalize(mul((float3x3)UNITY_MATRIX_MV, v.tangent.xyz));
                //将法线变换到NDC空间
                float3 ndcNormal = normalize(TransformViewToProjection(normal.xyz)) * pos.w;
                //将近裁剪面右上角位置的顶点变换到观察空间
                float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));
                float aspect = abs(nearUpperRight.y / nearUpperRight.x);//求得屏幕宽高比
                ndcNormal.x *= aspect;
                pos.xy += 0.1 * _OutlineWidth * ndcNormal.xy;
                o.pos = pos;
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                return _OutlineColor;
            }

            ENDCG
        }
    }
}
