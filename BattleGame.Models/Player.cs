using System.Drawing;
using BattleGame.Models.Tanks;

namespace BattleGame.Models
{
    public class Player : Cell
    {
        public Player(int x, int y) : base(x, y)
        {
        }

        public Player(int x, int y, Image sprite) : base(x, y)
        {
            Sprite = sprite;
        }

        public override bool IsAllowStep { get; set; } = false;
        public bool IsBot { get; set; } = true;
        public Tank Tank { get; set; }
        public Direction LastDirection { get; set; } = Direction.Down;
    }
}