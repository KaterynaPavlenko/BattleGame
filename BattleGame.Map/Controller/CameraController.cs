using System.Windows.Forms;
using BattleGame.Models;

namespace BattleGame.Map.Controller
{
    public static class CameraController
    {
        public static void Move(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    MapController.TryToStep(Direction.Up);
                    break;
                case Keys.Down:
                case Keys.S:
                    MapController.TryToStep(Direction.Down);
                    break;
                case Keys.Left:
                case Keys.A:
                    MapController.TryToStep(Direction.Left);
                    break;
                case Keys.Right:
                case Keys.D:
                    MapController.TryToStep(Direction.Right);
                    break;
            }
        }
    }
}