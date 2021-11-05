using RLNET;

namespace SibRus
{
    public class Game
    {
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;

        private static RLRootConsole _rootConsole;

        public static void Main()
        {
            string fontFileName = "terminal8x8.png";
    
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            
            _rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight,
              8, 8, 1f, consoleTitle);
            
            _rootConsole.Update += OnRootConsoleUpdate;
            
            _rootConsole.Render += OnRootConsoleRender;
            
            _rootConsole.Run();
        }

        
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _rootConsole.Print(10, 10, "It worked!", RLColor.White);
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            _rootConsole.Draw();
        }
    }
}
