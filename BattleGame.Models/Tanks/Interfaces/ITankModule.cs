namespace BattleGame.Models.Tanks.Interfaces
{
    public interface ITankModule
    {
        int Level { get; set; }
        void SetLevel(int level);
    }
}