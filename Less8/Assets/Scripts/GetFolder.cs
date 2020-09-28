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
        var s1 = "ab";

        using (var md5 = MD5.Create())
        {
            var checksum = md5.ComputeHash(Encoding.ASCII.GetBytes(s1));
            var output = BitConverter.ToString(checksum).Replace("-","").ToLower();
        }

        var x = Application.dataPath;
        var sceneFiles = Directory.GetFiles(x, "*.unity", SearchOption.AllDirectories);
        var byteList = new List<byte[]>();
        foreach (var sceneFile in sceneFiles)
        {
            if (!File.Exists(sceneFile)) Debug.LogError("Файл не найден, путь: "+sceneFile);
            var sceneFileInfo = new FileInfo(sceneFile);
            if (sceneFileInfo.Extension != ".unity") continue;
            byteList.Add(File.ReadAllBytes(sceneFile));
        }

        var byteSum = byteList.Aggregate((b1, b2) => b1.Concat(b2).ToArray());
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(byteSum);
            var hashSum = BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
