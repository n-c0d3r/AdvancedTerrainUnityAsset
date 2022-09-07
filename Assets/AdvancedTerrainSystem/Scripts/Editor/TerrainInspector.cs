using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace AdvancedTerrainSystem
{

    [CustomEditor(typeof(Terrain))]
    public class TerrainInspector : Editor
    {

        private string m_CreateDefaultLayerBtn_FileName = "NewLayer";

        private int m_CreateDefaultLayerBtn_Selected = 0;

        private string[] m_CreateDefaultLayerBtn_DisplayedOptions = new string[] {

            "LitLayer"

        };

        private ShaderBuilder m_ShaderBuilder = new ShaderBuilder();



        public string m_CreateDefaultLayerBtn_DisplayedOption
        {

            get
            {

                return m_CreateDefaultLayerBtn_DisplayedOptions[m_CreateDefaultLayerBtn_Selected];

            }

        }



        public override void OnInspectorGUI()
        {

            Terrain terrain = (Terrain)target;



            DrawDefaultInspector();



            //Create Default Layer Btn
            {

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Create Default Layer"))
                {

                    CreateDefaultLayer();

                }

                if (GUILayout.Button("Add Default Layer"))
                {

                    Layer layer = CreateDefaultLayer();

                    terrain.m_Layers.Add(layer);

                }

                m_CreateDefaultLayerBtn_FileName = EditorGUILayout.TextArea(m_CreateDefaultLayerBtn_FileName);

                m_CreateDefaultLayerBtn_Selected = EditorGUILayout.Popup("", m_CreateDefaultLayerBtn_Selected, m_CreateDefaultLayerBtn_DisplayedOptions);

                EditorGUILayout.EndHorizontal();

            }



            //Generate Quadtree and Chunks Btn
            {

                if (GUILayout.Button("Generate Quadtree and Chunks"))
                {

                    GenerateQuadtreeAndChunks();

                }

            }



            //Build Shader Btn
            {

                if (GUILayout.Button("Build Shader"))
                {

                    BuildShader();

                }

            }

        }

        public void OnSceneGUI()
        {

            Terrain terrain = (Terrain)target;

            UpdateMaterial();

        }

        public Layer CreateDefaultLayer()
        {

            Terrain terrain = (Terrain)target;

            Layer result = null;

            string newLayerPath = "Assets/" + terrain.Directory + "/Layers/" + m_CreateDefaultLayerBtn_FileName + ".asset";

            if (AssetDatabase.LoadAssetAtPath<Layer>(newLayerPath) != null)
            {

                Debug.LogWarning(m_CreateDefaultLayerBtn_FileName + " layer already created");

                result = AssetDatabase.LoadAssetAtPath<Layer>(newLayerPath);

                return result;

            }

            switch (m_CreateDefaultLayerBtn_DisplayedOption)
            {

                case "LitLayer":

                    result = AssetDatabase.LoadAssetAtPath<Layer>("Assets/AdvancedTerrainSystem/Terrains/Default/Layers/LitLayer.asset");

                    result = Instantiate(result);

                    AssetDatabase.CreateAsset(result, newLayerPath);

                    break;

                default:

                    break;

            }

            return result;

        }



        public void GenerateQuadtreeAndChunks()
        {

            Terrain terrain = (Terrain)target;



        }

        public void BuildShader()
        {

            Terrain terrain = (Terrain)target;

            m_ShaderBuilder.Build(terrain);

        }

        public void UpdateMaterial()
        {

            Terrain terrain = (Terrain)target;



        }

    }

}
