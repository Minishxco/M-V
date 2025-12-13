Shader "UI/WhiteImageWithOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", Range(0,5)) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alpha = tex2D(_MainTex, i.uv).a;

                // Outline detection
                float outlineAlpha = 0;
                float2 offset = _MainTex_TexelSize.xy * _OutlineSize;

                outlineAlpha += tex2D(_MainTex, i.uv + float2( offset.x, 0)).a;
                outlineAlpha += tex2D(_MainTex, i.uv + float2(-offset.x, 0)).a;
                outlineAlpha += tex2D(_MainTex, i.uv + float2(0,  offset.y)).a;
                outlineAlpha += tex2D(_MainTex, i.uv + float2(0, -offset.y)).a;

                outlineAlpha = saturate(outlineAlpha - alpha);

                fixed4 baseColor = fixed4(1,1,1,alpha);
                fixed4 outline = fixed4(_OutlineColor.rgb, outlineAlpha * _OutlineColor.a);

                return lerp(outline, baseColor, alpha);
            }
            ENDCG
        }
    }
}
