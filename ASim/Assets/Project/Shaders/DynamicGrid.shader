Shader "Custom/DynamicGrid_AlwaysVisible"
{
    Properties
    {
        _GridColor ("Grid Color", Color) = (1,1,1,1)
        _CellSize ("Cell Size", Float) = 1
        _LineThickness ("Line Thickness", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _GridColor;
            float _CellSize;
            float _LineThickness;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            float GridLine(float coord, float thickness)
            {
                float d = abs(frac(coord) - 0.5);
                return smoothstep(thickness, 0.0, d);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 coord = i.worldPos.xy / _CellSize;

                // 📌 Çizgi kalınlığını sabit tut, minimuma sabitle
                float thickness = max(_LineThickness, 0.005); // Çok küçük olursa görünmez

                float lineX = GridLine(coord.x, thickness);
                float lineY = GridLine(coord.y, thickness);
                float gridLine = max(lineX, lineY);

                fixed4 col = _GridColor;
                col.a *= gridLine;

                clip(col.a - 0.01); // saydam alanları kırp

                return col;
            }
            ENDCG
        }
    }
}
