#ifndef LANDSCAPE_FRAGMENT_H
#define LANDSCAPE_FRAGMENT_H



//$(INCLUDES)



void TerrainVertex_float (

	float3 PositionIn,
	float3 NormalIn,
	float4 UVIn,
	float3 TangentIn,

	out float3 PositionOut,
	out float3 NormalOut,
	out float3 TangentOut,
	out float TessellationFactorOut,
	out float3 TessellationDisplacementOut

) {

	PositionOut = PositionIn;
	NormalOut = NormalIn;
	TangentOut = TangentIn;
	TessellationFactorOut = 1;
	TessellationDisplacementOut = float3(0, 0, 0);

	//$(VERTEX_COMPUTE)

}



#endif