using TMPro;
using UnityEngine;

public class MemoryFragmentCounter : MonoBehaviour
{
    public TextMeshProUGUI fragmentText;
    private int fragmentsCollected = 0;
    private int maxFragments = 3;

    void Start()
    {
        UpdateUI();
    }

    public void AddFragment()
    {
        if (fragmentsCollected < maxFragments)
        {
            fragmentsCollected++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        fragmentText.text = $"Fragmentos de memoria recolectados: {fragmentsCollected}/{maxFragments}";
    }
}
