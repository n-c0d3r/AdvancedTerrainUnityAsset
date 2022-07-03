using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace AdvancedTerrainSystem
{

    [CustomEditor(typeof(Terrain))]
    public class TerrainInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            Terrain terrain = (Terrain)target;



            DrawDefaultInspector();





        }

        public void OnSceneGUI()
        {

            Terrain terrain = (Terrain)target;

        }

    }

}
