using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;

namespace RanterTools.UI
{
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class PasswordInputField : TMP_InputField
    {


        #region Parameters
        [SerializeField]
        Button switchButton;
        [SerializeField]
        Image starPrefab;
        [SerializeField]
        RectTransform stars;
        #endregion Parameters
        #region State
        List<Image> starsContainer = new List<Image>();
        bool visible = false;
        RectTransform starsMask;
        HorizontalLayoutGroup starsLayout;
        HorizontalLayoutGroup StarsLayout
        {
            get { return starsLayout = starsLayout ?? stars.GetComponent<HorizontalLayoutGroup>(); }
        }
        #endregion State


        #region Methods


        void Switch()
        {
            visible = !visible;
            if (visible)
            {
                Color c = textComponent.color;
                c.a = 1;
                textComponent.color = c;
                stars.gameObject.SetActive(false);
            }
            else
            {

                stars.gameObject.SetActive(true);
                Color c = textComponent.color;
                c.a = 0;
                textComponent.color = c;
                OnValueChanged(text);
            }
            ForceLabelUpdate();
        }

        void OnValueChanged(string text)
        {
            if (!visible && text.Length > 0)
            {
                var a = textComponent.GetRenderedValues(false);
                stars.anchoredPosition = new Vector2((textComponent.transform as RectTransform).anchoredPosition.x, stars.anchoredPosition.y);
                stars.sizeDelta = new Vector2(a.x, stars.sizeDelta.y);
            }
            if (text.Length < starsContainer.Count)
            {
                while (text.Length != starsContainer.Count)
                {
                    Destroy(starsContainer[starsContainer.Count - 1].gameObject);
                    starsContainer.RemoveAt(starsContainer.Count - 1);
                }
            }
            else
            {
                while (text.Length != starsContainer.Count)
                {
                    var star = Instantiate(starPrefab, stars);
                    star.gameObject.SetActive(true);
                    starsContainer.Add(star);
                }
            }

        }
        #endregion Methods

        #region Unity

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            visible = true;
            Switch();
        }


        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();
            starsMask = stars.parent as RectTransform;
            if (switchButton != null) switchButton.onClick.AddListener(Switch);
            onValueChanged.AddListener(OnValueChanged);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled or inactive.
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();
            if (switchButton != null) switchButton.onClick.RemoveListener(Switch);
            onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {

        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (!visible && text.Length > 0)
            {
                starsMask.anchorMax = textViewport.anchorMax;
                starsMask.anchorMin = textViewport.anchorMin;
                starsMask.anchoredPosition = textViewport.anchoredPosition;
                starsMask.sizeDelta = textViewport.sizeDelta;
                var a = textComponent.GetRenderedValues(false);
                stars.anchoredPosition = new Vector2((textComponent.transform as RectTransform).anchoredPosition.x, stars.anchoredPosition.y);
                stars.sizeDelta = new Vector2(a.x, stars.sizeDelta.y);
                StarsLayout.CalculateLayoutInputHorizontal();
            }
        }


        #endregion Unity
    }
}