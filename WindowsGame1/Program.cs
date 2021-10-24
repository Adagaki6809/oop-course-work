using System;
using System.Windows.Forms;
namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                try
                {
                    game.Run();
                }
                catch (Exception e)
                {
                    Exception exc=new Exception();
                    MessageBox.Show("sad", e.Message, MessageBoxButtons.OK);
                }
            }
        }
    }
#endif
}

