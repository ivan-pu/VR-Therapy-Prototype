Shader "SodaCraft/SunFog"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        // _Density("Density",Float)=0.025
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
        }
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        HLSLINCLUDE
        float _Density;
        half4 _SunFogColor;
        float _SunPower;
        float _FogStartDistance;
        half4 _SkyFogColor;
        half4 _EquatorFogColor;
        float _EquatorPower;
        float _MaxFogHeight;
        float _MinFogHeight;
        float _Power;
        float _HeightDensityPower;
        float _Thickness;

        sampler2D _DitherNoise;
        float4 _DitherNoise_TexelSize;


        float GetDensity(float height)
        {
            float den = (1 - (height - _MinFogHeight) / (_MaxFogHeight - _MinFogHeight));
            den = clamp(den, 0, 1);
            den = pow(den, _HeightDensityPower);
            return den;
        }
        ENDHLSL

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile SUNFOG_BLEND_NORMAL SUNFOG_BLEND_ADDITIVE
            #include "Packages/com.unity.render-pipelines.universal/shaderLibrary/core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            struct Attributes
            {
                // positionOS 变量包含对象空间中的顶点
                // 位置。
                float4 positionOS : POSITION;
            };

            struct V2F
            {
                // 此结构中的位置必须具有 SV_POSITION 语义。
                float4 positionHCS : SV_POSITION;
            };

            V2F vert(Attributes IN)
            {
                V2F o;
                o.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 frag(V2F i) : SV_Target
            {
                float2 UV = i.positionHCS.xy / _ScaledScreenParams.xy;
                float4 col = tex2D(_MainTex, UV);
                //float2 UV = i.uv.xy / _ScaledScreenParams.xy;
                // 从摄像机深度纹理中采样深度。
                #if UNITY_REVERSED_Z
                real depth = SampleSceneDepth(UV);
                #else
                //  调整 Z 以匹配 OpenGL 的 NDC ([-1, 1])
                real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(UV));
                #endif

                //获取Noise
                int index = _Time * 1000;
                float2 noiseUV = (UV / _MainTex_TexelSize.xy + index) / (1 / _DitherNoise_TexelSize);
                float noise = tex2D(_DitherNoise, noiseUV).r;
                noise = (noise - 0.25) * 0.008;


                // 重建世界空间位置。
                float3 worldPos = ComputeWorldSpacePosition(UV, depth, UNITY_MATRIX_I_VP);


                float3 camPos = GetCameraPositionWS();
                float3 relativePosWS = worldPos - camPos;
                float3 dir = normalize(relativePosWS);
                float distance = length(relativePosWS);
                float3 lightDir = _MainLightPosition;

                //for now,we only care the vertical direction
                float rayLowPosY = min(worldPos.y, camPos.y);
                float rayHighPosY = max(worldPos.y, camPos.y);

                float underPartLow = min(rayLowPosY, _MinFogHeight);
                float underPartHigh = min(rayHighPosY, _MinFogHeight);
                float underPart = underPartHigh - underPartLow;
                underPart = abs(underPart / dot(dir, float3(0, 1, 0)));
                //underPart = abs(underPart / relativePosWS.y) * distance;
                float fogStrength = underPart * 1;

                float middlePartLow = clamp(rayLowPosY, _MinFogHeight, _MaxFogHeight);
                float middlePartHigh = clamp(rayHighPosY, _MinFogHeight, _MaxFogHeight);
                float middlePart = middlePartHigh - middlePartLow;
                middlePart = abs(middlePart / dot(dir, float3(0, 1, 0)));
                //limit far distance
                float clmapMiddlePart = min(_Thickness, middlePart);
                middlePartHigh = middlePartLow + (middlePartHigh - middlePartLow) * (clmapMiddlePart / middlePart);
                //middlePart = abs(middlePart / relativePosWS.y) * distance;
                fogStrength = fogStrength + clmapMiddlePart * 0.5 * (GetDensity(middlePartLow) + GetDensity(
                    middlePartHigh));
                //float totalDistance = underPart + middlePart;
                float topDotValue = dot(dir, float3(0, 1, 0));
                if (abs(topDotValue) < 0.0001)
                {
                    //return float4(1,0,0,1);
                    if (rayLowPosY > _MinFogHeight)
                    {
                        fogStrength = GetDensity(rayLowPosY) * min(_Thickness, distance);
                    }
                    else
                    {
                        fogStrength = GetDensity(rayLowPosY) * distance;
                    }
                }
                fogStrength = saturate(fogStrength * _Density);
                fogStrength = lerp(0, fogStrength, clamp(distance / _FogStartDistance - 1, 0, 1));
                //fogStrength = pow(fogStrength, _Power);
                fogStrength = 1 - clamp(exp(-fogStrength * _Power), 0, 1);
                fogStrength += noise;
                //float sunScatter = clamp(dot(dir, lightDir) * 0.5 + 0.5, 0, 1);

                //sunScatter = pow(sunScatter, _SunPower * (saturate(topDotValue) * 0.2 + 0.8));

                float sunScatter = 1 / exp((1 - dot(dir, lightDir)) * _SunPower);

                sunScatter += noise;
                _SkyFogColor = lerp(_SkyFogColor, _EquatorFogColor,
                                    pow(1 - clamp(topDotValue, 0, 1), _EquatorPower) + noise);
                
                half4 fogColors=_SkyFogColor;
            #ifdef SUNFOG_BLEND_NORMAL
                 fogColors = _SkyFogColor + _SunFogColor * (1 + noise) * sunScatter;    
            #endif
            #ifdef SUNFOG_BLEND_ADDITIVE
                 fogColors=lerp(_SkyFogColor,_SunFogColor,(1 + noise) * sunScatter);  
            #endif


                float4 finalCol = lerp(col, fogColors, fogStrength);
                //return clamp(abs(underPart+middlePart-distance)*1000,0,1);
                return finalCol;
            }
            ENDHLSL
        }
    }
}