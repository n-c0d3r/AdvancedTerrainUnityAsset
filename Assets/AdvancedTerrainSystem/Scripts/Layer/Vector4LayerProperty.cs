using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class Vector4LayerProperty : LayerProperty
    {

        public Vector4LayerProperty(string name) : base(name, "Vector4")
        {



        }



        public virtual void Apply2Material(Material material, uint layerIndex)
        {

            material.SetVector(MatPropReferenceWithLayerIndex(layerIndex), Vector4_Value);

        }

    }

}
