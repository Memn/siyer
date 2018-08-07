using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsConstructor : MonoBehaviour
{
    public GameObject QuestsParent;
    public GameObject QuestPrefab;
    public CongratsUtil Congrats;

    public Quest2[] Construct()
    {
        var i = 0;
        var initQuests = Util.InitQuests();
        var quests = new Quest2[initQuests.Length];
        foreach (var quest in initQuests)
        {
            quests[i++] = CreateQuest(i, quest);
        }

        return quests;
    }

    private Quest2 CreateQuest(int i, Util.Quest2 quest)
    {
        var memberObj = Instantiate(QuestPrefab, Vector3.zero, Quaternion.identity, QuestsParent.transform);
        memberObj.GetComponent<RectTransform>().localScale = Vector3.one;
        memberObj.GetComponent<RectTransform>().SetAsFirstSibling();
        memberObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(225, 55, 0);
        memberObj.gameObject.SetActive(false);
        memberObj.name = string.Format("Quest({0})", i);
        var q = memberObj.GetComponent<Quest2>();
        q.Congrats = Congrats;
        q.Question = quest.Question;
        q.Url = quest.Url;
        q.Completed = quest.Completed;
        q.DecorateQuestion();
        return q;
    }
}