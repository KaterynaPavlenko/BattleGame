namespace BattleGame.Models
{
    public class Wall : Cell
    {
        public Wall(int x, int y) : base(x, y)
        {
        }

        public override bool IsAllowStep { get; set; } = false;
    }
}