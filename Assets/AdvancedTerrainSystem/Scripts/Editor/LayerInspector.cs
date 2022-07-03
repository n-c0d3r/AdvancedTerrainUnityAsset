using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace AdvancedTerrainSystem
{

    [CustomEditor(typeof(Layer))]
    public class LayerInspector : Editor
    {
        public override void OnInspectorGUI()
        {

            Layer layer = (Layer)target;



            DrawDefaultInspector();



            //Create New Property Btn
            {

                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("Create New Property"))
                {

                    layer.CreateNewProperty("NewPropertyName", m_Name2PropertyType[m_CreateNewPropertyBtn_DisplayedOption]);

                }

                m_CreateNewPropertyBtn_Selected = EditorGUILayout.Popup("", m_CreateNewPropertyBtn_Selected, m_CreateNewPropertyBtn_DisplayedOptions);

                EditorGUILayout.EndHorizontal();

            }

        }

        public void OnSceneGUI()
        {

            Layer layer = (Layer)target;

        }



        
        private int m_CreateNewPropertyBtn_Selected = 0;

        private string[] m_CreateNewPropertyBtn_DisplayedOptions = new string[] {

            "Color",

            "HDRColor",

            "Float",

            "Matrix4x4",

            "Texture2D",

            "Texture3D",

            "Vector2",

            "Vector3",

            "Vector4"

        };

        public string m_CreateNewPropertyBtn_DisplayedOption
        {

            get
            {

                return m_CreateNewPropertyBtn_DisplayedOptions[m_CreateNewPropertyBtn_Selected];

            }

        }

        private Dictionary<string, System.Type> m_Name2PropertyType = new Dictionary<string, System.Type>
        {

            { "Color", typeof(ColorLayerProperty) },

            { "HDRColor", typeof(HDRColorLayerProperty) },

            { "Float", typeof(FloatLayerProperty) },

            { "Matrix4x4", typeof(Matrix4x4LayerProperty) },

            { "Texture2D", typeof(Texture2DLayerProperty) },

            { "Texture3D", typeof(Texture3DLayerProperty) },

            { "Vector2", typeof(Vector2LayerProperty) },

            { "Vector3", typeof(Vector3LayerProperty) },

            { "Vector4", typeof(Vector4LayerProperty) }

        };
        

    }

}
