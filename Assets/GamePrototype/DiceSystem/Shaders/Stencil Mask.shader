Shader "Custom/StencilMask"
{
    SubShader
    {
        Tags 
        { 
            "RenderPipeline"="UniversalPipeline"
            "Queue" = "Geometry-1"
        }
        ColorMask 0
        ZWrite Off

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                pass Replace
            }
        }
    }
}
