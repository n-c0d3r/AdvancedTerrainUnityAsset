
$Main{


	float2 uv = PositionIn.xz * $SizeAndOffset.xy + $SizeAndOffset.zw;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D($AlbedoMap, SamplerState_Linear_Repeat, uv);
	float3 fromNormalMap = SAMPLE_TEXTURE2D($NormalMap, SamplerState_Linear_Repeat, uv);
	float fromRouchMap = SAMPLE_TEXTURE2D($RouchMap, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * $BaseColor;
	float3 tangentNormal = lerp(float3(0,1,0), fromNormalMap, $NormalStrength);
	float3 rouch = lerp(1, fromRouchMap, $RouchIntensity);
	float smoothBlend = $SmoothBlend;




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