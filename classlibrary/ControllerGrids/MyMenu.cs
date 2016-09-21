using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassLibrary
{
    public class MyMenu
    {
        public readonly Grid grid;

        public MyMenu(RoutedEventHandler ResumeBtn_Click, RoutedEventHandler SaveBtn_Click, RoutedEventHandler MainMenuBtn_Click)
        {
            grid = new Grid();
            
            //MainMenuGrid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Star) });



            Button resumeBtn = new Button();
            resumeBtn.Style = Application.Current.Resources["AccentedSquareButtonStyle"] as Style;
            TextBlock ResumeBtnTxtBlc = new TextBlock();
            ResumeBtnTxtBlc.Text = "Відновити";
            resumeBtn.Content = ResumeBtnTxtBlc;
            resumeBtn.FontSize = 18;

            resumeBtn.Click += ResumeBtn_Click;
            Grid.SetColumn(resumeBtn, 1);
            Grid.SetRow(resumeBtn, 1);
            grid.Children.Add(resumeBtn);


            Button saveBtn = new Button();
            saveBtn.Style = Application.Current.Resources["AccentedSquareButtonStyle"] as Style;
            TextBlock SaveBtnTxtBlc = new TextBlock();
            SaveBtnTxtBlc.Text = "Зберегти";
            saveBtn.Content = SaveBtnTxtBlc;
            saveBtn.FontSize = 18;
            saveBtn.Click += SaveBtn_Click;
            Grid.SetColumn(saveBtn, 1);
            Grid.SetRow(saveBtn, 3);
            grid.Children.Add(saveBtn);

            Button mainMenuBtn = new Button();
            mainMenuBtn.Style = Application.Current.Resources["AccentedSquareButtonStyle"] as Style;
            TextBlock MainMenuBtnTxtBlc = new TextBlock();
            MainMenuBtnTxtBlc.Text = "Головне меню";
            mainMenuBtn.Content = MainMenuBtnTxtBlc;
            mainMenuBtn.FontSize = 18;
            mainMenuBtn.Click += MainMenuBtn_Click;
            Grid.SetColumn(mainMenuBtn, 1);
            Grid.SetRow(mainMenuBtn, 5);
            grid.Children.Add(mainMenuBtn);
        }
    }
}
