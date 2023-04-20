using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LeaderboardMessageGenerator;

/* Because I am lazy and don't want to write the laderboard message manually I made this extremely terrible script that does it for me because I'm lazy and have nothing better to do in my spare time. */
var months = new List<string>
{
    "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november",
    "december"
};
var url = "https://raw.githubusercontent.com/marksam32/StreamSettings/master/TP%20Previous%20Leaderboards/Official/Regular/Marksam%20(AUG%202021%2B)/TP%20Scores%20{0}%20{1}.xml";

Console.WriteLine("Please type the month and year in this format:");
Console.WriteLine("Month Year");
var input = Console.ReadLine();
var tokenizedInput = input.Trim().Split(" ");

if (tokenizedInput.Length != 2)
{
    Console.WriteLine("Error 1");
    return;
}

var foo = tokenizedInput[0].ToLowerInvariant();

if (!months.Contains(foo))
{
    Console.WriteLine("Error 2");
    return;
}

if (!int.TryParse(tokenizedInput[1], out var year))
{
    Console.WriteLine("Error 3");
    return;
}

if (year > DateTime.Today.Year)
{
    Console.WriteLine("Error 4");
    return;
}

tokenizedInput[0] = foo[0].ToString().ToUpperInvariant() + foo.Substring(1);
Console.WriteLine(tokenizedInput[0] + " " + tokenizedInput[1]);

var reader = new XmlTextReader(string.Format(url, tokenizedInput[0], tokenizedInput[1]));
var deserializedXml = (LeaderboardSet)new XmlSerializer(typeof(LeaderboardSet)).Deserialize(reader)!;

var topThree = deserializedXml.LeaderboardEntries.Take(3).ToArray();

var sb = new StringBuilder();
sb.Append("Good evening @everyone!");
sb.AppendLine()
    .AppendFormat(
        $"The leaderboard has been finalized for {tokenizedInput[0]} {tokenizedInput[1]}. Here are the top 3 winners: ");
sb.AppendLine().AppendLine().AppendLine("Congratulations to:");
for (var i = 0; i < 3; ++i)
{
    var curr = topThree[i];
    sb.AppendFormat(
            $"{curr.UserName} with __{curr.SolveCount}__ Solves, __{curr.StrikeCount}__ Strikes, and a final score of **{curr.ScoreString}!** awarding them with a {placeToMedal(i)} medal in {ordinal(i)} place{((curr.Score >= 10000) ? ", as well as a Platinum Medal for **10K!** points!" : "!")}").AppendLine();
}

Console.WriteLine(sb.ToString());

static string placeToMedal(int place)
{
    switch (place)
    {
        case 0:
            return "gold";
        case 1:
            return "silver";
        case 2:
            return "bronze";
        default:
            throw new InvalidOperationException();
    }
}

static string ordinal(int number)
{
    switch (number)
    {
        case 0:
            return "1st";
        case 1:
            return "2nd";
        case 2:
            return "3rd";
        default:
            throw new InvalidOperationException();
    }
}