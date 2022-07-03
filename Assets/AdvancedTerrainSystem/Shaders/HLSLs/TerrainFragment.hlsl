#ifndef LANDSCAPE_VERTEX_H
#define LANDSCAPE_VERTEX_H



//$(INCLUDES)



void TerrainFragment_float(

	float3 PositionIn,
	float3 NormalIn,
	float4 UVIn,
	float3 TangentIn,

	out float3 BaseColorOut,
	out float3 NormalOut,
	out float3 BentNormalOut,
	out float MetallicOut,
	out float3 EmissionOut,
	out float SmoothnessOut,
	out float AmbientOcclusionOut,
	out float AlphaOut

) {

	BaseColorOut = float3(1, 1, 1);
	NormalOut = float3(0, 1, 0);
	BentNormalOut = float3(0, 1, 0);
	MetallicOut = 0;
	EmissionOut = float3(0, 0, 0);
	SmoothnessOut = 0.5f;
	AmbientOcclusionOut = 1;
	AlphaOut = 1;

	//$(FRAGMENT_COMPUTE)

}



#endif