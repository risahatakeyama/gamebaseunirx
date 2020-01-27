using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FictionProject.Master
{
    public class PuzzleSetMB:MBBase
    {
        public long id { get; private set; }
        public List<long> puzzlePieceIdList { get; private set; }
        public string prefabPath { get; private set; }
        public PuzzleSetMB(long id,List<long> puzzlePieceIdList,string prefabPath):base(id)
        {
            this.puzzlePieceIdList = puzzlePieceIdList;
            this.prefabPath = prefabPath;
        }
    }
}

