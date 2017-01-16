using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour 
{
	public delegate void SceneControllerEvents ();
	public static event SceneControllerEvents OnAnyCellSelected;

	/// <summary>
	/// Object-container for content of scrollView in scene. You must manually set it in Inspector.
	/// </summary>
	[SerializeField]
	private Transform scrollViewContentObject;

	private List <ScrollViewCell> cells;
	private static ScrollViewCell currentSelectedCell;

	public static void AnimateCurrentActiveModel (bool animate) 
	{
		if (currentSelectedCell != null)
			currentSelectedCell.model.AnimateModel (animate);
	}

	void Start () 
	{
		cells = new List<ScrollViewCell> ();
		FillScrollViewContent ();
	}

	void OnDestroy ()
	{
		foreach (var cell in cells)
		{
			cell.OnCellSelected -= OnCellClickedHandler;
		}
		currentSelectedCell = null;
	}

	void FillScrollViewContent ()
	{
		if (scrollViewContentObject != null)
		{
			foreach (Transform child in scrollViewContentObject)
			{
				var cell = child.GetComponent <ScrollViewCell> ();
				if (cell != null)
				{
					cell.OnCellSelected += OnCellClickedHandler;
					cells.Add (cell);
				}
			}
		}
	}

	void OnCellClickedHandler (ScrollViewCell scrollViewCell)
	{
		DeselectCellsExcept (scrollViewCell);
	}

	void DeselectCellsExcept (ScrollViewCell scrollViewCell)
	{
		if (scrollViewCell != currentSelectedCell)
		{	
			foreach (var cell in cells)
			{
				if (cell != scrollViewCell)
					cell.DeselectCell ();
				else
				{
					currentSelectedCell = cell;

					if (OnAnyCellSelected != null)
						OnAnyCellSelected ();
				}
			}
		}
	}
}
