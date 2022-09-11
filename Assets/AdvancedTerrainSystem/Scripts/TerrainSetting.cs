using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{
    [Serializable]
    public class TerrainSettings
    {
        public float width;
        public float length;
        public float height;
        public int heightMapRes = 513;
        public int alphaMapRes = 512;
    }

}
