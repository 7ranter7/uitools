using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RanterTools.UI;
namespace RanterTools.UI.Editor
{

    [CustomEditor(typeof(AnchorsFromCurrentScreen))]
    public class AnchorsFromCurrentScreenEditor : UnityEditor.Editor
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
        AnchorsFromCurrentScreen instance;
        #endregion State

        #region Methods

        #endregion Methods

        #region Unity
        public override void OnInspectorGUI()
        {
            instance = target as AnchorsFromCurrentScreen;
            if (GUILayout.Button("Apply anchors"))
            {
                instance.ApplyAnchors();
            }
            if (GUILayout.Button("Revert anchors"))
            {
                instance.RevertAnchors();
            }
        }
        #endregion Unity
    }
}