using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class Texture3DLayerProperty : LayerProperty
    {

        public Texture3DLayerProperty(string name) : base(name, "Texture3D")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetTexture(MatPropReferenceWithLayerIndex(layerIndex), Texture3D_Value);

        }

    }

}
