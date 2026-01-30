Shader "TyrAds/UI/LinearGradient"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (0,0,0,1)
        _Angle ("Angle", Range(0,360)) = 0
        _Center ("Center", Range(0,1)) = 0.5

        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Pass
        {
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest [unity_GUIZTestMode]
            ColorMask [_ColorMask]

            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float2 localPos : TEXCOORD1;
            };

            fixed4 _Color1;
            fixed4 _Color2;
            float _Angle;
            float _Center;
            float2 _RectMin;
            float2 _RectMax;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);
                OUT.color = IN.color;
                OUT.localPos = IN.vertex.xy;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 dir = float2(cos(radians(_Angle)), sin(radians(_Angle)));
                float2 size = _RectMax - _RectMin;
                float2 centerUV = (IN.localPos - _RectMin) / size - _Center;
                float gradientValue = saturate(dot(centerUV, dir) + 0.5);
                fixed4 gradientColor = lerp(_Color1, _Color2, gradientValue);
                fixed4 texColor = tex2D(_MainTex, IN.uv);
                fixed4 finalColor = gradientColor * texColor * IN.color;

                return finalColor;
            }
            ENDCG
        }
    }
}