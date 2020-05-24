using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
namespace RanterTools.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ScrollOverDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        #region Parameters
        [SerializeField]
        RectTransform parent;
        [SerializeField]
        [Range(0.01f, 1f)]
        [Tooltip("How long needed drag for events.")]
        float overDragValue = 0.1f;
        [SerializeField]
        [Tooltip("Scroll rect if used")]
        ScrollRect scrollRect;
        [SerializeField]
        [Tooltip("Used another scroll if true.")]
        bool scroll = false;
        [SerializeField]
        [Range(0.1f, 10f)]
        [Tooltip("Theresold for scroll drag end.")]
        float theresold;
        #endregion Parameters
        #region Events
        public OverDragEvent OnBeginOverDrag = new OverDragEvent();
        public OverDragEvent OnOverDrag = new OverDragEvent();
        public OverDragEvent OnEndOverDrag = new OverDragEvent();
        #endregion Events
        #region State
        RectTransform RectTransform;
        bool dragged = false;
        bool firstOverDrag = true;
        bool endOverDrag = false;

        #endregion State
        #region Methods
        public void OnBeginDrag(PointerEventData pointer)
        {
            dragged = true;
        }
        public void OnDrag(PointerEventData pointer)
        {
            if (dragged)
            {
                OnScroll(pointer.delta);
            }
        }
        public void OnEndDrag(PointerEventData pointer)
        {
            dragged = false;
            firstOverDrag = false;
        }

        void OnScroll(Vector2 delta)
        {

            if (!scroll) RectTransform.anchoredPosition += new Vector2(0, delta.y);
            var min = Mathf.Min(parent.rect.height, RectTransform.rect.height * overDragValue);
            if (RectTransform.anchoredPosition.y < -0.1f)
            {
                if (endOverDrag) return;
                var value = Mathf.Clamp01(Mathf.Abs(RectTransform.anchoredPosition.y) / min);
                if (firstOverDrag)
                {
                    firstOverDrag = false;
                    OnBeginOverDrag?.Invoke(false, value);
                }
                if (value >= 1)
                {
                    endOverDrag = true;
                    value = 0;
                    OnEndOverDrag?.Invoke(true, value);
                }
                else
                    OnOverDrag?.Invoke(true, value);
            }
            else
            {
                float pos = 0;
                if (RectTransform.rect.height > parent.rect.height) pos = RectTransform.rect.height - parent.rect.height;
                if (RectTransform.anchoredPosition.y - pos > 0.1f)
                {
                    if (endOverDrag) return;
                    var value = Mathf.Clamp01((RectTransform.anchoredPosition.y - pos) / min);
                    if (firstOverDrag)
                    {
                        firstOverDrag = false;
                        OnBeginOverDrag?.Invoke(false, value);
                    }

                    if (value >= 1)
                    {
                        endOverDrag = true;
                        value = 0;
                        OnEndOverDrag?.Invoke(false, value);
                    }
                    else
                        OnOverDrag?.Invoke(false, value);
                }
                else
                {
                    firstOverDrag = true;
                    endOverDrag = false;
                }
            }

        }
        #endregion Methods
        #region Unity
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            if (parent == null)
            {
                parent = transform.parent as RectTransform;
            }
            if (scrollRect == null)
            {
                scrollRect = GetComponentInParent<ScrollRect>();
            }
            if (scrollRect == null) scroll = false;
            else
            {
                scrollRect.onValueChanged.AddListener(OnScroll);
                scroll = true;
            }
            RectTransform = transform as RectTransform;
        }
        #endregion Unity
    }

    [Serializable]
    public class OverDragEvent : UnityEvent<bool, float>
    {

    }

}
