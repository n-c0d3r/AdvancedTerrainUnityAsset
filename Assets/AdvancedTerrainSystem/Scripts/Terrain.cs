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

        public Shader m_Shader;

        public List<Layer> m_Layers;

        [HideInInspector]
        public QuadtreeNode m_RootNode;



        private void Awake()
        {



        }

        private void Start()
        {

            StartCoroutine(UpdateChunksVisibilityCoroutine());

        }

        private void Update()
        {

            if(Application.isEditor)
                UpdateMaterialsForNode(m_RootNode);

        }

        public void UpdateMaterialsForNode(QuadtreeNode node)
        {


            if (node.currentNodeLevel == QuadtreeLevelCount - 1)
            {

                Material material = node.chunkTerrain.materialTemplate;

                for (uint i = 0; i < m_Layers.Count; ++i)
                {

                    Layer layer = m_Layers[(int)i];



                    foreach (LayerProperty prop in layer.m_Properties)
                    {

                        prop.Apply2Material(material, i);

                    }

                }

            }



            if (node.childs != null)
                foreach (QuadtreeNode childNode in node.childs)
                {

                    UpdateMaterialsForNode(childNode);

                }

        }



        IEnumerator UpdateChunksVisibilityCoroutine()
        {

            while (true)
            {

                UpdateChunksVisibility(Camera.allCameras);

                yield return new WaitForSeconds(1.0f);

            }

            yield return null;

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

                if(node.childs != null)
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
