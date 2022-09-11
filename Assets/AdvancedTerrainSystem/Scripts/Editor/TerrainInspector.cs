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



            if(false){

                GUILayout.Space(80);

                for (int i = 0; i < terrain.m_Layers.Count; i++)
                {

                    var layer = terrain.m_Layers[i];

                    EditorGUILayout.LabelField("Layer " + i.ToString());

                    var editor = Editor.CreateEditor(layer);

                    editor.OnInspectorGUI();

                    GUILayout.Space(40);

                }

                GUILayout.Space(80);

            }



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



            //Build All Btn
            {

                if (GUILayout.Button("Build All"))
                {

                    BuildAll();

                }

            }

        }

        public void OnSceneGUI()
        {

            Terrain terrain = (Terrain)target;



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



        public void BuildMaterialsForNode(QuadtreeNode node, Terrain terrain)
        {


            if (node.currentNodeLevel == terrain.QuadtreeLevelCount - 1)
            {

                Material material = new Material(terrain.m_Shader);

                node.chunkTerrain.materialTemplate = material;

            }


            if (!node.IsLeafNode())
                foreach (QuadtreeNode childNode in node.childs)
                {

                    BuildMaterialsForNode(childNode, terrain);

                }

        }

        public void GenerateQuadtreeAndChunks()
        {

            Terrain terrain = (Terrain)target;




            terrain.m_TerrainLayers = new TerrainLayer[terrain.m_Layers.Count];

            for (int i = 0; i < terrain.m_Layers.Count; i++)
            {

                terrain.m_TerrainLayers[i] = new UnityEngine.TerrainLayer();

            }



            QuadtreeNode oldRootNode = terrain.m_RootNode;



            GameObject rnGObj = new GameObject("IntermediateRootNode");

            rnGObj.transform.parent = terrain.transform;

            terrain.m_RootNode = new QuadtreeNode(rnGObj, terrain.QuadtreeLevelCount, 0, terrain);



            if(oldRootNode != null)
            {

                if(oldRootNode.gobj != null)
                {

                    terrain.m_RootNode.CopyFromBackup(oldRootNode);

                    if(oldRootNode.numLevel == terrain.m_RootNode.numLevel)
                        DestroyImmediate(oldRootNode.gobj);

                }

            }

            rnGObj.name = "RootNode";

        }



        public void BuildShader()
        {

            Terrain terrain = (Terrain)target;

            terrain.m_Shader = m_ShaderBuilder.Build(terrain);

            BuildMaterialsForNode(terrain.m_RootNode, terrain);

        }



        public void BuildAll()
        {

            GenerateQuadtreeAndChunks();

            BuildShader();

        }

    }

}
