using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private GameObject queueCardPrefab;
    [SerializeField] private GameObject queuePanel;
    private Queue<GameObject> cardsQueue = new Queue<GameObject>();

    //private const int MaxDrawCardsCount = 5;
    
    void Awake()
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

    public void CreateQueueCards()
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

            queueCard.transform.Find("FightCharacterName").GetComponent<Text>().text = "Character" + cardsQueue.Count;
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
