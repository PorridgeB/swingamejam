using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerPreview : MonoBehaviour
{
    [HideInInspector]
    public Spawner spawner;

    [SerializeField]
    private GameObject icons;

    private void Start()
    {
        // Remove placeholder icons
        foreach (Transform child in icons.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var spawnItem in spawner.spawnItems)
        {
            var spawnItemIcon = new GameObject("SpawnItemIcon");
            var iconImage = spawnItemIcon.AddComponent<Image>();
            iconImage.sprite = spawnItem.icon;
        }
    }
}
