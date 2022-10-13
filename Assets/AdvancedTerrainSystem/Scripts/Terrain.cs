using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [ExecuteInEditMode]
    public class Terrain : MonoBehaviour
    {
        
        public TerrainSettings settings;

        [SerializeField]
        private uint m_QuadtreeLevelCount = 4;

        public uint QuadtreeLevelCount
        {

            get
            {
                return m_QuadtreeLevelCount;
            }

            set
            {
                
            }

        }

        public uint ChunkCountX
        {

            get {

                return (uint)Mathf.Pow(2, QuadtreeLevelCount - 1);
            
            }

        }

        public uint ChunkCount
        {

            get
            {

                return ChunkCountX * ChunkCountX;

            }

        }



        [SerializeField]
        private string m_Directory;

        public string Directory
        {

            get
            {

                return m_Directory;

            }

        }

        [SerializeField]
        private ShaderTemplate m_ShaderTemplate;

        public ShaderTemplate ShaderTemplate
        {

            get
            {

                return m_ShaderTemplate;

            }

        }

        [HideInInspector]
        public Shader m_Shader;

        public List<Layer> m_Layers;

        [HideInInspector]
        public QuadtreeNode m_RootNode;
        [HideInInspector]
        public UnityEngine.TerrainLayer[] m_TerrainLayers = new UnityEngine.TerrainLayer[0];



        private void Awake()
        {



        }

        private void Start()
        {

            StartCoroutine(UpdateChunksVisibilityCoroutine());

        }

        private void Update()
        {

            if(!Application.isPlaying)
                UpdateMaterialsForNode(m_RootNode);

            if (!Application.isPlaying)
                UpdateChunksAlphaMaps();

        }

        public void UpdateMaterialsForNode(QuadtreeNode node)
        {


            if (node.currentNodeLevel == QuadtreeLevelCount - 1)
            {

                Material material = node.chunkTerrain.materialTemplate;

                try
                {

                    for (uint i = 0; i < m_Layers.Count; ++i)
                    {

                        Layer layer = m_Layers[(int)i];



                        foreach (LayerProperty prop in layer.m_Properties)
                        {

                            prop.Apply2Material(material, i);

                        }

                    }

                }
                catch
                {



                }

            }



            if (!node.IsLeafNode())
                foreach (QuadtreeNode childNode in node.childs)
                {

                    UpdateMaterialsForNode(childNode);

                }

        }



        IEnumerator UpdateChunksAlphaMapsCoroutine()
        {

            UpdateChunksAlphaMaps();

            yield return null;

        }



        IEnumerator UpdateChunksVisibilityCoroutine()
        {

            while (true)
            {

                if (Application.isPlaying)
                    UpdateChunksVisibility(Camera.allCameras);

                yield return new WaitForSeconds(1.0f);

            }

            yield return null;

        }



        public void UpdateQuadtreeNodeAlphaMaps(QuadtreeNode node)
        {

            if (!Application.isPlaying)
            {

                if (!node.IsLeafNode())
                {

                    foreach (QuadtreeNode childNode in node.childs)
                    {

                        UpdateQuadtreeNodeAlphaMaps(childNode);

                    }

                }
                else
                {
                    try
                    {

                        for (int i = 0; i < m_Layers.Count; i += 4)
                        {

                            int alphaMapIndex = i / 4;

                            node.chunkTerrain.materialTemplate.SetTexture("ALPHAMAP_" + alphaMapIndex.ToString(), node.chunkTerrain.terrainData.GetAlphamapTexture(alphaMapIndex));

                        }

                    }
                    catch
                    {



                    }

                }

            }
            else
            {



            }

        }

        public void UpdateChunksAlphaMaps()
        {

            UpdateQuadtreeNodeAlphaMaps(m_RootNode);

        }



        public void UpdateQuadtreeNodeVisibility(QuadtreeNode node, Camera[] cameras)
        {

            Vector3 pos = node.gobj.transform.position;

            float nodeRadius = Mathf.Sqrt(node.nodeWidth * node.nodeWidth + node.nodeLength * node.nodeLength);

            bool b = false;

            foreach (Camera camera in cameras)
            {

                Vector3 cpos = camera.transform.position;

                if (

                    ((pos - cpos).magnitude <= camera.farClipPlane + nodeRadius)

                )
                {

                    b = true;

                    break;

                }

            }

            if (b)
            {

                node.gobj.SetActive(true);

                if(!node.IsLeafNode())
                    foreach (QuadtreeNode childNode in node.childs)
                    {

                        UpdateQuadtreeNodeVisibility(childNode, cameras);

                    }

            }
            else
            {

                node.gobj.SetActive(false);

            }

        }

        public void UpdateChunksVisibility(Camera[] cameras)
        {

            UpdateQuadtreeNodeVisibility(m_RootNode, cameras);

        }

    }

}
