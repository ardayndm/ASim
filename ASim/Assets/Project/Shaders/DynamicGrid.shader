Shader "Custom/DynamicGrid"
{
    Properties
    {
        _GridColor ("Grid Color", Color) = (1,1,1,1)
        _CellSize ("Cell Size", Float) = 1
        _LineThickness ("Line Thickness", Float) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
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
                float2 uv : TEXCOORD0;
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
                // coord in [0..1] range, line centered at 0.5
                float dist = abs(frac(coord) - 0.5);
                float edge = thickness * 0.5;
                return smoothstep(edge, 0.0, dist);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // World position X and Y
                float2 worldXY = i.worldPos.xy;

                // Normalize world coords by cell size
                float2 coord = worldXY / _CellSize;

                // Calculate line intensities for X and Y
                float lineX = GridLine(coord.x, _LineThickness);
                float lineY = GridLine(coord.y, _LineThickness);

                // Combine lines (max to get crossing lines)
                float lineIntensity = max(lineX, lineY);

                // Final color with alpha based on line intensity
                fixed4 col = _GridColor;
                col.a *= lineIntensity;

                // Discard pixels with low alpha for transparency
                clip(col.a - 0.01);

                return col;
            }
            ENDCG
        }
    }
}
