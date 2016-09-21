using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary;
using MahApps.Metro.Controls;

namespace SudokuWPF
{
    public partial class Controller
    {
        void StartGameEvent()
        {
            if (StartGame != null)
                StartGame(this, EventArgs.Empty);
        }

        void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            FillValues((MyNamedTile)sender);
            finishBanner.dificulty = ((MyNamedTile)sender).Title.ToString();
            finishBanner.Hide();
            errorCount = 0;
            errList.Clear();

            StartGameEvent();

            timer.Clear();
            timer.Start();
            ShowGrid(MainGrid);
        }
        void MainMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(mainMenu.grid);
            
            timeLabel.Visibility = Visibility.Visible;
            
            if (!mainMenu.ContinueGameBtn.IsEnabled)
            {
                Grid.SetRow(mainMenu.OpenGameBtn, 2);
                mainMenu.ContinueGameBtn.IsEnabled = true;
                mainMenu.ContinueGameBtn.Visibility = Visibility.Visible;
            }
            
        }          
        void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(MainGrid);
            timer.Start();
        }
        public void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            ShowGrid(menu.grid);            
        }
        void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }
        void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (OpenFile())
            {
                finishBanner.Hide();
                errList.Clear();
                FillCellsArray(values);


                StartGameEvent();

                timer.Start();
                ShowGrid(MainGrid);
            }
        }
    }
}
