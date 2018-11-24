using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


[DataContract(Name = "GameData")]
public class GameData  

{
    [DataMember(Name = "userName")] public string UserName = "";
    [DataMember(Name = "score")] public int Score = 0;
    [DataMember(Name = "playedTime")] public long PlayedTime = 0;
    [DataMember(Name = "level")] public int Level = 1;
    [DataMember(Name = "achievements")] public List<AchievementDto> Achievements = new List<AchievementDto>();
}
