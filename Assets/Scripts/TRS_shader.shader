Shader "Custom/TRS_shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CutoutThreshold ("Cutout Threshold", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        AlphaTest Greater [_CutoutThreshold]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
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
            float4x4 _MyTRSMatrix;
            float _CutoutThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(mul(_MyTRSMatrix, v.vertex));
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a = col.a > _CutoutThreshold ? 1 : 0;
                return col;
            }
            ENDCG
        }
    }
}