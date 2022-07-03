using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Layer", menuName = "AdvancedTerrainSystem/Layer", order = 1)]
    public class Layer : ScriptableObject
    {

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

    }

}
