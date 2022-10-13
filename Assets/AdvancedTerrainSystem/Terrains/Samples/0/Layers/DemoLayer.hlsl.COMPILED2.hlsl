




void FRAGMENT_SHADER_2(float AlphaIn, float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut, out float DisplacementOut, out float SmoothBlendOut){


	float2 uv = PositionIn.xz * SizeAndOffset_2.xy + SizeAndOffset_2.zw;



	uv.x += _Time.x * SpeedX_2;
	uv.y += _Time.x * SpeedY_2;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D(AlbedoMap_2, SamplerState_Linear_Repeat, uv);

	float3 fromNormalMap = UnpackNormal(SAMPLE_TEXTURE2D(NormalMap_2, SamplerState_Linear_Repeat, uv));

	float fromRouchMap = SAMPLE_TEXTURE2D(RouchMap_2, SamplerState_Linear_Repeat, uv).x;

	float fromDisplacementMap = SAMPLE_TEXTURE2D(DisplacementMap_2, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * BaseColor_2;
	float3 tangentNormal = lerp(NormalIn, fromNormalMap, NormalStrength_2);
	float3 rouch = (1.0f - fromRouchMap) * (1.0f / (RouchIntensity_2 + 0.0000001f));
	float smoothBlend = SmoothBlend_2;
	float displacement = DisplacementOffset_2 + DisplacementAmplitude_2 * fromDisplacementMap * AlphaIn * 1.0f;




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