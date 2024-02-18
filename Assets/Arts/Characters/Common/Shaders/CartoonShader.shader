// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ShaderLearn/CartoonShader"
{
    Properties
    {
        _Color("Color",Color)=(0,0,0,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Ramp("Ramp Texture",2D)="white"{}
        _Outline ("Outline",Range(0,1)) = 0.1
        _OutlineColor("OutlineColor",Color)=(0,0,0,1)
        _Specular("Specular",Color)=(0,0,0,1)
        _SpecularScale("Specular Scale",Range(0,0.1))=0.01

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100


        //过程式集合渲染，使用渲染轮廓线，渲染背面面片,并在view空间下将模型顶点沿着法线方向外扩
        Pass
        {
            NAME "OUTLINE"
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal :NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

       
            float _Outline;
            fixed4 _OutlineColor;
            v2f vert (appdata v)
            {
                v2f o;
                /*直接使用顶点法线扩展:pos = pos + normal * _Outline,但是对于内凹的模型会产生背面面片遮挡正面的情况。
                    解决方法：先对顶点法线的z分量处理为定值，在归一化法线对其进行扩张，从而降低遮挡的可能性。
                */
                float4 pos = mul(UNITY_MATRIX_MV,v.vertex);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV,v.normal);
                normal.z = -0.5;
                pos = pos + float4(normalize(normal),0)*_Outline;
                o.pos = mul(UNITY_MATRIX_P,pos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(_OutlineColor.rgb,1);
            }
            ENDCG
        }

        //正常渲染正面，加入高光效果
        Pass{
            Tags{"LightMode"="ForwardBase"}
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal :NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal:TEXCOORD1;
                float3 worldPos:TEXTCORD2;
                SHADOW_COORDS(3)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Ramp;
            float4 _Ramp_ST;
            fixed4 _Specular;
            float _SpecularScale;
            fixed4 _Color;
            v2f vert (appdata v)
            {
                v2f o;
             
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
                o.worldNormal = mul(v.normal,(float3x3)unity_WorldToObject);
                o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;

                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
                fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
                fixed3 worldHalfDir = normalize(worldLightDir+worldViewDir);

                fixed4 c = tex2D(_MainTex,i.uv);
                fixed3 albedo = c.rgb*_Color.rgb;

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;

                UNITY_LIGHT_ATTENUATION(atten,i,i.worldPos);

                fixed diff =dot(worldNormal,worldLightDir);
                diff = (diff*0.5+0.5)+atten;

                fixed3 diffuse = _LightColor0.rgb*albedo*tex2D(_Ramp,float2(diff,diff)).rgb;

                /*计算normal和halfDir结果，然后与一个阈值比较，通过CG的STEP函数实现比较。这种方法会在高光区域边界造成锯齿
                原因：高光区域边缘不是平滑渐变。而是由0突变到1
                方法：在边界处一个小区域进行平滑处理
                */
                fixed3 spec = dot(worldNormal,worldHalfDir);
                fixed w = fwidth(spec)*2.0;
                fixed3 specular = _Specular.rgb*lerp(0,1,smoothstep(-w,w,spec+_SpecularScale-1))
                *step(0.0001,_SpecularScale);

                return fixed4(ambient+diffuse+specular,1.0);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
