using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class ShaderBuilder : UnityEngine.Object
    {

        public ShaderBuilder(Terrain terrain)
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



        public string Build()
        {

            string result = m_Terrain.ShaderTemplate.GetShaderFile();



            return result;

        }

    }

}
