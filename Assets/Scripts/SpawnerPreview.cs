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
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

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
            spawnItemIcon.transform.SetParent(icons.transform, false);

            var iconImage = spawnItemIcon.AddComponent<Image>();
            iconImage.sprite = spawnItem.icon;
        }
    }

    public void Show()
    {
        LeanTween.cancel(gameObject);

        LeanTween.alphaCanvas(canvasGroup, 1, 0.5f).setEaseOutExpo();
    }

    public void Hide()
    {
        LeanTween.cancel(gameObject);

        LeanTween.alphaCanvas(canvasGroup, 0, 0.5f).setEaseOutExpo();
    }
}
