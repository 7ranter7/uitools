using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RanterTools.UI
{

    /// <summary>
    /// Component for fast convert current size on current screen size to anchors.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class AnchorsFromCurrentScreen : MonoBehaviour
    {

        #region  State
        Vector2 defaultPosition;
        Vector2 defaultSize;
        Vector2 defaultAnchorMin, defaultAnchorMax;
        Vector2 defaultPivot;
        #endregion State
        #region Methods

        /// <summary>
        /// Convert current size for currect screen to anchors.
        /// </summary>
        public void ApplyAnchors()
        {
            RectTransform rectTransform = transform as RectTransform, parentRectTransform = transform.parent as RectTransform;
            defaultPosition = rectTransform.anchoredPosition;
            defaultSize = rectTransform.sizeDelta;
            defaultPivot = rectTransform.pivot;
            defaultAnchorMin = rectTransform.anchorMin;
            defaultAnchorMax = rectTransform.anchorMax;
            rectTransform.pivot = rectTransform.anchorMin = rectTransform.anchorMax = Vector2.zero;
            Vector2 min = new Vector2(rectTransform.anchoredPosition.x / parentRectTransform.rect.width, Mathf.Abs(rectTransform.anchoredPosition.y) / parentRectTransform.rect.height),
            max = new Vector2((rectTransform.anchoredPosition.x + rectTransform.rect.width) / parentRectTransform.rect.width, (Mathf.Abs(rectTransform.anchoredPosition.y) + rectTransform.rect.height) / parentRectTransform.rect.height);
            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;
            rectTransform.sizeDelta = rectTransform.anchoredPosition = Vector2.zero;
        }
        /// <summary>
        /// Revert previous state of rect transform.
        /// </summary>
        public void RevertAnchors()
        {
            RectTransform rectTransform = transform as RectTransform, parentRectTransform = transform.parent as RectTransform;
            //defaultRect = rectTransform.rect;
            rectTransform.pivot = defaultPivot;
            rectTransform.anchorMin = defaultAnchorMin;
            rectTransform.anchorMax = defaultAnchorMax;
            rectTransform.sizeDelta = defaultSize;
            rectTransform.anchoredPosition = defaultPosition;
        }
        #endregion Methods

        #region Unity
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            Destroy(this);
        }
        #endregion Unity
    }
}