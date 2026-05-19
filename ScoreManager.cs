using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Juego_de_Estrategias
{
    internal class ScoreRecord
    {
        public string Jugador { get; set; }
        public int Victorias { get; set; }
    }

    internal static class ScoreManager
    {
        private static readonly string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Juego-de-Estrategias");
        private static readonly string file = Path.Combine(folder, "scores.json");

        private static List<ScoreRecord> cache;

        private static void EnsureLoaded()
        {
            if (cache != null) return;
            try
            {
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                if (File.Exists(file))
                {
                    string json = File.ReadAllText(file);
                    cache = JsonSerializer.Deserialize<List<ScoreRecord>>(json) ?? new List<ScoreRecord>();
                }
                else
                {
                    cache = new List<ScoreRecord>();
                }
            }
            catch
            {
                cache = new List<ScoreRecord>();
            }
        }

        private static void Save()
        {
            try
            {
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                string json = JsonSerializer.Serialize(cache, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(file, json);
            }
            catch
            {
                // fallo silencioso, no bloquear la app
            }
        }

        public static IReadOnlyList<ScoreRecord> GetLeaderboard()
        {
            EnsureLoaded();
            return cache.OrderByDescending(r => r.Victorias).ToList();
        }

        public static void AddWin(Jugador ganador)
        {
            EnsureLoaded();
            string key = ganador.ToString();
            var rec = cache.Find(r => r.Jugador == key);
            if (rec == null)
            {
                rec = new ScoreRecord { Jugador = key, Victorias = 1 };
                cache.Add(rec);
            }
            else
            {
                rec.Victorias++;
            }
            Save();
        }
    }
}