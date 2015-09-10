using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;

public class ExportLevelController : MonoBehaviour {

	BinaryWriter writer;

	public void Export()
	{
		writer = new BinaryWriter(new FileStream("out.lvl", FileMode.Create));

		//Write all the data that needs to be saved

		writer.Close();
	}
}
