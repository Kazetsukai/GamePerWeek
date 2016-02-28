using UnityEngine;
using System.Collections;
using System;

public class GameTile : MonoBehaviour {
    private bool _highlighted;
    private bool _selected;

    private Color _origColor;

    private Renderer _renderer;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        _origColor = _renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateColor();
    }

    public void SetHighlighted(bool highlighted)
    {
        _highlighted = highlighted;
    }

    public void SetSelected(bool selected)
    {
        _selected = selected;
    }

    private void UpdateColor()
    {
        if (_selected)
            _renderer.material.color = Color.Lerp(_origColor, Color.black, 0.3f - (Mathf.Sin(Time.realtimeSinceStartup * 6) / 5.0f));
        else if (_highlighted)
            _renderer.material.color = Color.Lerp(_origColor, Color.yellow, 0.5f);
        else
            _renderer.material.color = _origColor;
    }

}

public enum SelectState
{
    Unselected,
    Highlighted,
    Selected
}