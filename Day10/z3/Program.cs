using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        RaceGame game = new RaceGame();

        Spectator spectator1 = new Spectator("Алексей");
        Spectator spectator2 = new Spectator("Мария");
        RaceCommentator commentator = new RaceCommentator("Дмитрий");

        game.Subscribe(spectator1);
        game.Subscribe(spectator2);
        game.Subscribe(commentator);

        game.SetRaceStatus("Гонка началась!");
        game.SetRaceStatus("Пилот №1 вышел вперед");
        game.SetRaceStatus("Финиш! Победитель - пилот №1");
    }
}