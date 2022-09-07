using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{
    [Serializable]
    public class TerrainSettings
    {
        public uint width;
        public uint length;
        public uint height;
    }

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

        private uint ChunkCountX
        {

            get {

                return (uint)Mathf.Pow(2, QuadtreeLevelCount - 1);
            
            }

        }

        private uint ChunkCount
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

        public List<Layer> m_Layers;



        private void Awake()
        {



        }

        private void Start()
        {

            

        }

        private void Update()
        {

            UpdateChunksVisibility(Camera.allCameras);

        }



        public void UpdateChunksVisibility(Camera[] cameras)
        {



        }

    }

}
