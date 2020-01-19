
	sampler s0;  
		
    texture lightMask;  
    sampler lightSampler : register(s1) = sampler_state {Texture = <lightMask>;};
	
      
   float4 PixelShaderLight(float2 coords: TEXCOORD0) : COLOR0  
    {  
		float4 color = tex2D(s0, coords.xy);  
        float4 lightColor = tex2D(lightSampler, coords);  
        lightColor = clamp(lightColor, 0, 1);
		return color * (lightColor * 1.3);  
    }  

	      
    technique Technique1  
    {  
        pass Pass1
        {  
            PixelShader = compile ps_2_0 PixelShaderLight();  
        }  
    }  

