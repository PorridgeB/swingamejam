using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private InventoryItem[] items;
    private int selectedItemIndex;

    private void OnEnable()
    {
        items = Resources.LoadAll<InventoryItem>("InventoryItems");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying)
        {
            return;
        }

        var inventory = target as Inventory;

        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        selectedItemIndex = EditorGUILayout.Popup(selectedItemIndex, items.Select(x => x.name).ToArray());

        var selectedItem = items[selectedItemIndex];

        if (GUILayout.Button("Add"))
        {
            inventory.AddItem(selectedItem);
        }

        if (GUILayout.Button("Remove"))
        {
            inventory.RemoveItem(selectedItem);
        }

        GUILayout.EndHorizontal();
    }
}
