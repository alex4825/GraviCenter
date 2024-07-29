Shader "Custom/CircularGradient"
{
    Properties
    {
        _MainTex ("Gradient Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5; // Центрирование UV
                float dist = length(uv); // Расстояние от центра
                float alpha = 1.0 - smoothstep(0.45, 0.5, dist); // Круглый градиент

                fixed4 color = tex2D(_MainTex, i.uv);
                color.a *= alpha; // Применение градиента
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}