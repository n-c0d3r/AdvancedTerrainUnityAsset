using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    public class Terrain : MonoBehaviour
    {

        [SerializeField]
        private uint m_QuadtreeLevelCount = 4;

        private uint QuadtreeLevelCount
        {

            get;

            set;

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
