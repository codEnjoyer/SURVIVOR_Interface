using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class Selector: MonoBehaviour
{
	private static Selectable[] units; // массив всех юнитов, которых мы можем выделить
	private static List<Selectable> unitSelected; // массив выделенных юнитов

	[SerializeField] private GUISkin skin;
	private Rect rect;
	private bool draw;
	private Vector2 startPos;
	private Vector2 endPos;

	void Awake ()
	{
		units = FindObjectsOfType<Selectable>();
		unitSelected = new List<Selectable>();
	}
	
	void Select()
	{
		foreach (var unit in unitSelected)
			unit.OnSelected();
	}

	void Deselect()
	{
		foreach (var unit in unitSelected)
			unit.OnDeselected();
	}
	
	void OnGUI ()
	{
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

			foreach (var unit in units)
			{
				var tmp = WorldToScreenPoint(unit.transform.position);

				if(rect.Contains(tmp)) 
				{
					unitSelected.Add(unit);
				}
			}
		}
	}
	
	private Vector2 WorldToScreenPoint(Vector3 pos) =>
		new(Camera.main.WorldToScreenPoint(pos).x, Screen.height - Camera.main.WorldToScreenPoint(pos).y);
}