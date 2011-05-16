using UnityEngine;
using UnityEditor;

public class Quad : MonoBehaviour
{
    public enum QuadFacingDirection { X = 0, Y = 1, Z = 2 }

    [MenuItem("Quads/Create Quad Facing Positive X")]
    static void CreateQuadPosX()
    {
        CreateQuad(QuadFacingDirection.X, false);
    }

    [MenuItem("Quads/Create Quad Facing Negative X")]
    static void CreateQuadNegX()
    {
        CreateQuad(QuadFacingDirection.X, true);
    }

    [MenuItem("Quads/Create Quad Facing Positive Y")]
    static void CreateQuadPosY()
    {
        CreateQuad(QuadFacingDirection.Y, true);
    }

    [MenuItem("Quads/Create Quad Facing Negative Y")]
    static void CreateQuadNegY()
    {
        CreateQuad(QuadFacingDirection.Y, false);
    }

    [MenuItem("Quads/Create Quad Facing Positive Z")]
    static void CreateQuadPosZ()
    {
        CreateQuad(QuadFacingDirection.Z, false);
    }

    [MenuItem("Quads/Create Quad Facing Negative Z")]
    static void CreateQuadNegZ()
    {
        CreateQuad(QuadFacingDirection.Z, true);
    }

    static void CreateQuad(QuadFacingDirection quadFacing, bool invertAxis)
    {
        GameObject Quad = new GameObject("Quad");
        Quad.transform.position = Vector3.zero;
        Quad.transform.localEulerAngles = Vector3.zero;
        Quad.transform.localScale = Vector3.one;
        Quad.transform.rotation = Quaternion.identity;
        Quad.transform.localRotation = Quaternion.identity;
        Quad.AddComponent("MeshFilter");
        Quad.AddComponent("MeshRenderer");
        Material quadMat = new Material("Shader \"Custom/Diffuse\" { Properties {_Color (\"Main Color\", Color) = (1,1,1,1) _SpecColor (\"Spec Color\", Color) = (1,1,1,1) _Emission (\"Emissive Color\", Color) = (0.5,0.5,0.5,1) _Shininess (\"Shininess\", Range (0.1, 1)) = 0.7 _MainTex (\"Base (RGB) Trans (A)\", 2D) = \"white\"} " +
        "SubShader { Tags {\"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\"} " +
        "Pass { Alphatest Greater 0 ZWrite Off Blend SrcAlpha OneMinusSrcAlpha ColorMask RGB " +
        "Material { Diffuse [_Color] Ambient [_Color] Shininess [_Shininess] Specular [_SpecColor] Emission [_Emission] } " +
        "Lighting On SeparateSpecular On SetTexture [_MainTex] { Combine texture * primary DOUBLE, texture * primary } }}} ");
        quadMat.color = Color.white;
        Mesh mesh = new Mesh();
        mesh.Clear();

        if (quadFacing == QuadFacingDirection.X)
        {
            mesh.vertices = new Vector3[] 
            {
                //FACING RIGHT/LEFT
                new Vector3(0f, 0.5f, -0.5f),
                new Vector3(0f,-0.5f, 0.5f),
                new Vector3(0f, -0.5f, -0.5f), 
                new Vector3(0f, 0.5f, 0.5f) 
            };
        }
        
        else if (quadFacing == QuadFacingDirection.Y)
        {
            mesh.vertices = new Vector3[] 
            {
                //FACING TOP/BOTTOM
                new Vector3(0.5f, 0, -0.5f),
                new Vector3(-0.5f, 0, 0.5f),
                new Vector3(-0.5f, 0, -0.5f), 
                new Vector3(0.5f, 0, 0.5f) 
            };
        }

        else if (quadFacing == QuadFacingDirection.Z)
        {
            mesh.vertices = new Vector3[] 
            {
                //FACING FRONT/BACK
                new Vector3(0.5f, -0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(-0.5f, -0.5f, 0f), 
                new Vector3(0.5f, 0.5f, 0f) 
            };        
        }

        mesh.RecalculateBounds();
        if (quadFacing == QuadFacingDirection.X)
        {
            mesh.uv = new Vector2[] 
            {
                new Vector2(1, 1),
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1)
            };
        }
        else
        {
            mesh.uv = new Vector2[] 
            {
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(0, 0),
                new Vector2(1, 1)
            };
        }


        if (quadFacing == QuadFacingDirection.X)
        {
            if (invertAxis)
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.left,
                    Vector3.left,
                    Vector3.left,
                    Vector3.left  
                };
            }
            else
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.right,
                    Vector3.right,
                    Vector3.right,
                    Vector3.right
                };
            }
        }

        else if (quadFacing == QuadFacingDirection.Y)
        {
            if (invertAxis)
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.up,
                    Vector3.up,
                    Vector3.up,
                    Vector3.up
                };
                
            }
            else
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.down,
                    Vector3.down,
                    Vector3.down,
                    Vector3.down
                };
            }
        }
        
        else if (quadFacing == QuadFacingDirection.Z)
        {
            if (invertAxis)
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.back,
                    Vector3.back,
                    Vector3.back,
                    Vector3.back
                };
            }
            else
            {
                mesh.RecalculateBounds();
                mesh.normals = new Vector3[]
                {
                    Vector3.forward,
                    Vector3.forward,
                    Vector3.forward,
                    Vector3.forward
                };
            }
        }
        mesh.RecalculateBounds();
        if (invertAxis)
        {
            mesh.triangles = new int[] { 0, 2, 1, 3, 0, 1 };
        }
        else
        {
            mesh.triangles = new int[] { 1, 2, 0, 1, 0, 3 };
        }
        Quad.GetComponent<MeshFilter>().mesh = mesh;
        Quad.GetComponent<MeshRenderer>().material = quadMat;
    }
}