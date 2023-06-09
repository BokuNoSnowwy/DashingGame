
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    protected GameManager gameManager;

    [SerializeField] protected TextMeshProUGUI textTitle;
    [SerializeField] protected Image[] objectivesArray = new Image[3];
    [SerializeField] protected TextMeshProUGUI[] textObjectivesArray = new TextMeshProUGUI[3];

    [SerializeField] protected Sprite spriteObjectiveNotAchieved; 
    [SerializeField] protected Sprite spriteObjectiveDone;

    [SerializeField] private Button launchLevelButton;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    // Start is called before the first frame update
    private void Start()
    {
        launchLevelButton.onClick.AddListener(gameManager.LaunchLevel);
    }

    

    // Setup level informations
    public virtual void SetupPanelForLevel(int index)
    {
        gameObject.SetActive(true);

        textTitle.text = "Level " + (index + 1);
        //TODO Create sentences depending of the objectives
        //TODO Update the stars if objectives are done

        List<Objective> listObjectives = gameManager.GetActualLevel().GetObjectives();

        for (int i = 0; i < listObjectives.Count; i++)
        {
            objectivesArray[i].sprite = listObjectives[i].ObjectiveDone ? spriteObjectiveDone : spriteObjectiveNotAchieved;
            textObjectivesArray[i].text = listObjectives[i].ObjectiveString;
        }
    }
}
