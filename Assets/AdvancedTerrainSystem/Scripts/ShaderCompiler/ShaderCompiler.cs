using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class ShaderCompiler : UnityEngine.Object
    {

        public ShaderCompiler(Terrain terrain)
        {

            m_Terrain = terrain;

        }



        private Terrain m_Terrain;

        public Terrain Terrain
        {

            get
            {

                return m_Terrain;

            }

        }



        public string Compile()
        {

            string result = m_Terrain.ShaderTemplate.GetHLSL();



            return result;

        }

    }

}
