float innerRadius;
float innerIntensity;
float inputIntensity;
float3 inputColor;
texture bayerMask;
sampler bayerSampler : register(s1) = sampler_state {Texture = <bayerMask>;};




float4 PixelShaderLight(float2 uv: TEXCOORD0) : SV_TARGET
{
    float tempIntensity = inputIntensity;
    float tempInnerIntensity = innerIntensity;
    tempInnerIntensity = clamp(tempInnerIntensity,0,4);
    tempIntensity = clamp(tempIntensity, 0, 1);
        float intensity = 1 / tempIntensity;
        float radius = innerRadius * tempIntensity;
        float radius2 = radius - (1 / tempInnerIntensity) * tempIntensity;
        float3 lightColor = inputColor / 255;
        float3 secondColor = lightColor / 1.4;
        float scale = .5 * intensity;
        float2 st = uv / scale;
        float pct = 0.0;
        pct = distance(st, float2(.5 / scale , .5 / scale));
        float3 color = float3(pct,pct,pct);
        lightColor = lerp(lightColor, secondColor, pct / 2);
        float3 finalColor = lerp(lightColor, secondColor, smoothstep(radius2, radius, pct / .5));
        finalColor = finalColor / intensity;
        finalColor = clamp(finalColor, 0, 1);
        finalColor = finalColor - color;
        float4 deltaColor = float4(finalColor, 1);
        deltaColor += tex2D(bayerSampler, uv).r / 32 - (1/128);
        deltaColor = clamp(deltaColor, 0, 1);
        //float4 bayerColor = tex2D(bayerSampler, uv).r / 1;
        //finalColor += bayerColor;
        return deltaColor;
}

technique Technique1
{
        pass
        {
                PixelShader = compile ps_2_0 PixelShaderLight();
        }
}