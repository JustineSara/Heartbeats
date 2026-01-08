using UnityEngine;
using Unity.VisualScripting;

public class UpdateVariable : MonoBehaviour
{
	private string VarToChange;
	
	public void VariableToChange(string var2ch)
	{
		VarToChange = var2ch;
	}

	public void ChangeVariableTo(bool newValue)
	{
		Debug.Log(VarToChange);
		Debug.Log(newValue);
		Variables.ActiveScene.Set(VarToChange, newValue);
		//GetComponent<SceneVariables>().set(VarToChange, newValue);
	}
}
