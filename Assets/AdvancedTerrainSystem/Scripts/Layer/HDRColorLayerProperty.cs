using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class HDRColorLayerProperty : LayerProperty
    {

        public HDRColorLayerProperty(string name) : base(name, "HDRColor")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetColor(MatPropReferenceWithLayerIndex(layerIndex), Color_Value);

        }

    }

}
