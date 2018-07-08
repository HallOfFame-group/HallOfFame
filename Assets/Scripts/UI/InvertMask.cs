using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class InvertMask :  Image
{
    public Material invertMat;
    public override Material materialForRendering
    {
        get
        {
            invertMat = base.materialForRendering;
            invertMat.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return invertMat;
        }
    }
}
