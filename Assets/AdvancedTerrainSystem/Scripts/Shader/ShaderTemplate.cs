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
        private string m_ShaderFilePath = "";

        public string ShaderFilePath
        {

            get
            {

                return m_ShaderFilePath;

            }

        }

        public string GetShaderFile()
        {

            return System.IO.File.ReadAllText(Application.dataPath + "/" + m_ShaderFilePath);

        }

    }

}
