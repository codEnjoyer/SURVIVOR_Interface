using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;


public class Selector: MonoBehaviour
{
	[SerializeField] private GUISkin skin;

	[HideInInspector] public static Selector instance;

	private bool isActive = true;
	private static Selectable[] units; // массив всех юнитов, которых мы можем выделить
	private static List<Selectable> unitSelected; // массив выделенных юнитов
	private Rect rect;
	private bool draw;
	private Vector2 startPos;
	private Vector2 endPos;

	private void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			units = FindObjectsOfType<Selectable>();
			unitSelected = new List<Selectable>();
		}
		else if (instance == this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	
	private void Select()
	{
		foreach (var unit in unitSelected)
			unit.Select();
	}

	private void Deselect()
	{
		foreach (var unit in unitSelected)
			unit.Deselect();
	}
	
	private void OnGUI ()
	{
		if (!isActive)
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

	public void Activate() => isActive = true;

	public void Deactivate() => isActive = false;

}