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
        MainWindow mainWindow;
        Grid MainGrid;
        MyLabel[,] CellsArray;

        MyTimer timer;
        public TransitioningContentControl contentControl;

        public event EventHandler StartGame;

        MyMainMenu mainMenu;
        MyMenu menu;
        FinishBanner finishBanner;

        public Controller(MainWindow mainWindow, Grid MainGrid, ref MyLabel[,] CellsArray, Label timeLabel)
        {
            this.mainWindow = mainWindow;
            this.MainGrid = MainGrid;
            this.CellsArray = CellsArray;
            this.timeLabel = timeLabel;
            timer = new MyTimer(timeLabel);
            
            contentControl = new TransitioningContentControl() {Transition = TransitionType.Left, RestartTransitionOnContentChange = true };

            mainMenu = new MyMainMenu(NewGameBtn_Click, ResumeBtn_Click, OpenBtn_Click);            
            menu = new MyMenu(ResumeBtn_Click,SaveBtn_Click,MainMenuBtn_Click);
            finishBanner = new FinishBanner(MainGrid, timer);

            ShowGrid(mainMenu.grid);
        }

        public void ShowGrid(Grid grid)
        {
            contentControl.Content = grid;
        }        
    }
}
