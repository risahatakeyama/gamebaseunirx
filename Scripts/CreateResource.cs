using System;
using UnityEngine;

namespace FictionProject.UI
{
    /// <summary>
    /// リソースを
    /// </summary>
    public class CreateResource
    {
        private static CreateResource _createResource = new CreateResource();

        //プロパティ
        public static CreateResource Instance
        {
            get
            {
                return _createResource;
            }
        }

        /// <summary>
        /// スクリプト付きのゲームオブジェクトを生成
        /// </summary>
        public T CreateGameObject<T>(Transform parent = null) where T : MonoBehaviour
        {
            T t = (T)Activator.CreateInstance(typeof(T));

            var attr = (PrefabPathAttribute)Attribute.GetCustomAttribute(t.GetType(), typeof(PrefabPathAttribute));
            var path = attr.path;
            GameObject prefab = Resources.Load<GameObject>(path);
            GameObject instanceObject = null;
            if (parent == null)
            {
                instanceObject = (GameObject)MonoBehaviour.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
            }
            else
            {
                instanceObject = (GameObject)MonoBehaviour.Instantiate(prefab, parent.position, parent.rotation, parent);
            }
            instanceObject.name = prefab.name;

            T res = (T)(object)instanceObject.GetComponent<T>();
            return res;
        }
    }
}