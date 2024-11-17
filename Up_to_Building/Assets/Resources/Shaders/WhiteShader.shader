Shader "Unlit/WhiteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintFloat ("Tint Float", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _TintFloat;

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

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed3 tintedColor = lerp(texColor.rgb, float3(1,1,1), _TintFloat); // tintFloat에 따라 흰색으로 보간
                return fixed4(tintedColor, texColor.a * (1 - _TintFloat)); // 알파 값도 tintFloat에 따라 조정
            }
            ENDCG
        }
    }   
}
