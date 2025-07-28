using UnityEngine;
using TMPro;
using System.Collections;

public class TutoSignals : MonoBehaviour
{
    public static TutoSignals Instance { get; private set; }

    public GameObject movementJumpAttackHintPanel;
    public GameObject jumpHintPanel;
    public GameObject swordHintPanel;
    public GameObject firefliesHintPanel;
    public GameObject handLanternHintPanel;
    public GameObject cassettesHintPanel;

    public float hintPanelDuration = 5f;
    public float delayBetweenHints = 1f;

    public float handLanternSpecificDuration = 7f;

    private bool initialHintsSequenceStarted = false;

    private bool lanternHintShown = false;
    private bool cassettesHintShown = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        HideAllHintPanels();

        if (!initialHintsSequenceStarted)
        {
            StartCoroutine(StartInitialHintSequence());
            initialHintsSequenceStarted = true;
        }
    }

    void HideAllHintPanels()
    {
        movementJumpAttackHintPanel.SetActive(false);
        jumpHintPanel.SetActive(false);
        swordHintPanel.SetActive(false);
        firefliesHintPanel.SetActive(false);
        handLanternHintPanel.SetActive(false);
        cassettesHintPanel.SetActive(false);
    }

    IEnumerator StartInitialHintSequence()
    {
        movementJumpAttackHintPanel.SetActive(true);
        yield return new WaitForSeconds(hintPanelDuration);
        movementJumpAttackHintPanel.SetActive(false);
        yield return new WaitForSeconds(delayBetweenHints);

        jumpHintPanel.SetActive(true);
        yield return new WaitForSeconds(hintPanelDuration);
        jumpHintPanel.SetActive(false);
        yield return new WaitForSeconds(delayBetweenHints);

        firefliesHintPanel.SetActive(true);
        yield return new WaitForSeconds(hintPanelDuration);
        firefliesHintPanel.SetActive(false);
        yield return new WaitForSeconds(delayBetweenHints);

        swordHintPanel.SetActive(true);
        yield return new WaitForSeconds(hintPanelDuration);
        swordHintPanel.SetActive(false);
        yield return new WaitForSeconds(delayBetweenHints);
    }

    public void ShowFirefliesHint()
    {
        if (!firefliesHintPanel.activeSelf)
        {
            firefliesHintPanel.SetActive(true);
            StartCoroutine(HideAfterDelay(firefliesHintPanel, hintPanelDuration));
        }
    }

    public void ShowHandLanternHint()
    {
        if (!lanternHintShown)
        {
            handLanternHintPanel.SetActive(true);
            StartCoroutine(HideAfterDelay(handLanternHintPanel, handLanternSpecificDuration));
            lanternHintShown = true;
        }
    }

    public void ShowCassettesHint()
    {
        if (!cassettesHintShown)
        {
            cassettesHintPanel.SetActive(true);
            StartCoroutine(HideAfterDelay(cassettesHintPanel, hintPanelDuration * 2));
            cassettesHintShown = true;
        }
    }

    IEnumerator HideAfterDelay(GameObject hintPanel, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (hintPanel != null && hintPanel.activeSelf)
        {
            hintPanel.SetActive(false);
        }
    }
}
