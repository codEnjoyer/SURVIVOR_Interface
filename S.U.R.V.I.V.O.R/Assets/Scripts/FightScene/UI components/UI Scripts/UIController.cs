using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private GameObject queueCardPrefab;
    [SerializeField] private GameObject groupCharacterCardPrefab;
    [SerializeField] private GameObject queuePanel;
    [SerializeField] private GameObject groupPanel;
    [SerializeField] private new Camera camera;
    private Queue<GameObject> cardsQueue = new Queue<GameObject>();
    private List<GameObject> groupCards = new List<GameObject>();

    //private const int MaxDrawCardsCount = 5;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject); 
        }
    }

    public void CreateUI()
    {
        CreateGroupCharactersCards();
        CreateQueueCards();
    }

    private void CreateGroupCharactersCards()
    {
        foreach (var fCharacter in FightSceneController.Instance.Characters
                     .Select(obj => obj.GetComponent<FightCharacter>())
                     .Where(fCharacter => fCharacter.Type == CharacterType.Ally))
        {
            var initPos = groupPanel.transform.position + 
                          new Vector3(0, (1.5f - groupCards.Count) 
                                         * groupCharacterCardPrefab.GetComponent<RectTransform>().rect.height, 0);
            var card = Instantiate(groupCharacterCardPrefab, initPos, Quaternion.identity);
            card.AddComponent<UIGroupCharacterCard>();
            card.GetComponent<UIGroupCharacterCard>().FightCharacter = fCharacter;
            groupCards.Add(card);
            
            var cardT = card.transform;
            var entityCharacter = fCharacter.Entity as Character;
            cardT.parent = groupPanel.transform;
            cardT.Find("CharacterName").GetComponent<Text>().text
                = $"{entityCharacter.FirstName} {entityCharacter.Surname}";
            cardT.Find("Photo").GetComponent<Image>().sprite = entityCharacter.Sprite;
            cardT.Find("Health").GetComponent<Text>().text = fCharacter.Entity.Body.Hp.ToString();
            cardT.Find("Energy").GetComponent<Text>().text = fCharacter.RemainingEnergy.ToString();
        }

    }

    private void CreateQueueCards()
    {
        foreach (var fightCharacter in FightSceneController.CharactersQueue
                                       .Select(obj => obj.GetComponent<FightCharacter>()))
        {
            var queueCard = Instantiate(queueCardPrefab, queuePanel.transform.position, Quaternion.identity);
            var cardYOffSet = -queuePanel.GetComponent<RectTransform>().rect.height / 2 
                              + (0.5 + cardsQueue.Count) * queueCard.GetComponent<RectTransform>().rect.height;
            queueCard.transform.Translate(new Vector3(0f, (float)cardYOffSet,0f));
            queueCard.transform.parent = queuePanel.transform;
            queueCard.GetComponent<Image>().color = (fightCharacter.Type == CharacterType.Ally) 
                                            ? new Color(0,1,0,0.7f) : new Color(1,0,0,0.7f);

            queueCard.AddComponent<UIQueueCard>();
            queueCard.GetComponent<UIQueueCard>().FightCharacter = fightCharacter;

            if (fightCharacter.Type == CharacterType.Ally)
            {
                var character = fightCharacter.Entity as Character;
                queueCard.transform.Find("FightCharacterName").GetComponent<Text>().text =
                    $"{character.FirstName} {character.Surname}";
            }
            else
                queueCard.transform.Find("FightCharacterName").GetComponent<Text>().text = "Крыса";

            //queueCard.SetActive(cardsQueue.Count < MaxDrawCardsCount);
            cardsQueue.Enqueue(queueCard);
        }
    }

    public void ChangeActiveCard()
    {
        var active = cardsQueue.Dequeue();
        //active.SetActive(cardsQueue.Count < MaxDrawCardsCount);

        var offset = cardsQueue.Count * active.GetComponent<RectTransform>().rect.height;
        active.transform.Translate(new Vector3(0, offset, 0));

        ShiftCards();
        cardsQueue.Enqueue(active);
    }

    public void RedrawGroupCharacterCards()
    {
        foreach (var card in groupCards)
        {
            var character = card.GetComponent<UIGroupCharacterCard>().FightCharacter;
            card.transform.Find("Energy").GetComponent<Text>().text 
                = character.RemainingEnergy.ToString();
            card.transform.Find("Health").GetComponent<Text>().text 
                = character.Entity.Body.Hp.ToString();
        }
    }

    public void DeleteDeathCharacterCard(FightCharacter character)
    {
        var newQueue = new Queue<GameObject>();
        var skippedCardsCount = 0;
        while(cardsQueue.Count > 0)
        {
            var currentCard = cardsQueue.Dequeue();
            if(character == currentCard.GetComponent<UIQueueCard>().FightCharacter)
            {
                ShiftCards(skippedCardsCount);
                Destroy(currentCard);
            }
            else
                newQueue.Enqueue(currentCard);
        }
        cardsQueue = newQueue;
    }

    public void DrawDamage(GameObject target, float damage)
    {
        var textPanel = new GameObject();
        textPanel.AddComponent<TextMesh>();
        textPanel.GetComponent<TextMesh>().text = damage.ToString();
        textPanel.GetComponent<TextMesh>().color = Color.red;
        textPanel.transform.position = target.transform.position - transform.right * 1f + transform.up * 2f;
        textPanel.transform.SetParent(target.transform);
        textPanel.transform
            .LookAt(new Vector3(camera.transform.position.x, textPanel.transform.position.y,
                camera.transform.position.z));
        textPanel.transform.Rotate(new Vector3(0, 180f, 0));
        StartCoroutine(DeleteDamageTextPanel(textPanel));
    }

    private IEnumerator DeleteDamageTextPanel(GameObject textPanel)
    {
        for (var i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            textPanel.transform.Translate(Vector3.up * 0.15f);
        }

        Destroy(textPanel);
    }

    private void ShiftCards(int startShiftIndex = 0)
    {
        var drawShiftCards = 0;
        foreach(var card in cardsQueue)
        {
            if(drawShiftCards >= startShiftIndex)
            {
                card.transform.Translate(new Vector3(0, -card.GetComponent<RectTransform>().rect.height, 0));
                //card.SetActive(drawShiftCards + startShiftIndex < MaxDrawCardsCount);
            }
            drawShiftCards++;
        }
    }
}
