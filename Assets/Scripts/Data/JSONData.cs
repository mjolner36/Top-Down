using System.IO;
using UnityEngine;

public static class JSONData
{
   public static void JsonSerialization<T>(T data, string path)
   {
      string jsonData = JsonUtility.ToJson(data, true);
      File.WriteAllText(Application.persistentDataPath + "/" + path, jsonData);
   }

   public static T JSONDeserialization<T>(string path) where T : new()
   {
      if (!File.Exists(Application.persistentDataPath + "/" + path)) return new T();
      string jsonData = File.ReadAllText(Application.persistentDataPath + "/" + path);
      T data = JsonUtility.FromJson<T>(jsonData);
      return data;
   }
}
