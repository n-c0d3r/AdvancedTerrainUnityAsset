using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Layer", menuName = "AdvancedTerrainSystem/Layer", order = 1)]
    public class Layer : ScriptableObject
    {

        protected void Awake()
        {



        }



        [SerializeField]
        private string m_HLSLFilePath = "";

        public string HLSLFilePath
        {

            get
            {

                return m_HLSLFilePath;

            }

        }

        public string CompiledHLSLPath(int i)
        {

            return HLSLFilePath + ".COMPILED" + i.ToString() + ".hlsl";

        }

        public List<LayerProperty> m_Properties = new List<LayerProperty>();



        public LayerProperty CreateNewProperty(string name, System.Type type)
        {

            LayerProperty property = (LayerProperty)System.Activator.CreateInstance(
                type,
                new object[] {
                    name
                }
            );

            m_Properties.Add(property);

            return property;

        }



        public string GetHLSL()
        {

            return System.IO.File.ReadAllText(Application.dataPath + "/" + m_HLSLFilePath);

        }

    }

}
