using System;
using System.IO;
using System.Text.Json;

namespace DungeonSlime;

internal static class HighScoreStore
{
    private const string s_fileName = "highscore.json";

    private static string FilePath => Path.Combine(AppContext.BaseDirectory, s_fileName);

    public static int LoadHighScore()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                return 0;
            }

            string json = File.ReadAllText(FilePath);
            HighScoreData data = JsonSerializer.Deserialize<HighScoreData>(json);

            return data?.HighScore ?? 0;
        }
        catch
        {
            return 0;
        }
    }

    public static void SaveHighScore(int highScore)
    {
        try
        {
            HighScoreData data = new HighScoreData { HighScore = highScore };
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(FilePath, json);
        }
        catch
        {
        }
    }

    private sealed class HighScoreData
    {
        public int HighScore { get; set; }
    }
}
