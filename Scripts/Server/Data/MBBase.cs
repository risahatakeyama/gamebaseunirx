using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FictionProject.Master
{
    public class MBBase
    {
        public long id { get; private set; }
        public MBBase(long id)
        {
            this.id = id;
        }

    }
}
