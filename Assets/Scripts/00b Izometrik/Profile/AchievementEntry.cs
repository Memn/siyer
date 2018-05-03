using UnityEngine;
using UnityEngine.UI;

public class AchievementEntry : MonoBehaviour
{
    public Image AchievementPic;
    public Text AchievementName;

    public void Init(Sprite pic, string name, bool completed = false)
    {
        AchievementPic.sprite = pic;
        AchievementName.text = name;
        AchievementPic.color = completed ? Color.white : Color.gray;
    }
}