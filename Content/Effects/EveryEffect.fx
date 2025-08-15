#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

uniform float flash_amount = 1.f;

uniform float tint_r = 1.f;
uniform float tint_g = 1.f;
uniform float tint_b = 1.f;

uniform float flash_color_r = 0.f;
uniform float flash_color_g = 0.f;
uniform float flash_color_b = 0.f;

uniform float tint_amount = 0.f;

uniform float alpha = 1.f;

uniform float reveal_amount = 1.f;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};


float3 mix_color(float3 color1 : COLOR, float3 color2 : COLOR, float strength) : COLOR
{
	float mix_r = color1.r * (1.f - strength) + color2.r * strength;
	float mix_g = color1.g * (1.f - strength) + color2.g * strength;
	float mix_b = color1.b * (1.f - strength) + color2.b * strength;

	return float3(mix_r, mix_g, mix_b);
}



float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	float3 mixed = float3(flash_color_r, flash_color_g, flash_color_b);

	//First operation.
	float3 tinting = float3(tint_r, tint_g, tint_b);

	float avg = (color.r + color.g + color.b) / 3.f;

	color.rgb = mix_color(color.rgb, float3(avg, avg, avg) * tinting, tint_amount);

	//Second operation.
	color.rgb = mix_color(color.rgb, mixed, flash_amount);

	//Third operation.
	
	color.a *= alpha; //Don't set, multiply instead.

	//Fourth operation.
	if (input.TextureCoordinates.y > reveal_amount)
	{
		color.a *= 0.f;
	}

	
	return color;
}


technique FlashDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};