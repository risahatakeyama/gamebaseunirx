using System;

namespace FictionProject.UI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PrefabPathAttribute : Attribute
    {
        public string path;

        public PrefabPathAttribute(string path)
        {
            this.path = path;
        }
    }
}