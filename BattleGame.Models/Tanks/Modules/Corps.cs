using BattleGame.Models.Tanks.Interfaces;

namespace BattleGame.Models.Tanks.Modules
{
    public class Corps : ITankModule
    {
        public Corps(int level)
        {
            SetLevel(level);
        }

        public float Speed { get; set; }
        public int Armor { get; set; }
        public int Level { get; set; }

        public void SetLevel(int level)
        {
            switch (level)
            {
                case 1:
                    Level = 1;
                    Armor = 2;
                    Speed = 4;
                    break;
                case 2:
                    Level = 2;
                    Armor = 4;
                    Speed = 2.6f;
                    break;
                case 3:
                    Level = 2;
                    Armor = 6;
                    Speed = 2;
                    break;
            }
        }
    }
}