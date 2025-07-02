using System.Collections.Generic;
using UnityEngine;

public class SchemaManager : MonoBehaviour
{
    public static List<GameObject> SchemaObjects = new();

    public void AddSchemaObject(GameObject obj) => SchemaObjects.Add(obj);

    /// <summary>
    /// Tüm şemada bulunan öğeleri siler.
    /// </summary>
    public void ClearAllSchema()
    {
        for (int i = SchemaObjects.Count - 1; i >= 0; i--)
        {
            if (SchemaObjects[i].transform != null)
                Destroy(SchemaObjects[i]);

            SchemaObjects.RemoveAt(i);
        }
    }
}