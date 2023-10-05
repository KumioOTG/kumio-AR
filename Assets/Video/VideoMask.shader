Shader "Custom/VideoMask" {
    Properties {
        _MainTex ("Video Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture (white for visible)", 2D) = "white" {}
        _MainTex_ST ("Main Texture Tiling and Offset", Vector) = (1, 1, 0, 0) // Declare this
    }

    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _MaskTex;

            // Declare the transformation matrix here
            float4 _MainTex_ST;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target {
                half4 col = tex2D(_MainTex, i.uv);
                half4 mask = tex2D(_MaskTex, i.uv);
                return col * mask.a;
            }
            ENDCG
        }
    }
}
