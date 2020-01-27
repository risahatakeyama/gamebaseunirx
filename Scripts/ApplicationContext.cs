using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FictionProject.Master;
namespace FictionProject
{
    public class ApplicationContext : SingletonMonobihavior<ApplicationContext>
    {

        private static MasterLoader _masterLoader = new MasterLoader();
        public static MasterLoader MasterLoader { get { return _masterLoader; } }

    }
}
