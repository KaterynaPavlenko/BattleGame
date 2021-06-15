using System.Drawing;

namespace BattleGame.Models
{
    public class Base : Cell
    {
        public Base(int x, int y) : base(x, y)
        {
        }

        public Base(int x, int y, Image sprite) : base(x, y)
        {
            Sprite = sprite;
        }

        public override bool IsAllowStep { get; set; } = false;
    }
}