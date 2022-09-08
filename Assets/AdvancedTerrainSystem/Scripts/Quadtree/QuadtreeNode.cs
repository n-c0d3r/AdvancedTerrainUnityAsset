using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class QuadtreeNode
    {

        public QuadtreeNode[] childs = null;
        public GameObject gobj = null;
        public uint currentNodeLevel = 0;
        public float nodeWidth = 0.0f;
        public float nodeLength = 0.0f;
        public UnityEngine.Terrain chunkTerrain = null;



        public QuadtreeNode(GameObject gobj, uint numLevel, uint currentNodeLevel, Terrain terrain)
        {
            this.gobj = gobj;
            this.currentNodeLevel = currentNodeLevel;

            float width = terrain.settings.width;
            float length = terrain.settings.length;

            float nodeWidth = width / ((float)(1 << (int)currentNodeLevel));
            float nodeLength = length / ((float)(1 << (int)currentNodeLevel));

            float chunkWidth = width / ((float)(1 << (int)(numLevel - 1)));
            float chunkLength = length / ((float)(1 << (int)(numLevel - 1)));

            this.nodeWidth = nodeWidth;
            this.nodeLength = nodeLength;



            if (currentNodeLevel == 0)
            {

                gobj.transform.position += new Vector3(-chunkWidth * 0.5f, 0, -chunkLength * 0.5f);

            }



            if (currentNodeLevel < numLevel)
            {

                if (currentNodeLevel == numLevel-1)
                {

                    var chunkTerrain = gobj.AddComponent<UnityEngine.Terrain>();
                    this.chunkTerrain = chunkTerrain;
                    gobj.AddComponent<TerrainCollider>();
                    chunkTerrain.terrainData = new TerrainData();
                    chunkTerrain.terrainData.size = new Vector3(chunkWidth, terrain.settings.height, chunkLength);

                }
                else
                {

                    childs = new QuadtreeNode[4];

                    for (uint numChildNodesX = 0; numChildNodesX < 2; numChildNodesX++)
                    {

                        for (uint numChildNodesZ = 0; numChildNodesZ < 2; numChildNodesZ++)
                        {

                            Vector3 childNodePos = gobj.transform.position + 0.5f * new Vector3((float)numChildNodesX * nodeWidth - nodeWidth * 0.5f, 0, (float)numChildNodesZ * nodeLength - nodeLength * 0.5f);// + new Vector3(-chunkWidth * 0.5f, 0, -chunkLength * 0.5f);

                            var childNodeGObj = new GameObject("Node");
                            childNodeGObj.transform.parent = gobj.transform;



                            childNodeGObj.gameObject.transform.position = childNodePos;

                            childs[numChildNodesX + numChildNodesZ * 2] = new QuadtreeNode(childNodeGObj, numLevel, currentNodeLevel + 1, terrain);
                        }

                    }

                }
            }
            
        }
    }

    

}
