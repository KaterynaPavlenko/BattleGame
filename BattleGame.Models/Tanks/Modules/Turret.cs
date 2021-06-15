using BattleGame.Models.Tanks.Interfaces;

namespace BattleGame.Models.Tanks.Modules
{
    public class Turret : ITankModule
    {
        public Turret(int level)
        {
            SetLevel(level);
        }

        public float Sharpshooting { get; set; }
        public int Level { get; set; }

        public void SetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Level = 1;
                    Sharpshooting = 0.7f;
                    break;
                case 2:
                    Level = 2;
                    Sharpshooting = 0.8f;
                    break;
                case 3:
                    Level = 3;
                    Sharpshooting = 0.9f;
                    break;
            }
        }
    }
}