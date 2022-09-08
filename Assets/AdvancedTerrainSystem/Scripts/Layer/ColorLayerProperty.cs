using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class ColorLayerProperty : LayerProperty
    {

        public ColorLayerProperty(string name) : base(name, "Color")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetColor(MatPropReferenceWithLayerIndex(layerIndex), Color_Value);

        }

    }

}
