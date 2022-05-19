using System.Text.RegularExpressions;
using UnityEngine;

namespace Client.Scripts.GameUi
{
    public class ObjectNameViewer : MonoBehaviour
    {
        [SerializeField] private string _text;
        [SerializeField] private int _textSize = 14;
        private Font _textFont;
        
        private readonly Color _textColor = Color.red;
        private readonly float _textHeight = 2.75f;
        private readonly bool isShowShadow = true;
        private readonly Color _shadowColor = new(0, 0, 0, 0.5f);
        private readonly Vector2 _shadowOffset = new(1, 1);
        private string _textShadow;

        private Camera _camera;

        private void Awake()
        {
            enabled = false;
            _camera = Camera.main;
            TextShadowReady();
        }

        private void TextShadowReady() => _textShadow = Regex.Replace(_text, "<color[^>]+>|</color>", string.Empty);

        private void OnGUI()
        {
            GUI.depth = 9999;

            GUIStyle style = new GUIStyle
            {
                fontSize = _textSize,
                richText = true
            };
            if (_textFont) style.font = _textFont;
            style.normal.textColor = _textColor;
            style.alignment = TextAnchor.MiddleCenter;

            var shadow = new GUIStyle();
            shadow.fontSize = _textSize;
            shadow.richText = true;
            if (_textFont) shadow.font = _textFont;
            shadow.normal.textColor = _shadowColor;
            shadow.alignment = TextAnchor.MiddleCenter;

            var worldPosition = new Vector3(transform.position.x, transform.position.y + _textHeight,
                transform.position.z);
            var screenPosition = _camera.WorldToScreenPoint(worldPosition);
            screenPosition.y = Screen.height - screenPosition.y;

            if (isShowShadow)
                GUI.Label(new Rect(screenPosition.x + _shadowOffset.x, screenPosition.y + _shadowOffset.y, 0, 0),
                    _textShadow, shadow);
            GUI.Label(new Rect(screenPosition.x, screenPosition.y, 0, 0), _text, style);
        }

        private void OnBecameVisible()
        {
            enabled = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }
    }
}