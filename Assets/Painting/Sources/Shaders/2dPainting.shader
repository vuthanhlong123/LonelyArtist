Shader "Custom/2dPainting"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _BrushPosition ("Brush Position", Vector) = (0, 0, 0, 0)
        _BrushSize ("Brush Size", Float) = 0.1
        _BrushColor ("Brush Color", Color) = (1, 0, 0, 1)
        _BrushPattern ("Brush Pattern", 2D) = "white" { }
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;   
            sampler2D _BrushPattern;
            float4 _BrushPosition;
            float _BrushSize;
            float4 _BrushColor;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                /*float dist = distance(i.worldPos, _BrushPosition.xyz);

                if (dist < _BrushSize)
                {
                    fixed4 baseColor = tex2D(_MainTex, i.uv);
                    fixed4 patternColor = tex2D(_BrushPattern, i.uv);
                    //return lerp(baseColor, patternColor * _BrushColor, 1.0 - dist / _BrushSize);
                    return lerp(baseColor, patternColor * _BrushColor, 1 );
                }
                return tex2D(_MainTex, i.uv);
                */

                float4 col  = tex2D(_MainTex, i.uv);
				float  size = _BrushSize;
				float  soft = 1;
				float  dist = distance(_BrushPosition.xyz, i.worldPos);
				float  f	= distance(_BrushPosition.xyz, i.worldPos);
					   f    = 1.0-smoothstep(size*soft, size, f);
				
					   col = lerp(col, _BrushColor, f* _BrushPosition.w* 1);
					   col = float4(col.x, col.y, col.z, 1.0f);

				return col;
                
            }
            ENDCG
        }
    }
}
