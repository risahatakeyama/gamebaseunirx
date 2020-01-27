using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FictionProject.Master
{
    public class PuzzlePieceMB:MBBase  {
        public long id { get; set; }
        public string imagePath { get; set; }
        public PuzzlePieceMB(long id,string imagePath) : base(id)
        {
            this.imagePath = imagePath;
        }
    }

}

