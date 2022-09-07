
/*

void VERTEX_SHADER(

	float3 PositionIn, 
	float3 NormalIn, 
	float4 UVIn, 
	float3 TangentIn, 

	out float3 PositionOut, 
	out float3 NormalOut, 
	out float3 TangentOut, 
	out float TessellationFactorOut, 
	out float3 TessellationDisplacementOut

)

void FRAGMENT_SHADER(

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
	
)

*/

void VERTEX_SHADER_1(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 PositionOut, out float3 NormalOut, out float3 TangentOut, out float TessellationFactorOut, out float3 TessellationDisplacementOut){

	PositionOut = PositionIn;
	NormalOut = NormalIn;
	TangentOut = TangentIn;
	TessellationFactorOut = 1;
	TessellationDisplacementOut = float3(0, 1, 0);

}



void FRAGMENT_SHADER_1(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut){

	BaseColorOut = BaseColor_1;
	NormalOut = NormalIn;
	BentNormalOut = float3(0, 1, 0);
	MetallicOut = 0.5f;
	EmissionOut = float3(0,0,0);
	SmoothnessOut = 0.5f;
	AmbientOcclusionOut = 1.0f;
	AlphaOut = 1.0f;

}