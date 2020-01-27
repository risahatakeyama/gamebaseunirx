using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FictionProject
{
    public class SingletonMonobihavior<T> : MonoBehaviour where T : new()
    {
        private static T _instance = new T();
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
