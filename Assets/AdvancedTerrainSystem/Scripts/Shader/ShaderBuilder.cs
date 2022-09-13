using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;



namespace AdvancedTerrainSystem
{

    public partial class ShaderBuilder
    {

        public ShaderBuilder()
        {



        }



        private Terrain m_Terrain;

        public Terrain Terrain
        {

            get
            {

                return m_Terrain;

            }

        }



        private void WriteFile(string path, string content)
        {

            if (!System.IO.File.Exists(path))
            {

                string dirPath = System.IO.Path.GetDirectoryName(path);

                if (!System.IO.Directory.Exists(dirPath))
                {

                    System.IO.Directory.CreateDirectory(dirPath);

                }

                System.IO.File.CreateText(path).Close();

            }

            System.IO.File.WriteAllText(path, content);

        }



        public void BuildForLayer(Layer layer, int index)
        {

            if (System.IO.File.Exists(Application.dataPath + "/" + layer.HLSLFilePath))
            {

                string rawContent = layer.GetHLSL();

                string absCompiledHLSLPath = Application.dataPath + "/" + layer.CompiledHLSLPath(index);

                string content = rawContent;

                List<KeyValuePair<string, string>> placeholders = new List<KeyValuePair<string, string>> {

                    //new KeyValuePair<string, string>(
                    //    "$VERTEX_SHADER",
                    //    "void VERTEX_SHADER_" + index.ToString() + "(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 PositionOut, out float3 NormalOut, out float3 TangentOut, out float TessellationFactorOut, out float3 TessellationDisplacementOut)"),
                    new KeyValuePair<string, string>(
                        "$Main",//"$FRAGMENT_SHADER",
                        "void FRAGMENT_SHADER_" + index.ToString() + "(float3 PositionIn, float3 NormalIn, float4 UVIn, float3 TangentIn, out float3 BaseColorOut, out float3 NormalOut, out float3 BentNormalOut, out float MetallicOut, out float3 EmissionOut, out float SmoothnessOut, out float AmbientOcclusionOut, out float AlphaOut, out float DisplacementOut, out float SmoothBlendOut)")
                };

                for (int i = 0; i < layer.m_Properties.Count; i++)
                {

                    placeholders.Add(
                    
                        new KeyValuePair<string, string>("$" + layer.m_Properties[i].Name, layer.m_Properties[i].Name + "_" + index.ToString())
                        
                    );

                }

                content = PlaceholderReplace(content, placeholders.ToArray());

                WriteFile(absCompiledHLSLPath, content);

            }

        }



