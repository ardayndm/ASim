using System.Collections.Generic;
using UnityEngine;

public class SchemaManager : MonoBehaviour
{
    public static List<GameObject> SchemaObjects = new();

    public static SchemaManager Instance { get; private set; }

    public void AddSchemaObject(GameObject obj) => SchemaObjects.Add(obj);

    private void Awake() => Instance = this;

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