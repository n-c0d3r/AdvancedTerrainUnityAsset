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
            //float nodeWidth = terrain.settings.width*(Mathf.Pow(2,terrain.QuadtreeLevelCount));
            //float nodeHeight = terrain.settings.height;
            //float nodeLength = terrain.settings.length*(Mathf.Pow(2, terrain.QuadtreeLevelCount));
            //Vector3 nodeSize = new Vector3(nodeWidth, terrain.settings.height, nodeLength);
            
            //Vector3 numSub(Vector3 nodeSize, uint levels)
            //{
            //    float numSub = 0;
            //    for(uint level = 1; level<=levels; level++)
            //    {
            //        numSub += 1 /(Mathf.Pow(2,level+1));
            //    }
            //    return new Vector3(nodeSize.x*numSub, 0,nodeSize.z*numSub);
            //}

            //Vector3 chunkPos = new Vector3(terrain.transform.position.x - numSub(nodeSize, terrain.QuadtreeLevelCount).x, terrain.settings.height, terrain.transform.position.z - numSub(nodeSize, terrain.QuadtreeLevelCount).z);         
            //float originChunkPos = chunkPos.z;
            //for (uint numChunksX = 0; numChunksX<(Mathf.Pow(2,terrain.QuadtreeLevelCount)); numChunksX++)
            //{
            //    chunkPos.z = originChunkPos;
            //    for(uint numChunksZ = 0; numChunksZ < Mathf.Pow(2, terrain.QuadtreeLevelCount); numChunksZ++)
            //    {
            //        var chunk = new GameObject();
            //        var chunkTerrain = chunk.gameObject.AddComponent<UnityEngine.Terrain>();
            //        chunk.gameObject.AddComponent<TerrainCollider>();
            //        chunk.gameObject.transform.position = chunkPos;
            //        chunkTerrain.terrainData = new TerrainData();
            //        chunkTerrain.terrainData.size = new Vector3(terrain.settings.width, terrain.settings.height, terrain.settings.length);
            //        chunkPos.z += terrain.settings.length;
            //    }
            //    chunkPos.x += terrain.settings.width;
            //}

            //for (uint level = 0; level < terrain.QuadtreeLevelCount; level++)
            //{
                
            //}
            QuadtreeNode node = new QuadtreeNode(terrain.gameObject, terrain.QuadtreeLevelCount, 0, terrain);
        }

        void Divide(GameObject node, uint numLevel, uint currentNodeLevel, Terrain terrain)
        {
            float nodeWidth = terrain.settings.width /* * (Mathf.Pow(2, terrain.QuadtreeLevelCount))*/ * Mathf.Pow(2, (numLevel - currentNodeLevel));
            float nodeHeight = terrain.settings.height;
            float nodeLength = terrain.settings.length /* * (Mathf.Pow(2, terrain.QuadtreeLevelCount))*/ * Mathf.Pow(2, (numLevel - currentNodeLevel));
            Vector3 nodeSize = new Vector3(nodeWidth, terrain.settings.height, nodeLength);
            
            Vector3 childNodePos = new Vector3(node.transform.position.x - nodeSize.x*1/2, terrain.settings.height, node.transform.position.z - nodeSize.z*1/2);
            float originChildNodePos = childNodePos.z;

            for (uint numChildNodesX = 0; numChildNodesX < 2; numChildNodesX++)
            {
                childNodePos.z = originChildNodePos;
                for (uint numChunksZ = 0; numChunksZ < 2; numChunksZ++)
                {
                    var chunk = new GameObject();
                    var chunkTerrain = chunk.gameObject.AddComponent<UnityEngine.Terrain>();
                    chunk.gameObject.AddComponent<TerrainCollider>();
                    chunk.gameObject.transform.position = childNodePos;
                    chunkTerrain.terrainData = new TerrainData();
                    chunkTerrain.terrainData.size = new Vector3(terrain.settings.width * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1))) /* level-1*/, terrain.settings.height /* level-1*/, terrain.settings.length * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1))) /* level-1*/);
                    childNodePos.z += terrain.settings.length * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1))) /* chinh lai */;
                }
                childNodePos.x += terrain.settings.width * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1)));
            }
        }


        public void BuildShader()
        {

            Terrain terrain = (Terrain)target;



        }

        public void UpdateMaterial()
        {

            Terrain terrain = (Terrain)target;



        }

    }

}
