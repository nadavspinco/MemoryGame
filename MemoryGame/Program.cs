using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MemoryGame;

namespace MemoryGameUi
{
    public class Program
    {
        public static void Main()
        {
            
           InvokeUi(); 
        }

        public static void InvokeUi()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameSettingForm settingForm = new GameSettingForm();
            Application.Run(settingForm);
            if (settingForm.DialogResult == DialogResult.OK)
            {
                DialogResult gameFormDialogResult;
                do
                {
                    MemoryGameBoardForm gameBoardForm = new MemoryGameBoardForm(
                        settingForm.RowSize,
                        settingForm.ColSize,
                        settingForm.FirstPlayerName,
                        settingForm.SecondPlayerName,
                        settingForm.GameMode);
                    Application.Run(gameBoardForm);
                    gameFormDialogResult = gameBoardForm.DialogResult;
                } while (gameFormDialogResult == DialogResult.Yes);
            }
           
        }
    }
}
