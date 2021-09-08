using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int currentLevel;
    private int unlockedLevel = 1;
    public Image selectedCharacterImage;
    public Image selectedAchievementImage;
    public Button endlessButton;
    public Button nextLevelButton;
    public Button character1Button;
    public Button character2Button;
    public Button character3Button;
    public Button character4Button;
    public Button character5Button;
    public Button character6Button;
    public Button character7Button;
    public Button character8Button;
    public Button achievement1Button;
    public Button achievement2Button;
    public Button achievement3Button;
    public Button achievement4Button;
    public Button achievement5Button;
    public Button achievement6Button;
    public Button achievement7Button;
    public Button achievement8Button;
    public Button previousLevelButton;
    public Slider loadingSlider;
    private string lockedText = "LOCKED";
    private string[] characterTextList = new string[8];
    private string[] achievementTextList = new string[8];
    private Button[] characterButtonList = new Button[8];
    private Button[] achievementButtonList = new Button[8];
    private ColorBlock enabledColourBlock = new ColorBlock()
    {
        normalColor = new Color(1, 1, 1, 0.75f),
        fadeDuration = 0.1f,
        pressedColor = new Color(1, 1, 1, 1),
        selectedColor = new Color(1, 1, 1, 1),
        colorMultiplier = 1,
        highlightedColor = new Color(1, 1, 1, 1)
    };
    private ColorBlock disabledColourBlock = new ColorBlock()
    {
        normalColor = new Color(0, 0, 0, 0.25f),
        fadeDuration = 0.1f,
        pressedColor = new Color(0, 0, 0, 0.5f),
        selectedColor = new Color(0, 0, 0, 0.5f),
        colorMultiplier = 1,
        highlightedColor = new Color(0, 0, 0, 0.5f)
    };
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI selectedCharacterText;
    public TextMeshProUGUI selectedAchievementText;

    public void Start()
    {
        PlayerPrefs.SetInt("unlockedCollie", 1);
        PlayerPrefs.SetInt("unlockedPatrick", 1);
        if (PlayerPrefs.HasKey("unlockedTina"))
        {
            unlockedLevel++;
        }
        if (PlayerPrefs.HasKey("unlockedGary"))
        {
            unlockedLevel++;
        }
        if (PlayerPrefs.HasKey("unlockedSteve"))
        {
            unlockedLevel++;
        }
        if (PlayerPrefs.HasKey("unlockedTommy"))
        {
            unlockedLevel++;
        }
        if (PlayerPrefs.HasKey("unlockedIda"))
        {
            unlockedLevel++;
        }
        currentLevel = unlockedLevel;
        UpdateLevelGameObjects();
        if (PlayerPrefs.HasKey("unlockedCabbitsu"))
        {
            endlessButton.interactable = true;
        }
        InitializeCharacter(0, character1Button, PlayerPrefs.HasKey("unlockedCollie"), "Broski, Brogle, Bro Collie goes by many names, but doesn't matter which you pick, he'll come to your beck and call. Big hair, big vibes and biiig flatulence are just some of the words used to describe your funky, floral, fibrous friend.");
        InitializeCharacter(1, character2Button, PlayerPrefs.HasKey("unlockedTina"), "Tina turnip turns UP. This proactive, purple pisces is the heart of the party. Always willing to lend a hand or an ear, you'll find it's her words of encouragement that push you to make the right decision every time. She's simply the best!");
        InitializeCharacter(2, character3Button, PlayerPrefs.HasKey("unlockedGary"), "Pee-ew, who invited Gary Garlic? Why, you did of course, because of his amazing health benefits! Just a shame about the smell... Best to avoid any face to face conversations for a while. And to think, all Gary really wanted was a hug.");
        InitializeCharacter(3, character4Button, PlayerPrefs.HasKey("unlockedTommy"), "Tommy's therapist keeps telling him, if you keep everything bottled up inside, one day you'll explode! And judging by how quiet he's been lately, and how red he's getting in the face... You know what? I'm just going to take a step back...");
        InitializeCharacter(4, character5Button, PlayerPrefs.HasKey("unlockedCabbitsu"), "Cabbitsu, cabbitsu, cab-bit-su. LETTUSU, LETTUSU, LET-TU-SUHHH! Cabbitsu is the official Broccoli vs Burgers mascot. They will guide you through the game, celebrate your successes and acknowledge your failures. Make them proud!");
        InitializeCharacter(5, character6Button, PlayerPrefs.HasKey("unlockedPatrick"), "Tantalise your tastebuds with the mouthwatering perfection that is Patrick. He's thicc, he's juicy, he's large and in charge. Serving you body-ody-ody with a side of fries. He'll probably leave you wanting less, but it's too late now. Oh lard he comin!");
        InitializeCharacter(6, character7Button, PlayerPrefs.HasKey("unlockedSteve"), "STEVE!");
        InitializeCharacter(7, character8Button, PlayerPrefs.HasKey("unlockedIda"), "Ice cold Ida they call her, and with good reason... To the world it would seem like her heart is frozen. But when it's just her on the couch in front of a soppy romcom, she eats her feelings and thinks about the cone that got away.");
        InitializeAchievement(0, achievement1Button, PlayerPrefs.HasKey("earnedGettingYourGreens"), "GETTING YOUR GREENS\nUnlock all healthy food characters");
        InitializeAchievement(1, achievement2Button, PlayerPrefs.HasKey("earnedTheDevilsYouKnow"), "THE DEVILS YOU KNOW\nUnlock all junk food characters");
        InitializeAchievement(2, achievement3Button, PlayerPrefs.HasKey("earnedHowManyAreThere"), "HOW MANY ARE THERE?\nUnlock endless mode");
        InitializeAchievement(3, achievement4Button, PlayerPrefs.HasKey("earnedOneForTheMoney"), "ONE FOR THE MONEY\nComplete one level in endless mode");
        InitializeAchievement(4, achievement5Button, PlayerPrefs.HasKey("earnedTwoForTheShow"), "TWO FOR THE SHOW\nComplete two levels in endless mode");
        InitializeAchievement(5, achievement6Button, PlayerPrefs.HasKey("earnedThreeToGetReady"), "THREE TO GET READY\nComplete three levels in endless mode");
        InitializeAchievement(6, achievement7Button, PlayerPrefs.HasKey("earnedGoCatGo"), "GO, CAT, GO!\nComplete nine levels in endless mode");
        InitializeAchievement(7, achievement8Button, PlayerPrefs.HasKey("earnedLaTomatina"), "LA TOMATINA\nComplete a level using only turnips and tomatoes");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        currentLevel++;
        UpdateLevelGameObjects();
    }

    public void PlayLevel()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        StartCoroutine(LoadScene(1));
    }

    public void PlayEndless()
    {
        PlayerPrefs.SetInt("currentLevel", -1);
        StartCoroutine(LoadScene(1));
    }

    public void PreviousLevel()
    {
        currentLevel--;
        UpdateLevelGameObjects();
    }

    public void SelectCharacter(int index)
    {
        Button button = characterButtonList[index];
        button.Select();
        selectedCharacterText.text = characterTextList[index];
        selectedCharacterImage.color = button.colors.highlightedColor;
        selectedCharacterImage.sprite = button.GetComponentsInChildren<Image>()[1].sprite;
    }

    public void SelectAchievement(int index)
    {
        Button button = achievementButtonList[index];
        button.Select();
        selectedAchievementText.text = achievementTextList[index];
        selectedAchievementImage.color = button.colors.highlightedColor;
        selectedAchievementImage.sprite = button.GetComponentsInChildren<Image>()[1].sprite;
    }

    private void InitializeCharacter(int index, Button button, bool unlocked, string text)
    {
        button.colors = unlocked ? enabledColourBlock : disabledColourBlock;
        characterTextList[index] = unlocked ? text : lockedText;
        characterButtonList[index] = button;
    }

    private void InitializeAchievement(int index, Button button, bool unlocked, string text)
    {
        button.colors = unlocked ? enabledColourBlock : disabledColourBlock;
        achievementTextList[index] = text;
        achievementButtonList[index] = button;
    }

    public void UpdateLevelGameObjects()
    {
        currentLevelText.text = "LEVEL " + currentLevel;
        nextLevelButton.interactable = currentLevel != unlockedLevel;
        previousLevelButton.interactable = currentLevel != 1;
    }

    private IEnumerator LoadScene(int sceneBuildIndex)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            if (asyncOperation.progress == 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
