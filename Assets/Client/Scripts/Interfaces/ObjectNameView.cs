using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectNameView : MonoBehaviour
{
    [SerializeField ]private string _text;
    [SerializeField] private int _textSize = 14;
    private Font _textFont;
    private readonly Color _textColor = Color.red;
    private float _textHeight = 2.75f;
    private bool isShowShadow = true;
    private readonly Color _shadowColor = new Color(0, 0, 0, 0.5f);
    private readonly Vector2 _shadowOffset = new Vector2(1,1);
    private string _textShadow;
    
    void Awake()
    {
        enabled = false;
        TextShadowReady();
    }

    void TextShadowReady()
    {
        _textShadow = Regex.Replace(_text, "<color[^>]+>|</color>", string.Empty);
    }

    void OnGUI()
    {
        GUI.depth = 9999;

        GUIStyle style = new GUIStyle();
        style.fontSize = _textSize;
        style.richText = true;
        if(_textFont) style.font = _textFont;
        style.normal.textColor = _textColor;
        style.alignment = TextAnchor.MiddleCenter;

        GUIStyle shadow = new GUIStyle();
        shadow.fontSize = _textSize;
        shadow.richText = true;
        if(_textFont) shadow.font = _textFont;
        shadow.normal.textColor = _shadowColor;
        shadow.alignment = TextAnchor.MiddleCenter;

        Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y + _textHeight, transform.position.z);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition.y = Screen.height - screenPosition.y;

        if(isShowShadow) GUI.Label(new Rect (screenPosition.x + _shadowOffset.x, screenPosition.y + _shadowOffset.y, 0, 0), _textShadow, shadow);
        GUI.Label(new Rect (screenPosition.x, screenPosition.y, 0, 0), _text, style);
    }

    void OnBecameVisible() 
    {
        enabled = true;
    }
	
    void OnBecameInvisible() 
    {
        enabled = false;
    }
}
