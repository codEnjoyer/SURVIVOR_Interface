using System;
using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class Selector: MonoBehaviour
{
	private static Selectable[] units; 
	private static List<Selectable> unitSelected;

	public static Selector Instance { get; private set; }

	[SerializeField] private GUISkin skin;

	private bool isActivate = true;
	private Rect rect;
	private bool draw;
	private Vector2 startPos;
	private Vector2 endPos;

	private void Awake ()
	{
		if (Instance == null)
		{
			Instance = this;
			units = FindObjectsOfType<Selectable>();
			unitSelected = new List<Selectable>();
		}
		else if (Instance == this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public void Activate() => isActivate = true;
	public void DeActivate() => isActivate = false;
	
	private void Select()
	{
		foreach (var unit in unitSelected)
			unit.Selected();
	}

	private void Deselect()
	{
		foreach (var unit in unitSelected)
			unit.Deselected();
	}
	
	private void OnGUI ()
	{
		if (!isActivate)
			return;
		
		GUI.skin = skin;
		GUI.depth = 99;
		
		if(Input.GetMouseButtonDown(0))
		{
			Deselect();
			startPos = Input.mousePosition;
			draw = true;
		}

		if (Input.GetMouseButtonUp(0)) 
		{
			draw = false;
			Select();
		}
		
		if(draw)
			DrawRectangleAndSelectUnits();
	}


	private void DrawRectangleAndSelectUnits()
	{
		unitSelected.Clear();
		endPos = Input.mousePosition;
		if(startPos == endPos) return;

		rect = new Rect(Mathf.Min(endPos.x, startPos.x),
			Screen.height - Mathf.Max(endPos.y, startPos.y),
			Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
			Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
		);
			
		GUI.Box(rect, "");
		SelectUnitsInRectangle(rect);
	}

	private void SelectUnitsInRectangle(Rect rect)
	{
		foreach (var unit in units)
		{
			var pos = unit.transform.position;
			var tmp = new Vector2(Camera.main.WorldToScreenPoint(pos).x,
				Screen.height - Camera.main.WorldToScreenPoint(pos).y);
			if(rect.Contains(tmp))
				unitSelected.Add(unit);
		}
	}
	
}