using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileController : MonoBehaviour {

	// CSV file
	public TextAsset file;

	Dictionary<int,List<string[]>> dict = new Dictionary<int,List<string[]>> ();
	string[] headers;

	// Use this for initialization
	void Start () {
		_parseCSV (file);
		Debug.Log (dict.Keys.Count);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Parse CSV file
	void _parseCSV (TextAsset file) {
		if (file!=null) {
			string[] records = file.text.Trim().Split ('\n');
			// column headers
			headers = records[0].Split (',');
			// records
			for (var i = 1; i < records.Length; i ++) {
				var values = records [i].Split (',');
				// data of a single line
				string[] o = new string[headers.Length];
				for (var j = 0; j < headers.Length; j ++) {
					o[j] = values[j];
				}
				_addToDic (o);
			}
		} else {
			Debug.Log ("can't find file");
		}
	}

	// Add data to dictionary with id as the key
	void _addToDic (string[] o) {
		int id = 0;
		int.TryParse (o [_getIndex("id")].Substring(0,2), out id);
		if (!dict.ContainsKey(id)) {
			List<string[]> data = new List<string[]>();
			dict[id] = data;
		}
		dict[id].Add(o);
	}

	// Locate the index of colunm that matches the given "name"
	int _getIndex (string name) {
		for (int i = 0; i < headers.Length; i++) {
			if (name.Equals(headers[i])) {
				return i;
			}
		}
		return 0;
	}
}
