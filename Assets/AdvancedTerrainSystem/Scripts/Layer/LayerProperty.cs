using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AdvancedTerrainSystem
{

    [System.Serializable]
    public class LayerProperty
    {

        protected LayerProperty(string name, string typeName)
        {

            m_TypeName = typeName;
            m_Name = name;

        }



        [SerializeField]
        private string m_Name;

        [ReadOnly]
        [SerializeField]
        private string m_TypeName;



        [HideInInspector]
        public Color m_ColorValue = Color.white;
        public Color Color_Value
        {

            get
            {

                return m_ColorValue;

            }

            set
            {

                m_ColorValue = value;

            }

        }

        [HideInInspector]
        public float m_FloatValue = 0;
        public float Float_Value
        {

            get
            {

                return m_FloatValue;

            }

            set
            {

                m_FloatValue = value;

            }

        }

        [HideInInspector]
        public Vector4 m_Matrix4x4_R0_Value = Vector4.zero;
        public Vector4 m_Matrix4x4_R1_Value = Vector4.zero;
        public Vector4 m_Matrix4x4_R2_Value = Vector4.zero;
        public Vector4 m_Matrix4x4_R3_Value = Vector4.zero;
        public Matrix4x4 Matrix4x4_Value
        {

            get
            {

                return new Matrix4x4(
                    m_Matrix4x4_R0_Value,
                    m_Matrix4x4_R1_Value,
                    m_Matrix4x4_R2_Value,
                    m_Matrix4x4_R3_Value
                );

            }

            set
            {

                m_Matrix4x4_R0_Value = value.GetRow(0);
                m_Matrix4x4_R1_Value = value.GetRow(1);
                m_Matrix4x4_R2_Value = value.GetRow(2);
                m_Matrix4x4_R3_Value = value.GetRow(3);

            }

        }

        [HideInInspector]
        public Texture2D m_Texture2DValue = Texture2D.whiteTexture;
        public Texture2D Texture2D_Value
        {

            get
            {

                return m_Texture2DValue;

            }

            set
            {

                m_Texture2DValue = value;

            }

        }

        [HideInInspector]
        public Texture3D m_Texture3DValue;
        public Texture3D Texture3D_Value
        {

            get
            {

                return m_Texture3DValue;

            }

            set
            {

                m_Texture3DValue = value;

            }

        }

        [HideInInspector]
        public Vector2 m_Vector2Value = Vector2.zero;
        public Vector2 Vector2_Value
        {

            get
            {

                return m_Vector2Value;

            }

            set
            {

                m_Vector2Value = value;

            }

        }

        [HideInInspector]
        public Vector3 m_Vector3Value = Vector3.zero;
        public Vector3 Vector3_Value
        {

            get
            {

                return m_Vector3Value;

            }

            set
            {

                m_Vector3Value = value;

            }

        }

        [HideInInspector]
        public Vector4 m_Vector4Value = Vector4.zero;
        public Vector4 Vector4_Value
        {

            get
            {

                return m_Vector4Value;

            }

            set
            {

                m_Vector4Value = value;

            }

        }



        public string Name {

            get
            {

                return m_Name;

            }

            set
            {

                m_Name = value;

            }

        }

        public string TypeName
        {

            get
            {

                return m_TypeName;

            }

        }

    }

}
