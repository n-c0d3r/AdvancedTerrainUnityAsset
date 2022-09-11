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
        public float chunkWidth = 0.0f;
        public float chunkLength = 0.0f;
        public UnityEngine.Terrain chunkTerrain = null;
        public int index = 0;
        public int numLevel = 0;



        public bool IsLeafNode()
        {

            if (childs == null)
            {

                return true;

            }

            if (childs.Length > 0)
            {

                if (childs[0] == null)
                {

                    return true;

                }

            }
            else
                return true;

            return false;
        }



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
            this.chunkWidth = chunkWidth;
            this.chunkLength = chunkLength;
            this.numLevel = (int)numLevel;



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
                    var chunkTerrainCollider = gobj.AddComponent<TerrainCollider>();

                    chunkTerrain.allowAutoConnect = true;

                    chunkTerrain.terrainData = new TerrainData();
                    chunkTerrain.terrainData.alphamapResolution = terrain.settings.alphaMapRes;
                    chunkTerrain.terrainData.heightmapResolution = terrain.settings.heightMapRes;
                    chunkTerrain.terrainData.size = new Vector3(chunkWidth, terrain.settings.height, chunkLength);

                    chunkTerrain.terrainData.terrainLayers = terrain.m_TerrainLayers;

                    chunkTerrainCollider.terrainData = chunkTerrain.terrainData;

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
                            childs[numChildNodesX + numChildNodesZ * 2].index = (int)(numChildNodesX + numChildNodesZ * 2);
                        }

                    }

                }
            }
            
        }


        public void CopyFromBackup(QuadtreeNode backup)
        {

            int chunkCountX = (1 << (numLevel - 1));
            int chunkCount = chunkCountX * chunkCountX;

            UnityEngine.Terrain[] chunkTerrains = new UnityEngine.Terrain[chunkCount];
            UnityEngine.Terrain[] bu_chunkTerrains = new UnityEngine.Terrain[chunkCount];

            void AddToChunkTerrains(QuadtreeNode node, int id, UnityEngine.Terrain[] chkTerrs)
            {

                if (!node.IsLeafNode())
                {

                    for (int i = 0; i < 4; i++)
                    {

                        int childLocalId = i << (int)((numLevel - (node.currentNodeLevel + 1) - 1) * 2);

                        int childId = childLocalId + id;

                        AddToChunkTerrains(node.childs[i], childId, chkTerrs);

                    }

                }
                else
                {

                    chkTerrs[id] = node.chunkTerrain;

                }

            }

            AddToChunkTerrains(this, 0, chunkTerrains);
            AddToChunkTerrains(backup, 0, bu_chunkTerrains);



            for (int i = 0; i < chunkCount; i++)
            {

                UnityEngine.Terrain target_terr = chunkTerrains[i];
                UnityEngine.Terrain bu_terr = bu_chunkTerrains[i];

                TerrainData oldData = target_terr.terrainData;

                target_terr.terrainData = bu_terr.terrainData;

                if(
                    target_terr.terrainData.alphamapResolution
                    != oldData.alphamapResolution
                )
                    target_terr.terrainData.alphamapResolution = oldData.alphamapResolution;

                if (
                    target_terr.terrainData.heightmapResolution
                    != oldData.heightmapResolution
                )
                    target_terr.terrainData.heightmapResolution = oldData.heightmapResolution;

                if (
                    target_terr.terrainData.size
                    != oldData.size
                )
                    target_terr.terrainData.size = oldData.size;

                target_terr.terrainData.terrainLayers = oldData.terrainLayers;

                target_terr.gameObject.GetComponent<TerrainCollider>().terrainData = target_terr.terrainData;

            }

        }

    }

    

}
