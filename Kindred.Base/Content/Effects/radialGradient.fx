

float4 PS(float2 vTex0 : TEXCOORD0 ) : COLOR0
{
		float distfromcenter=distance(float2(0.5f, 0.5f), vTex0);	
		float4 rColor = lerp(float4(0,0,0,1),float4(1,1,1,1), saturate(distfromcenter));	
		return rColor;
}
technique Technique1  
    {  
        pass Pass1
        {  
            PixelShader = compile ps_3_0 PS();  
        }  
    }