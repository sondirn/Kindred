#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler s0;

texture bayerMask;
sampler bayerSampler : register(s1) = sampler_state {Texture = <bayerMask>;};
float Intensity;
float3 inputColor;

float4 main(float2 uv: TEXCOORD0) : SV_TARGET
{
	float3 trueColor = inputColor / 255;
	float4 color = tex2D(s0, uv.xy);
	float4 bayerColor = tex2D(bayerSampler, uv).r / 32 - (1/128);
	float4 diff = float4(1,1,1,1) / float4 (trueColor,1);
	float4 finalColor = color / diff;
	finalColor += bayerColor;
	finalColor = finalColor * Intensity;
	finalColor = clamp(finalColor, 0, 1);
	return finalColor;
}

technique Technique1
{
	pass
	{
		PixelShader = compile PS_SHADERMODEL main();
	}
}

