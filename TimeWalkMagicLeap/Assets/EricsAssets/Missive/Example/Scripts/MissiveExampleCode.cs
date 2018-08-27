using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissiveExampleCode : MonoBehaviour
{
	public InputField exampleIntInputField;
	public InputField exampleFloatInputField;
	public Toggle exampleBoolInputToggle;
	public InputField exampleStringInputField;
	public Text exampleOutputText;

	public void SendExampleMissive()
	{
		int tempInt;
		float tempFloat;

		ExampleMissive missive = new ExampleMissive
		{
			missiveInt = int.TryParse(exampleIntInputField.text, out tempInt) ? tempInt : 0,
			missiveFloat = float.TryParse(exampleFloatInputField.text, out tempFloat) ? tempFloat : 0f,
			missiveBool = exampleBoolInputToggle.isOn,
			missiveString = exampleStringInputField.text
		};
		Missive.Send(missive);

		Missive.Send("NamedTypelessTest");

		Missive.Send("NamedTypedTest", new ExampleMissive { missiveInt = 1, missiveFloat = 1f, missiveBool = true, missiveString = "named typed missive"} );
	}

	private void OnEnable()
	{
		Missive.AddListener<ExampleMissive>(OnMissiveTypeReceived);
		Missive.AddListener("NamedTypelessTest", OnMissiveTypelessReceived);
		Missive.AddListener<ExampleMissive>("NamedTypedTest", OnMissiveNamedTypeReceived);
	}

	private void OnMissiveTypeReceived(ExampleMissive exampleMissive)
	{
		string tempString = string.Format("Typed Missive received! Data: int = {0}, float = {1:F10}, bool = {2}, string = {3}", exampleMissive.missiveInt, exampleMissive.missiveFloat, exampleMissive.missiveBool, exampleMissive.missiveString);
		Debug.Log(tempString);
		exampleOutputText.text = tempString;
	}

	private void OnMissiveTypelessReceived()
	{
		Debug.Log("Named Typeless Missive received!");
	}

	private void OnMissiveNamedTypeReceived(ExampleMissive exampleMissive)
	{
		string tempString = string.Format("Named Typed Missive received! Data: int = {0}, float = {1:F10}, bool = {2}, string = {3}", exampleMissive.missiveInt, exampleMissive.missiveFloat, exampleMissive.missiveBool, exampleMissive.missiveString);
		Debug.Log(tempString);
	}

	private void OnDisable()
	{
		Missive.RemoveListener<ExampleMissive>(OnMissiveTypeReceived);
		Missive.RemoveListener("NoTypeTest", OnMissiveTypelessReceived);
		Missive.RemoveListener<ExampleMissive>("NamedTypedTest", OnMissiveNamedTypeReceived);
	}
}

public class ExampleMissive : Missive
{
	public int missiveInt;
	public float missiveFloat;
	public bool missiveBool;
	public string missiveString;
}
