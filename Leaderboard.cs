using System.Xml.Serialization;

namespace LeaderboardMessageGenerator;

[XmlRoot("ArrayOfLeaderboardEntry")]
public class LeaderboardSet
{
    [XmlElement("LeaderboardEntry")] public LeaderboardEntry[] LeaderboardEntries;
}

public class LeaderboardEntry
{
    [XmlElement("UserName")] public string UserName;
    [XmlElement("SolveCount")] public string SolveCount;
    [XmlElement("StrikeCount")] public string StrikeCount;
    [XmlElement("SolveScore")] public string ScoreString;

    public int Score
    {
        get
        {
            return int.Parse(ScoreString);
        }
    }
}