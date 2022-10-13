




void FRAGMENT_SHADER_1(float AlphaIn, float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut, out float DisplacementOut, out float SmoothBlendOut){


	float2 uv = PositionIn.xz * SizeAndOffset_1.xy + SizeAndOffset_1.zw;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D(AlbedoMap_1, SamplerState_Linear_Repeat, uv);

	float3 fromNormalMap = UnpackNormal(SAMPLE_TEXTURE2D(NormalMap_1, SamplerState_Linear_Repeat, uv));

	float fromRouchMap = SAMPLE_TEXTURE2D(RouchMap_1, SamplerState_Linear_Repeat, uv).x;

	float fromDisplacementMap = SAMPLE_TEXTURE2D(DisplacementMap_1, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * BaseColor_1;
	float3 tangentNormal = lerp(NormalIn, fromNormalMap, NormalStrength_1);
	float3 rouch = (1.0f - fromRouchMap) * (1.0f / (RouchIntensity_1 + 0.0000001f));
	float smoothBlend = SmoothBlend_1;
	float displacement = DisplacementOffset_1 + DisplacementAmplitude_1 * fromDisplacementMap * AlphaIn * 1.0f;




	BaseColorOut = baseColor;
	NormalOut = tangentNormal;
	BentNormalOut = float3(0, 1, 0);
	MetallicOut = 0.5f;
	EmissionOut = float3(0,0,0);
	SmoothnessOut = rouch;
	AmbientOcclusionOut = 1.0f;
	AlphaOut = 1.0f;
	DisplacementOut = displacement;
	SmoothBlendOut = smoothBlend;

}