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
        public int Puntos { get; set; }
    }

    internal static class ScoreManager
    {

        private static Dictionary<Jugador, PlayerScore> currentGameScores;

        private class PlayerScore
        {
            public int SoldadosEliminados { get; set; }
            public int TorresEliminadas { get; set; }
            public int ReyesEliminados { get; set; }

            public int Puntos => SoldadosEliminados * 10 + TorresEliminadas * 10 + ReyesEliminados * 60;
        }

        public static void StartNewGame()
        {
            currentGameScores = new Dictionary<Jugador, PlayerScore>
            {
                [Jugador.Blanco] = new PlayerScore(),
                [Jugador.Negro] = new PlayerScore()
            };
        }

        public static void RegisterCapture(Jugador captor, TipoPieza pieza)
        {
            if (currentGameScores == null) StartNewGame();
            var ps = currentGameScores[captor];
            switch (pieza)
            {
                case TipoPieza.Soldado:
                    ps.SoldadosEliminados++;
                    break;
                case TipoPieza.Torre:
                    ps.TorresEliminadas++;
                    break;
                case TipoPieza.Rey:
                    ps.ReyesEliminados++;
                    break;
            }
        }

        public static (int puntos, int soldados, int torres, int reyes) GetScoreSummary(Jugador jugador)
        {
            if (currentGameScores == null) StartNewGame();
            var ps = currentGameScores[jugador];
            return (ps.Puntos, ps.SoldadosEliminados, ps.TorresEliminadas, ps.ReyesEliminados);
        }

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

            }
        }

        public static IReadOnlyList<ScoreRecord> GetLeaderboard()
        {
            EnsureLoaded();
            return cache.OrderByDescending(r => r.Puntos).ToList();
        }

        public static void AddGamePoints(Jugador jugador, int puntosObtenidos)
        {
            EnsureLoaded();
            string key = jugador.ToString();
            var rec = cache.Find(r => r.Jugador == key);
            if (rec == null)
            {
                rec = new ScoreRecord { Jugador = key, Puntos = puntosObtenidos };
                cache.Add(rec);
            }
            else
            {
                rec.Puntos += puntosObtenidos;
            }
            Save();
        }

        public static void ResetLeaderboard()
        {
            EnsureLoaded();
            cache.Clear();
            Save();
        }
    }
}