using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GetFolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var x = Application.dataPath;
        var sceneFiles = Directory.GetFiles(x, "*.unity", SearchOption.AllDirectories);
        var stringList = new List<string>();
        foreach (var sceneFile in sceneFiles)
        {
            if (!File.Exists(sceneFile)) Debug.LogError("Файл не найден, путь: "+sceneFile);
            var sceneFileInfo = new FileInfo(sceneFile);
            if (sceneFileInfo.Extension != ".unity") continue;
            stringList.Add(File.ReadAllText(sceneFile));
        }

        var stringSum = stringList.Aggregate((s1, s2) => s1 + s2);
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(stringSum));
            var hashSum = BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
