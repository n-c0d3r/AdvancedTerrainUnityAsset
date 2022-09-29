
void FRAGMENT_SHADER_1(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut, out float DisplacementOut, out float SmoothBlendOut){


	float2 uv = PositionIn.xz * SizeAndOffset_1.xy + SizeAndOffset_1.zw;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D(AlbedoMap_1, SamplerState_Linear_Repeat, uv);
	float3 fromNormalMap = SAMPLE_TEXTURE2D(NormalMap_1, SamplerState_Linear_Repeat, uv);
	float fromRouchMap = SAMPLE_TEXTURE2D(RouchMap_1, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * BaseColor_1;
	float3 tangentNormal = lerp(float3(0,1,0), fromNormalMap, NormalStrength_1);
	float3 rouch = lerp(1, fromRouchMap, RouchIntensity_1);
	float smoothBlend = SmoothBlend_1;




	BaseColorOut = NormalIn;// float3(UVIn.xy, 0);
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