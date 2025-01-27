using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; set; }
    private TextMeshProUGUI uiScoreText;
    private Image uiCurrentHp;
    private GameObject uiMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        uiCurrentHp = GameObject.Find("HPBar").GetComponent<Image>();
        uiMenu = GameObject.Find("MenuContainer");
        uiScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        uiScoreText.text = "Score: 0";
        ShowMenu(false);

    }

    public void AddScore(int newScore) {
        uiScoreText.text = $"Score: {newScore.ToString()}";
    }

    public void UpdatePlayerHealth(float newCurrentHP, float maxHp) {
        float ratio  =  newCurrentHP / maxHp;
        uiCurrentHp.transform.localScale = new Vector3(ratio, 1,1);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowMenu(bool setActive) {
        uiMenu.gameObject.SetActive(setActive);
    }

}
