using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Be aware this will not prevent a non singleton constructor
    ///   such as `T myT = new T();`
    /// To prevent that, add `protected T () {}` to your singleton class.
    /// 
    /// As a note, this is made as MonoBehaviour because we need Coroutines.
    /// </summary>
    public class SingletonBehaviour<TInternal, TInstance> : MonoBehaviour where TInternal : MonoBehaviour, TInstance
    {
        #region Fields
        private readonly static object _lock = new object();

        private static TInternal _instance;
        private static bool _applicationIsQuitting;
        #endregion

        #region Properties
        public static TInstance Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(TInternal) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return default(TInstance);
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (TInternal)FindObjectOfType(typeof(TInternal));

                        if (FindObjectsOfType(typeof(TInternal)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopenning the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            var singleton = new GameObject();
                            _instance = singleton.AddComponent<TInternal>();
                            singleton.name = "(singleton) " + typeof(TInternal).ToString();

                            DontDestroyOnLoad(singleton);

                            Debug.Log("[Singleton] An instance of " + typeof(TInternal) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " +
                                _instance.gameObject.name);
                        }
                    }

                    return _instance;
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public void OnDestroy()
        {
            _applicationIsQuitting = true;
        }
        #endregion
    }
}
