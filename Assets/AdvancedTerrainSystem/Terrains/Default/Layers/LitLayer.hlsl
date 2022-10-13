
$Main{


	float2 uv = PositionIn.xz * $SizeAndOffset.xy + $SizeAndOffset.zw;



	float3 fromAlbedoMap = SAMPLE_TEXTURE2D($AlbedoMap, SamplerState_Linear_Repeat, uv);

	float3 fromNormalMap = UnpackNormal(SAMPLE_TEXTURE2D($NormalMap, SamplerState_Linear_Repeat, uv));

	float fromRouchMap = SAMPLE_TEXTURE2D($RouchMap, SamplerState_Linear_Repeat, uv).x;

	float fromDisplacementMap = SAMPLE_TEXTURE2D($DisplacementMap, SamplerState_Linear_Repeat, uv).x;

	float3 baseColor = fromAlbedoMap * $BaseColor;
	float3 tangentNormal = lerp(NormalIn, fromNormalMap, $NormalStrength);
	float3 rouch = (1.0f - fromRouchMap) * (1.0f / ($RouchIntensity + 0.0000001f));
	float smoothBlend = $SmoothBlend;
	float displacement = $DisplacementOffset + $DisplacementAmplitude * fromDisplacementMap * AlphaIn * 1.0f;




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