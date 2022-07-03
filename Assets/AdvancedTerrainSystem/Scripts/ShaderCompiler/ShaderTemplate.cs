using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Shader Template", menuName = "AdvancedTerrainSystem/ShaderTemplate", order = 1)]
    public class ShaderTemplate : ScriptableObject
    {     

        [SerializeField]
        private string m_HLSLFilePath = "";

        public string HLSLFilePath
        {

            get
            {

                return m_HLSLFilePath;

            }

        }

        public string GetHLSL()
        {

            return System.IO.File.ReadAllText(Application.dataPath + "/" + m_HLSLFilePath);

        }

    }

}
