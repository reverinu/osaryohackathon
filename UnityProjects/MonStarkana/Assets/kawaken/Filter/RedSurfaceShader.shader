Shader "Custom/RedSurfaceShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200
		Pass{
		CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag

#include "UnityCG.cginc"

		uniform sampler2D _MainTex;

	fixed4 frag(v2f_img i) : SV_Target{
		float4 c = tex2D(_MainTex, i.uv);
		float f = (c.r + c.g + c.b) / 3;

		return float4(0.25, c.g, c.b, 1);
	}
		ENDCG
	}
	}
}