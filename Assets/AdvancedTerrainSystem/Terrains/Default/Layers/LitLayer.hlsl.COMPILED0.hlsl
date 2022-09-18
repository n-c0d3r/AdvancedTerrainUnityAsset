
void FRAGMENT_SHADER_0(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut, out float DisplacementOut, out float SmoothBlendOut){


	float2 uv = PositionIn.xz * SizeAndOffset_0.xy + SizeAndOffset_0.zw;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D(AlbedoMap_0, SamplerState_Linear_Repeat, uv);
	float3 fromNormalMap = SAMPLE_TEXTURE2D(NormalMap_0, SamplerState_Linear_Repeat, uv);
	float fromRouchMap = SAMPLE_TEXTURE2D(RouchMap_0, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * BaseColor_0;
	float3 tangentNormal = lerp(float3(0,1,0), fromNormalMap, NormalStrength_0);
	float3 rouch = lerp(1, fromRouchMap, RouchIntensity_0);
	float smoothBlend = SmoothBlend_0;




	BaseColorOut = baseColor;// float3(UVIn.xy, 0);
	NormalOut = tangentNormal;
	BentNormalOut = float3(0, 1, 0);
	MetallicOut = 0.5f;
	EmissionOut = float3(0,0,0);
	SmoothnessOut = 1.0f - rouch;
	AmbientOcclusionOut = 1.0f;
	AlphaOut = 1.0f;
	DisplacementOut = 0.0f;
	SmoothBlendOut = smoothBlend;

}