#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D g_texture;
sampler g_sampler : register (s1) = sampler_state {Texture = <g_texture>;};

struct pixel_in
{
    float2 tex_coord : TEXCOORD;
};

float3 bilinear(float2 texcoord, float tex_dimension)
{
    float3 result;

    // red channel
    float4 reds = g_texture.GatherRed(g_sampler, texcoord);
    float r1 = reds.x;
    float r2 = reds.y;
    float r3 = reds.z;
    float r4 = reds.w;

    float2 pixel = texcoord * tex_dimension + 0.5;
    float2 fract = frac(pixel);
      
    float top_row_red = lerp(r4, r3, fract.x);
    float bottom_row_red = lerp(r1, r2, fract.x);

    float final_red = lerp(top_row_red, bottom_row_red, fract.y);
    result.x = final_red;
            
    // green channel
    float4 greens = g_texture.GatherGreen(g_sampler, texcoord);
    float g1 = greens.x;
    float g2 = greens.y;
    float g3 = greens.z;
    float g4 = greens.w;

    float top_row_green = lerp(g4, g3, fract.x);
    float bottom_row_green = lerp(g1, g2, fract.x);

    float final_green = lerp(top_row_green, bottom_row_green, fract.y);
    result.y = final_green;
            
    // blue channel
    float4 blues = g_texture.GatherBlue(g_sampler, texcoord);
    float b1 = blues.x;
    float b2 = blues.y;
    float b3 = blues.z;
    float b4 = blues.w;

    float top_row_blue = lerp(b4, b3, fract.x);
    float bottom_row_blue = lerp(b1, b2, fract.x);

    float final_blue = lerp(top_row_blue, bottom_row_blue, fract.y);
    result.z = final_blue;

    return result;
}



float4 MainPS(float2 uv: TEXCOORD0) : SV_Target
{
    int width_texels;
    int height_texels;
    g_texture.GetDimensions(width_texels, height_texels);
    
    float4 result = float4(0.0f, 0.0f, 0.0f, 1.0f);

    result.xyz = bilinear(uv, width_texels);
    
    return result;
}

technique BasicColorDrawing
{
	pass P0
	{
		
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};