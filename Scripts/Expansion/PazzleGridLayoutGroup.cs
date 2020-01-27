using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FictionProject.Expantion
{
    public class PazzleGridLayoutGroup : GridLayoutGroup
    {
        public int HorizontalPazzleCount;
        public int VarticalPazzleCount;
        public List<RectTransform> GetChildRectTransforms { get { return rectChildren; } }
        
    }
}

