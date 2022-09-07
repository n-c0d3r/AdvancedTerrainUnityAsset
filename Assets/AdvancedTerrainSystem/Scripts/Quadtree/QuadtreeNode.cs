using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public struct QuadtreeNode
    {

        public QuadtreeNode(GameObject node, uint numLevel, uint currentNodeLevel, Terrain terrain)
        {
            if(currentNodeLevel < numLevel)
            {
                float nodeWidth = terrain.settings.width * Mathf.Pow(2, (numLevel - currentNodeLevel));
                float nodeHeight = terrain.settings.height;
                float nodeLength = terrain.settings.length * Mathf.Pow(2, (numLevel - currentNodeLevel));

                Vector3 childNodePos = new Vector3(node.transform.position.x, nodeHeight, node.transform.position.z);
                float originChildNodePos = childNodePos.z;

                for (uint numChildNodesX = 0; numChildNodesX < 2; numChildNodesX++)
                {
                    childNodePos.z = originChildNodePos;
                    for (uint numChildNodesZ = 0; numChildNodesZ < 2; numChildNodesZ++)
                    {
                        var childNode = new GameObject();
                        childNode.transform.parent = node.transform;

                        if (currentNodeLevel == numLevel-1)
                        {
                            var chunkTerrain = childNode.gameObject.AddComponent<UnityEngine.Terrain>();
                            childNode.gameObject.AddComponent<TerrainCollider>();
                            chunkTerrain.terrainData = new TerrainData();
                            chunkTerrain.terrainData.size = new Vector3(terrain.settings.width * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1))), nodeHeight, terrain.settings.length * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1))));
                        }
                        

                       

                        childNode.gameObject.transform.position = childNodePos;

                        childNodePos.z += terrain.settings.length * Mathf.Pow(2, (numLevel - (currentNodeLevel + 1)));
                        new QuadtreeNode(childNode, numLevel, currentNodeLevel + 1, terrain);
                    }
                    childNodePos.x += terrain.settings.width * Mathf.Pow(2, (numLevel - (currentNodeLevel+1)));
                }
            }
            
        }
    }

    

}
