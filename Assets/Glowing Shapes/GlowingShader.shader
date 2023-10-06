Shader "Custom/GlowingShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Glow Color", Color) = (0,0,0,1)
        _GlowStrength ("Glow Strength", Range(0, 1)) = 0.5
        _Speed ("Glow Speed", Range(0, 10)) = 2
    }

    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // This enables transparency

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
                fixed4 color : COLOR;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _GlowStrength;
            float _Speed;

            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                float glow = _GlowStrength * sin(_Time.y * _Speed);
                o.color = _Color * glow;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Use the texture's alpha as a mask for the glow
                col.rgb += i.color.rgb * col.a;

                return col;
            }
            ENDCG
        }
    }
}
