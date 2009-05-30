Shader "iPhone/Simple Color + No Light + Transparent" {
	Properties {
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	Category {
		Tags { "Queue"="Transparent" "RenderType"="Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

		SubShader {
			Pass {
				Color [_Color]
			}
		}
   }
}