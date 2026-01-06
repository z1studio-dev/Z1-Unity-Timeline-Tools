Shader "Skybox/Blended Cubemap"
{
    Properties
    {
        _CubeA    ("Cubemap A",  CUBE) = ""         {}
        _CubeB    ("Cubemap B",  CUBE) = ""         {}
        _Blend    ("Blend",      Range(0,1)) = 0.0   // 0 = A, 1 = B
        _Tint     ("Tint",       Color)      = (1,1,1,1)
        _Exposure ("Exposure",   Float)      = 1.0
        _RotationA ("Rotation A (deg)", Float) = 0      
        _RotationB ("Rotation B (deg)", Float) = 0      
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" }
        Cull Off ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            samplerCUBE _CubeA, _CubeB;
            float _Blend;
            half4 _Tint;
            half _Exposure;
            float  _RotationA;
            float  _RotationB;

            struct appdata { float4 vertex : POSITION; };
            struct v2f     { float4 vertex : SV_POSITION; float3 dir : TEXCOORD0; };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.dir = normalize(mul((float3x3)UNITY_MATRIX_M, v.vertex.xyz));
                return o;
            }
            
            half3 RotateY(float3 v, float deg)
            {
                float  rad = radians(deg);
                float  s = sin(rad), c = cos(rad);
                return float3(c*v.x - s*v.z, v.y, s*v.x + c*v.z);
            }



            half4 frag (v2f i) : SV_Target
            {
                half4 a = texCUBE(_CubeA, RotateY(i.dir, _RotationA));
                half4 b = texCUBE(_CubeB, RotateY(i.dir, _RotationB));
                half hdrScale = exp2(_Exposure);          // = 2 ^ _Exposure
                half4 col = lerp(a, b, _Blend) * _Tint * hdrScale;
                return half4(col.rgb, 1);
            }
            ENDHLSL
        }
    }
    FallBack "RenderFX/Skybox"
}
