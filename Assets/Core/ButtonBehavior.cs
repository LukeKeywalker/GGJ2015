using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonBehavior : MonoBehaviour
{
	public bool m_useAnalytics = true;

	public string m_analyticsLabel;

	public bool m_greyOutOnPress = true;
	

	public object UserData
	{
		get; set;
	}

	private UIWidget m_widget;

	private Dictionary<UIWidget, Color> m_originalColors = new Dictionary<UIWidget, Color>();

	private bool m_enabled = true;
	
	void Start()
	{
		if (this.GetComponent<UIButton>() != null) //UI button handles color change on press
			m_greyOutOnPress = false;

		InitializeColors ();
	}
	
	void Update()
	{
	}

	public void SetEnabled(bool value)
	{
		m_enabled = value;
	}
	
	public void OnPress(bool isPressed)
	{
		if (m_enabled)
		{
			if (m_greyOutOnPress)
				SetFade (isPressed);
			if (isPressed)
			{				
				if (Pressed != null)
					Pressed(this);
			}
			else
			{
				if (Released != null)
					Released(this);
			}
		}
	}

	private void SetFade(bool value)
	{
		float shadeOfGray = 0.5f;
		foreach (UIWidget child in this.GetComponentsInChildren<UIWidget>(true))
			if (value)
				child.color = new Color (shadeOfGray, shadeOfGray, shadeOfGray);
			else
				child.color = m_originalColors[child];
	}

	private void InitializeColors()
	{
		foreach (UIWidget child in this.GetComponentsInChildren<UIWidget>(true))
			m_originalColors.Add(child, child.color);
	}
	
	public void OnClick()
	{
		if (m_enabled)
		{
			if (Clicked != null)
				Clicked(this);
		}
	}
	
	public event System.Action<object> Clicked;
    public event System.Action<object> Pressed;
    public event System.Action<object> Released;
}
