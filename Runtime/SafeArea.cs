using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RanterTools.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        #region Events

        #endregion Events

        #region Global State

        #endregion Global State

        #region Global Methods

        #endregion Global Methods

        #region Parameters

        #endregion Parameters

        #region State
        RectTransform rectTransform;
        RectTransform RectTransform
        {
            get { return rectTransform = rectTransform ?? GetComponent<RectTransform>(); }
        }
        #endregion State

        #region Methods

        #endregion Methods

        #region Unity
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            RectTransform.anchorMin = RectTransform.anchorMax = RectTransform.pivot = new Vector2(0, 0);
            RectTransform.anchoredPosition = Screen.safeArea.position;
            RectTransform.sizeDelta = Screen.safeArea.size;
        }
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {

        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        void OnDisable()
        {

        }
        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {

        }

        #endregion Unity
    }
}