        public Shader Build(Terrain terrain)
        {

            m_Terrain = terrain;

            string shaderContent = m_Terrain.ShaderTemplate.GetShaderFile();

            string compiledShaderContent = shaderContent;



            string propsStr = "";

            char[] alphaMapChannelNames = new char[m_Terrain.m_Layers.Count];
            char currAlphaMapChannelName = 'z';

            for (int i = 0; i < m_Terrain.m_Layers.Count; i++)
            {

                Layer layer = m_Terrain.m_Layers[i];





                switch (currAlphaMapChannelName) {

                    case 'x':
                        currAlphaMapChannelName = 'y';
                        break;

                    case 'y':
                        currAlphaMapChannelName = 'z';
                        break;

                    case 'z':
                        currAlphaMapChannelName = 'w';
                        break;

                    case 'w':
                        currAlphaMapChannelName = 'x';
                        break;

                    default:
                        break;

                }

                alphaMapChannelNames[i] = currAlphaMapChannelName;






                for (int j = 0; j < layer.m_Properties.Count; j++)
                {

                    LayerProperty prop = layer.m_Properties[j];

                    string UShaderType = "";
                    string ValueSTR = "";

                    switch (prop.TypeName)
                    {

                        case "Color":
                            UShaderType = "Color";
                            ValueSTR = "(0,0,0,0)";
                            break;

                        case "Float":
                            UShaderType = "Float";
                            ValueSTR = "0";
                            break;

                        case "Texture2D":
                            UShaderType = "2D";
                            ValueSTR = '"' + '"' + " {}";
                            break;

                        case "Texture3D":
                            UShaderType = "3D";
                            ValueSTR = '"' + '"' + " {}";
                            break;

                        case "Vector2":
                            UShaderType = "Float2";
                            ValueSTR = "(0,0)";
                            break;

                        case "Vector3":
                            UShaderType = "Float3";
                            ValueSTR = "(0,0,0)";
                            break;

                        case "Vector4":
                            UShaderType = "Vector";
                            ValueSTR = "(0,0,0,0)";
                            break;

                        default:
                            break;

                    }

                    propsStr += prop.Name + "_" + i.ToString() + "(" + '"' + prop.Name + "_" + i.ToString() + '"' + "," + UShaderType + ") = " + ValueSTR + System.Environment.NewLine;

                }

            }

            {

                int alphaMapIndex = 0;

                for (int i = 0; i < m_Terrain.m_Layers.Count; i+=4)
                {

                    propsStr += "ALPHAMAP_" + alphaMapIndex.ToString() + "(" + '"' + "ALPHAMAP_" + alphaMapIndex.ToString() + '"' + "," + "2D" + ") = " + '"' + '"' + " {}" + System.Environment.NewLine;

                    alphaMapIndex++;

                }

            }



            string propDefsStr = "";

            for (int i = 0; i < m_Terrain.m_Layers.Count; i++)
            {

                Layer layer = m_Terrain.m_Layers[i];

                for (int j = 0; j < layer.m_Properties.Count; j++)
                {

                    LayerProperty prop = layer.m_Properties[j];

                    string UShaderType = "";

                    switch (prop.TypeName)
                    {

                        case "Color":
                            UShaderType = "float4";
                            break;

                        case "Float":
                            UShaderType = "float";
                            break;

                        case "Texture2D":
                            UShaderType = "Texture2D";
                            break;

                        case "Texture3D":
                            UShaderType = "Texture3D";
                            break;

                        case "Vector2":
                            UShaderType = "float2";
                            break;

                        case "Vector3":
                            UShaderType = "float3";
                            break;

                        case "Vector4":
                            UShaderType = "float4";
                            break;

                        default:
                            break;

                    }

                    propDefsStr += UShaderType + " " + prop.Name + "_" + i.ToString() + ";" + System.Environment.NewLine;

                }

            }

            {

                int alphaMapIndex = 0;

                for (int i = 0; i < m_Terrain.m_Layers.Count; i+=4)
                {

                    propDefsStr += "Texture2D ALPHAMAP_" + alphaMapIndex.ToString() + ";" + System.Environment.NewLine;

                    alphaMapIndex++;

                }

            }



                string includes = "";

            for (int i = 0; i < m_Terrain.m_Layers.Count; i++)
            {

                Layer layer = m_Terrain.m_Layers[i];

                BuildForLayer(layer, i);

                includes += "#include " + '"' + "Assets/" + layer.CompiledHLSLPath(i) +'"' + System.Environment.NewLine;

            }



            string header = "";

            header += includes + System.Environment.NewLine;



            string vcomputes = "";

            for (int i = 0; i < 0;)//m_Terrain.m_Layers.Count; i++)
            {

                vcomputes += @"	
	                float3 PositionOut_" + i.ToString() + @" = float3(0,0,0);
	                float3 NormalOut_" + i.ToString() + @" = float3(0,0,0);
	                float3 TangentOut_" + i.ToString() + @" = float3(0,0,0); 
	                float TessellationFactorOut_" + i.ToString() + @" = 0;
	                float3 TessellationDisplacementOut_" + i.ToString() + @" = float3(0,0,0);
                ";
                vcomputes += System.Environment.NewLine;
                vcomputes += "VERTEX_SHADER_" + i.ToString() + "(PositionIn,NormalIn,UVIn,TangentIn,PositionOut_" + i.ToString() + ",NormalOut_" + i.ToString() + ",TangentOut_" + i.ToString() + ",TessellationFactorOut_" + i.ToString() + ",TessellationDisplacementOut_" + i.ToString() + ");";
                vcomputes += System.Environment.NewLine;

            }



            string pcomputes = @"

                    DisplacementOut = 0;

                    float4 UV = UVIn;

";

            for (int i = 0; i < m_Terrain.m_Layers.Count; i++)
            {

                char alphaChannelName = alphaMapChannelNames[i];

                int alphaMapIndex = i / 4;




                pcomputes += @"	
                    float3 BaseColorOut_" + i.ToString() + @" = float3(0,0,0);
                    float3 NormalOut_" + i.ToString() + @" = float3(0,0,0);
                    float3 BentNormalOut_" + i.ToString() + @" = float3(0,0,0);
                    float MetallicOut_" + i.ToString() + @" = 0;
                    float3 EmissionOut_" + i.ToString() + @" = float3(0,0,0); 
                    float SmoothnessOut_" + i.ToString() + @" = 0; 
                    float AmbientOcclusionOut_" + i.ToString() + @" = 0; 
                    float AlphaOut_" + i.ToString() + @" = 0;
                    float DisplacementOut_" + i.ToString() + @" = 0;
                    float SmoothBlendOut_" + i.ToString() + @" = 0;
                ";
                pcomputes += System.Environment.NewLine;
                pcomputes += "FRAGMENT_SHADER_" + i.ToString() + "(PositionIn,NormalIn,UVIn,TangentIn,BaseColorOut_" + i.ToString() + ",NormalOut_" + i.ToString() + ",BentNormalOut_" + i.ToString() + ",MetallicOut_" + i.ToString() + ",EmissionOut_" + i.ToString() + ",SmoothnessOut_" + i.ToString() + ",AmbientOcclusionOut_" + i.ToString() + @",AlphaOut_" + i.ToString() + ",DisplacementOut_" + i.ToString() + ",SmoothBlendOut_" + i.ToString() + @");";
                pcomputes += System.Environment.NewLine;


                if (i > 0)
                    pcomputes += @"	

                        {

                            float Alpha = SAMPLE_TEXTURE2D(ALPHAMAP_" + alphaMapIndex + @", SamplerState_Linear_Repeat, UV)." + alphaChannelName + @";

                            float3 BaseColorSmoothBlend = lerp(BaseColorOut, BaseColorOut_" + i.ToString() + @", Alpha);
                            float3 NormalSmoothBlend = lerp(NormalOut, NormalOut_" + i.ToString() + @", Alpha);
                            float3 BentNormalSmoothBlend = lerp(BentNormalOut, BentNormalOut_" + i.ToString() + @", Alpha);
                            float MetallicSmoothBlend = lerp(MetallicOut, MetallicOut_" + i.ToString() + @", Alpha);
                            float3 EmissionSmoothBlend = lerp(EmissionOut, EmissionOut_" + i.ToString() + @", Alpha); 
                            float SmoothnessSmoothBlend = lerp(SmoothnessOut, SmoothnessOut_" + i.ToString() + @", Alpha); 
                            float AmbientOcclusionSmoothBlend = lerp(AmbientOcclusionOut, AmbientOcclusionOut_" + i.ToString() + @", Alpha); 
                            float AlphaSmoothBlend = lerp(AlphaOut, AlphaOut_" + i.ToString() + @", Alpha);

                            float3 BaseColorHardBlend = BaseColorOut;
                            float3 NormalHardBlend = NormalOut;
                            float3 BentNormalHardBlend = BentNormalOut;
                            float MetallicHardBlend = MetallicOut;
                            float3 EmissionHardBlend = EmissionOut; 
                            float SmoothnessHardBlend = SmoothnessOut; 
                            float AmbientOcclusionHardBlend = AmbientOcclusionOut; 
                            float AlphaHardBlend = AlphaOut;

                            if(DisplacementOut <= DisplacementOut_" + i.ToString() + @"){

                                BaseColorHardBlend = BaseColorOut_" + i.ToString() + @";
                                NormalHardBlend = NormalOut_" + i.ToString() + @";
                                BentNormalHardBlend = BentNormalOut_" + i.ToString() + @";
                                MetallicHardBlend = MetallicOut_" + i.ToString() + @";
                                EmissionHardBlend = EmissionOut_" + i.ToString() + @";
                                SmoothnessHardBlend = SmoothnessOut_" + i.ToString() + @";
                                AmbientOcclusionHardBlend = AmbientOcclusionOut_" + i.ToString() + @";
                                AlphaHardBlend = AlphaOut_" + i.ToString() + @";

                                DisplacementOut = DisplacementOut_" + i.ToString() + @";

                            }

                            BaseColorOut = lerp( BaseColorHardBlend, BaseColorSmoothBlend, SmoothBlendOut_" + i.ToString() + @");
                            NormalOut = lerp( NormalHardBlend, NormalSmoothBlend, SmoothBlendOut_" + i.ToString() + @");
                            BentNormalOut = lerp( BentNormalHardBlend, BentNormalSmoothBlend, SmoothBlendOut_" + i.ToString() + @");
                            MetallicOut = lerp( MetallicHardBlend, MetallicSmoothBlend, SmoothBlendOut_" + i.ToString() + @");
                            EmissionOut = lerp( EmissionHardBlend, EmissionSmoothBlend, SmoothBlendOut_" + i.ToString() + @"); 
                            SmoothnessOut = lerp( SmoothnessHardBlend, SmoothnessSmoothBlend, SmoothBlendOut_" + i.ToString() + @"); 
                            AmbientOcclusionOut = lerp( AmbientOcclusionHardBlend, AmbientOcclusionSmoothBlend, SmoothBlendOut_" + i.ToString() + @"); 
                            AlphaOut = lerp( AlphaHardBlend, AlphaSmoothBlend, SmoothBlendOut_" + i.ToString() + @");

                        }
                    ";
                else
                    pcomputes += @"	

                        {

                            BaseColorOut = BaseColorOut_" + i.ToString() + @";
                            NormalOut = NormalOut_" + i.ToString() + @";
                            BentNormalOut = BentNormalOut_" + i.ToString() + @";
                            MetallicOut = MetallicOut_" + i.ToString() + @";
                            EmissionOut = EmissionOut_" + i.ToString() + @";
                            SmoothnessOut = SmoothnessOut_" + i.ToString() + @";
                            AmbientOcclusionOut = AmbientOcclusionOut_" + i.ToString() + @";
                            AlphaOut = AlphaOut_" + i.ToString() + @";

                            DisplacementOut = DisplacementOut_" + i.ToString() + @";

                        }
                    ";

            }

            pcomputes += @"

//BaseColorOut = SAMPLE_TEXTURE2D(ALPHAMAP_0, SamplerState_Linear_Repeat, UV).xyz;
//BaseColorOut = UVIn;

";



            compiledShaderContent = PlaceholderReplace(compiledShaderContent, new KeyValuePair<string, string>[] {
            
                new KeyValuePair<string, string>("__ATS_PROPERTIES(" + '"' + "$(PROPERTIES)"+'"'+", Float) = 0", propsStr + System.Environment.NewLine),
                new KeyValuePair<string, string>("// Graph Functions", header),
                new KeyValuePair<string, string>("Shader " + '"' + "HDRPTerrain" + '"', "Shader " + '"' + "HDRPTerrain_" + System.Guid.NewGuid().ToString("N") + '"'),
                new KeyValuePair<string, string>("float __ATS_PROPERTIES;", propDefsStr),
                new KeyValuePair<string, string>("//$(VERTEX_COMPUTE)", vcomputes),
                new KeyValuePair<string, string>("//$(FRAGMENT_COMPUTE)", pcomputes)
            
            });



            string compiledShaderPath = "Assets/" + terrain.Directory + "/Shaders/Compiled.shader";
            string absCompiledShaderPath = Application.dataPath + "/../" + compiledShaderPath;

            WriteFile(absCompiledShaderPath, compiledShaderContent);

            Shader shader = (Shader)AssetDatabase.LoadAssetAtPath(compiledShaderPath, typeof(Shader));

            return shader;
        }

    }

}
