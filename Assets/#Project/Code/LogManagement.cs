using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class LogManagement : MonoBehaviour
{
	private List<string> logs = new();
	public string path;
	

	public void AddToLogs(string s){
		logs.Add(s);
		Debug.Log(s);
	}
	
	public void SaveLogs(){
		
		string logfile = Path.Combine(Directory.GetCurrentDirectory(), path);
		
		if (File.Exists(logfile)) {
			File.AppendAllLines(logfile, logs);
		} else {
			File.WriteAllLines(logfile, logs);
		}
		logs.Clear();
	}
}
