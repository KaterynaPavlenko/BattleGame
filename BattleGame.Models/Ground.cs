using System.Drawing;

namespace BattleGame.Models
{
    public class Ground : Cell
    {
        public Ground(int x, int y, Image sprite) : base(x, y)
        {
            Sprite = sprite;
        }

        public override bool IsAllowStep { get; set; } = true;
    }
}