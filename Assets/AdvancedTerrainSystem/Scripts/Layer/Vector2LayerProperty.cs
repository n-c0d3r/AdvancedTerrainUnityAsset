using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class Vector2LayerProperty : LayerProperty
    {

        public Vector2LayerProperty(string name) : base(name, "Vector2")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetVector(MatPropReferenceWithLayerIndex(layerIndex), Vector2_Value);

        }

    }

}
