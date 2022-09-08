using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class Matrix4x4LayerProperty : LayerProperty
    {

        public Matrix4x4LayerProperty(string name) : base(name, "Matrix4x4")
        {



        }



        public override void Apply2Material(Material material, uint layerIndex)
        {

            material.SetMatrix(MatPropReferenceWithLayerIndex(layerIndex), Matrix4x4_Value);

        }

    }

}
