using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace AdvancedTerrainSystem
{

    [CustomPropertyDrawer(typeof(LayerProperty))]
    public class LayerPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return (

                0

                + 2
            
                + EditorGUIUtility.singleLineHeight 
                
                + 2

                + EditorGUIUtility.singleLineHeight       
                
                + 2

                + m_Name2ValurPropertyHeight[property.FindPropertyRelative("m_TypeName").stringValue]

            );
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {

            EditorGUI.BeginProperty(position, label, property);



            EditorGUI.LabelField(
                new Rect(
                    position.position + new Vector2(0, 2),
                    new Vector2(80, EditorGUIUtility.singleLineHeight)
                ),
                "Name"
            );

            property.FindPropertyRelative("m_Name").stringValue = EditorGUI.TextField(
                new Rect(
                    position.position + new Vector2(80, 2),
                    new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)
                ),
                property.FindPropertyRelative("m_Name").stringValue
            );



            EditorGUI.LabelField(
                new Rect(
                    position.position + new Vector2(0, EditorGUIUtility.singleLineHeight + 4),
                    new Vector2(80, EditorGUIUtility.singleLineHeight)
                ),
                "Type Name"
            );

            GUI.enabled = false;
            EditorGUI.TextField(
                new Rect(
                    position.position + new Vector2(80, EditorGUIUtility.singleLineHeight + 4),
                    new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)
                ),
                property.FindPropertyRelative("m_TypeName").stringValue
            );
            GUI.enabled = true;







            EditorGUI.LabelField(
                new Rect(
                    position.position + new Vector2(0, EditorGUIUtility.singleLineHeight * 2 + 6),
                    new Vector2(80, EditorGUIUtility.singleLineHeight)
                ),
                "Value"
            );

            Vector2 valueFieldPosition = position.position + new Vector2(80, EditorGUIUtility.singleLineHeight * 2 + 6);

            switch (property.FindPropertyRelative("m_TypeName").stringValue)
            {
                case "Color":

                    property.FindPropertyRelative("m_ColorValue").colorValue = EditorGUI.ColorField(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        property.FindPropertyRelative("m_ColorValue").colorValue
                    );

                    break;

                case "Float":

                    property.FindPropertyRelative("m_FloatValue").floatValue = EditorGUI.FloatField(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        property.FindPropertyRelative("m_FloatValue").floatValue
                    );

                    break;

                case "Matrix4x4":

                    property.FindPropertyRelative("m_Matrix4x4_R0_Value").vector2Value = EditorGUI.Vector4Field(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Matrix4x4_R0_Value").vector2Value
                    );

                    property.FindPropertyRelative("m_Matrix4x4_R1_Value").vector2Value = EditorGUI.Vector4Field(
                        new Rect(

                            valueFieldPosition + new Vector2(0, EditorGUIUtility.singleLineHeight + 2),

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Matrix4x4_R1_Value").vector2Value
                    );

                    property.FindPropertyRelative("m_Matrix4x4_R2_Value").vector2Value = EditorGUI.Vector4Field(
                        new Rect(

                            valueFieldPosition + new Vector2(0, EditorGUIUtility.singleLineHeight + 2) * 2,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Matrix4x4_R2_Value").vector2Value
                    );

                    property.FindPropertyRelative("m_Matrix4x4_R3_Value").vector2Value = EditorGUI.Vector4Field(
                        new Rect(

                            valueFieldPosition + new Vector2(0, EditorGUIUtility.singleLineHeight + 2) * 3,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Matrix4x4_R3_Value").vector2Value
                    );

                    break;

                case "Texture2D":

                    //UnityEngine.Object ObjectField(Rect position, string label, UnityEngine.Object obj, Type objType);
                    property.FindPropertyRelative("m_Texture2DValue").objectReferenceValue = EditorGUI.ObjectField(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Texture2DValue").objectReferenceValue,

                        typeof(Texture2D)

                    );

                    break;

                case "Texture3D":

                    //UnityEngine.Object ObjectField(Rect position, string label, UnityEngine.Object obj, Type objType);
                    property.FindPropertyRelative("m_Texture3DValue").objectReferenceValue = EditorGUI.ObjectField(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Texture3DValue").objectReferenceValue,

                        typeof(Texture3D)

                    );

                    break;

                case "Vector2":

                    property.FindPropertyRelative("m_Vector2Value").vector2Value = EditorGUI.Vector2Field(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Vector2Value").vector2Value
                    );

                    break;

                case "Vector3":

                    property.FindPropertyRelative("m_Vector3Value").vector2Value = EditorGUI.Vector3Field(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Vector3Value").vector2Value
                    );

                    break;

                case "Vector4":

                    property.FindPropertyRelative("m_Vector4Value").vector2Value = EditorGUI.Vector4Field(
                        new Rect(

                            valueFieldPosition,

                            new Vector2(position.width - 80, EditorGUIUtility.singleLineHeight)

                        ),

                        "",

                        property.FindPropertyRelative("m_Vector4Value").vector2Value
                    );

                    break;

                default:

                    break;
            }






            EditorGUI.EndProperty();

        }

        public float GetValuePropertyHeight(SerializedProperty property)
        {

            

            return 0;

        }

        private static Dictionary<string, float> m_Name2ValurPropertyHeight = new Dictionary<string, float>()
        {

            { "Color", EditorGUIUtility.singleLineHeight },

            { "Float", EditorGUIUtility.singleLineHeight },

            { "Matrix4x4", EditorGUIUtility.singleLineHeight * 4 + 8 },

            { "Texture2D", EditorGUIUtility.singleLineHeight },

            { "Texture3D", EditorGUIUtility.singleLineHeight },

            { "Vector2", EditorGUIUtility.singleLineHeight },

            { "Vector3", EditorGUIUtility.singleLineHeight },

            { "Vector4", EditorGUIUtility.singleLineHeight }

        };

    }

}