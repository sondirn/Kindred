float innerRadius;
float innerIntensity;
float inputIntensity;
float3 inputColor;

float4 PixelShaderLight(float2 uv: TEXCOORD0) : COLOR0
{
        float intensity = 1 / inputIntensity;
        float radius = innerRadius * inputIntensity;
        float radiuss2 = radius - (1/innerIntensity) * inputIntensity;
        float3 lightColor = inputColor / 255;
        float3 secondColor = lightColor / 1.4;
        float3 scale = .7 * intensity;
        float2 st = uv.xy / scale;
        float pct = 0.0;
        pct = distance(st, float2(.5 / sacle, .5 / scale));
        float3 color = float3(pct, pct, pct)

        float3 finalColor = lerp(lightColor, secondColor, smoothstep(radius2, radius, pct / .5));
        finalColor = finalColor / intensity;
        return float4(finalColor - color, 1.0)
}

technique Technique1
{
        pass Pass1
        {
                PixelShader = compile ps_2_0 PixelShaderLight();
        }
}