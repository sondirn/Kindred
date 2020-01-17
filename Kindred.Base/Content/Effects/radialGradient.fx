float innerRadius;
float innerIntensity;
float inputIntensity;
float3 inputColor;
texture bayerMask;
sampler bayerSampler : register(s1) = sampler_state {Texture = <bayerMask>;};




float4 PixelShaderLight(float2 uv: TEXCOORD0) : SV_TARGET
{
        float intensity = 1 / inputIntensity;
    float radius = innerRadius * inputIntensity;
    float radius2 = radius - (1 / innerIntensity) * inputIntensity;
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
    finalColor = finalColor - color;
    float4 deltaColor = float4(finalColor, 1);
    deltaColor += tex2D(bayerSampler, uv).r / 32 - (1/128);
    
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