using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class FloatLayerProperty : LayerProperty
    {

        public FloatLayerProperty(string name) : base(name, "Float")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetFloat(MatPropReferenceWithLayerIndex(layerIndex), Float_Value);

        }

    }

}
