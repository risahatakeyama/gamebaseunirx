using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FictionProject.Data
{
    public class PuzzlePieceInfo
    {
        public long id { get; set; }
        public Sprite sprite { get; set; }
        public PuzzlePieceInfo(long id,Sprite sprite)
        {
            this.id = id;
            this.sprite=sprite;
        }

    }
}

