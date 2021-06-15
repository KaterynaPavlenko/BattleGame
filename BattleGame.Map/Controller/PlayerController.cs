using System;
using BattleGame.Models.Tanks;
using BattleGame.Models.Tanks.Modules;

namespace BattleGame.Map.Controller
{
    public class PlayerController
    {
        public Tank CreatePlayer()
        {
            var tank = new Tank();
            var rnd = new Random();
            var value = rnd.Next(1, 3);
            tank.Corps = new Corps(value);
            value = rnd.Next(1, 2);
            if (value == 1)
            {
                value = rnd.Next(1, 3);
                tank.Gun = new RapidFiringGun(value);
            }
            else
            {
                value = rnd.Next(1, 3);
                tank.Gun = new ArmorPiercingCannon(value);
            }

            value = rnd.Next(1, 3);
            tank.Corps = new Corps(value);
            return tank;
        }
    }
}