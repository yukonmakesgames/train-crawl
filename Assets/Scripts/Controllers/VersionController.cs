using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionController : MonoBehaviour
{
	[SerializeField]
	private string versionPrefex;
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	private void Awake()
	{
		gameObject.GetComponent<Text>().text = versionPrefex + Application.version;
	}
}
