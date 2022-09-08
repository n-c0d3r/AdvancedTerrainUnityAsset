using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class Texture2DLayerProperty : LayerProperty
    {

        public Texture2DLayerProperty(string name) : base(name, "Texture2D")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetTexture(MatPropReferenceWithLayerIndex(layerIndex), Texture2D_Value);

        }

    }

}
