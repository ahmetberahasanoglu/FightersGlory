using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject fightScreen;
    public GameObject gymScreen;
    public GameObject shopScreen;
    public GameObject profileScreen;
    public GameObject achievementsScreen;

    public Button fightButton;
    public Button gymButton;
    public Button shopButton;
    public Button profileButton;
    public Button achievementsButton;

    public TextMeshProUGUI diamondText;
    public TextMeshProUGUI goldText;   
    public TextMeshProUGUI ageText;    

    // Oyuncu verileri
    private int diamonds = 100; 
    private int gold = 500;     
    private float age = 18.0f;  

    public float panelTransitionDuration = 0.5f; 
    public Ease panelTransitionEase = Ease.OutQuad; 

    private GameObject currentScreen; 

    void Start()
    {
        ShowMainMenu();
        UpdateUI();
        fightButton.onClick.AddListener(() => ShowScreen(fightScreen));
        gymButton.onClick.AddListener(() => ShowScreen(gymScreen));
        shopButton.onClick.AddListener(() => ShowScreen(shopScreen));
        profileButton.onClick.AddListener(() => ShowScreen(profileScreen));
        achievementsButton.onClick.AddListener(() => ShowScreen(achievementsScreen));
    }
    public void AnimateText(TextMeshProUGUI text)
    {
        text.transform.DOScale(1.2f, 0.2f) 
            .OnComplete(() => text.transform.DOScale(1f, 0.2f));
    }

    public void UpdateUI()
    {
        AnimateText(diamondText);
        AnimateText(goldText);
        AnimateText(ageText);

        diamondText.text = diamonds.ToString();
        goldText.text = gold.ToString();
        ageText.text = age.ToString("F1"); 
    }


    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        UpdateUI();
    }


    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

  
    public void IncreaseAge()
    {
        age += 0.5f;
        UpdateUI();
    }

    public void OnFightEnd(bool isWin)
    {
        if (isWin)
        {
            AddGold(100); 
            AddDiamonds(10);
        }
        else
        {
            AddGold(50); 
        }
        IncreaseAge(); 
    }
    public void ShowMainMenu()
    {
       
        SetActiveScreen(mainMenu);
        AnimateButtons(); 
    }

    public void ShowScreen(GameObject screen)
    {
        if (currentScreen == screen) return; 

        // Mevcut ekraný kapat
        if (currentScreen != null)
        {
            CloseScreen(currentScreen);
        }

        // Yeni ekraný aç
        OpenScreen(screen);
    }

    private void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);

      
        RectTransform rectTransform = screen.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(2000, 0, 0); 

      
        rectTransform.DOLocalMoveX(0, panelTransitionDuration)
            .SetEase(panelTransitionEase)
            .OnComplete(() => Debug.Log(screen.name + " açýldý"));

        currentScreen = screen; 
        mainMenu.SetActive(false);
    }

    private void CloseScreen(GameObject screen)
    {
        RectTransform rectTransform = screen.GetComponent<RectTransform>();

        rectTransform.DOLocalMoveX(-2000, panelTransitionDuration)
            .SetEase(panelTransitionEase)
            .OnComplete(() =>
            {
                screen.SetActive(false);
                Debug.Log(screen.name + " kapandý");
            });
    }

    private void SetActiveScreen(GameObject activeScreen)
    {
        fightScreen.SetActive(false);
        gymScreen.SetActive(false);
        shopScreen.SetActive(false);
        profileScreen.SetActive(false);
        achievementsScreen.SetActive(false);

        activeScreen.SetActive(true);
    }

    private void AnimateButtons()
    {
        float delay = 0.1f;
        fightButton.transform.localScale = Vector3.zero;
        gymButton.transform.localScale = Vector3.zero;
        shopButton.transform.localScale = Vector3.zero;
        profileButton.transform.localScale = Vector3.zero;
        achievementsButton.transform.localScale = Vector3.zero;

        fightButton.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay * 1);
        gymButton.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay * 2);
        shopButton.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay * 3);
        profileButton.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay * 4);
        achievementsButton.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(delay * 5);
    }
}