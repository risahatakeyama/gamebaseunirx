using System;
using System.Collections.Generic;
using UnityEngine;

namespace FictionProject.Expansion
{
    public static class DictionaryExpansion
    {
        public static T Get<T>(this Dictionary<string, object> param, string key)
        {
            try
            {
                T res = (T)(object)param[key];
                return res;
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return default(T);
            }
            
        }

        public static int GetInt(this Dictionary<string, object> param, string key)
        {
            try
            {
                return (int)param[key];
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return default(int);
            }
        }
        public static float GetFloat(this Dictionary<string,object> param,string key)
        {
            try
            {
                return (float)param[key];
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return default(float);
            }
        }
        public static string GetString(this Dictionary<string,object> param,string key)
        {
            try
            {
                return (string)param[key];
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                return default(string);
            }
        }

    }
}