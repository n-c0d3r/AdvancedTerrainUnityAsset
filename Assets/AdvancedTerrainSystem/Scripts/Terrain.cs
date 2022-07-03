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



        public List<Layer> m_Layers;

    }

}
