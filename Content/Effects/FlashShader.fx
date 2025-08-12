#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

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

float amount = 1.f;

float color_r = 0.f;
float color_g = 0.f;
float color_b = 0.f; 

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
	float3 mixed = float3(color_r,color_g,color_b);


	color.rgb = mix_color(color.rgb, mixed, amount);
	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};