Shader "CnYz/AlwaysVisible"
{
    Properties{
		_Color("Color", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (1,0,0,1)
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		//Render a pass if anything is infront 
		Pass
		{
			ZTest Greater
			ZWrite Off
			Cull Front
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
				float4 vertex : SV_POSITION;
			};

			float4 _OutlineColor;

			v2f vert(appdata_full v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return  _OutlineColor;
			}
		ENDCG
		}

		Pass
		{
			ZTest LEqual
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag() : SV_Target
			{
				return _Color;
			}
		ENDCG
		}
	}
 }
