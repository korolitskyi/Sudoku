using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClassLibrary
{
    public class MyMainMenu
    {
        public readonly Grid grid;

        public readonly MyTile ContinueGameBtn;
        public readonly MyTile OpenGameBtn;

        public MyMainMenu(RoutedEventHandler NewGameBtn_Click, RoutedEventHandler ResumeBtn_Click, RoutedEventHandler OpenBtn_Click)
        {
            grid = new Grid();


            //MainMenuGrid.ShowGridLines = true;

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(13, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(200) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(7, GridUnitType.Star) });


            Label newGameTextLabel = new Label();
            newGameTextLabel.Content = "НОВА ГРА";
            newGameTextLabel.Foreground = new SolidColorBrush(Colors.White);
            newGameTextLabel.VerticalContentAlignment = VerticalAlignment.Center;
            newGameTextLabel.FontSize = 24;
            Grid.SetColumn(newGameTextLabel, 1);
            Grid.SetRow(newGameTextLabel, 0);
            grid.Children.Add(newGameTextLabel);

            Label continueGameTextLabel = new Label();
            continueGameTextLabel.Content = "ПРОДОВЖИТИ";
            continueGameTextLabel.Foreground = new SolidColorBrush(Colors.White);
            continueGameTextLabel.VerticalContentAlignment = VerticalAlignment.Center;
            continueGameTextLabel.FontSize = 24;
            Grid.SetColumn(continueGameTextLabel, 4);
            Grid.SetRow(continueGameTextLabel, 0);
            grid.Children.Add(continueGameTextLabel);

            Rectangle icon_quality_low = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 45,
                Width = 45,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_connection_quality_low"] as Visual)
            };
            MyNamedTile NewGameVeryEasyBtn = new MyNamedTile();
            NewGameVeryEasyBtn.Content = icon_quality_low;
            NewGameVeryEasyBtn.Title = "Дуже легкий";
            NewGameVeryEasyBtn.Height = Double.NaN;
            NewGameVeryEasyBtn.Width = Double.NaN;
            NewGameVeryEasyBtn.SetDificulty(1, 2, "Дуже легкий");
            NewGameVeryEasyBtn.Background = new SolidColorBrush(Colors.Green);
            NewGameVeryEasyBtn.TiltFactor = 4;
            NewGameVeryEasyBtn.Click += NewGameBtn_Click;
            Grid.SetColumn(NewGameVeryEasyBtn, 1);
            Grid.SetRow(NewGameVeryEasyBtn, 1);
            grid.Children.Add(NewGameVeryEasyBtn);

            Rectangle icon_quality_medium = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 45,
                Width = 45,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_connection_quality_medium"] as Visual)
            };
            MyNamedTile NewGameEasyBtn = new MyNamedTile();
            NewGameEasyBtn.Content = icon_quality_medium;
            NewGameEasyBtn.Title = "Легкий";
            NewGameEasyBtn.Height = Double.NaN;
            NewGameEasyBtn.Width = Double.NaN;
            NewGameEasyBtn.SetDificulty(2, 3, "Легкий");
            NewGameEasyBtn.TiltFactor = 4;
            NewGameEasyBtn.Click += NewGameBtn_Click;
            Grid.SetColumn(NewGameEasyBtn, 1);
            Grid.SetRow(NewGameEasyBtn, 2);
            grid.Children.Add(NewGameEasyBtn);

            Rectangle icon_quality_high = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 45,
                Width = 45,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_connection_quality_high"] as Visual)
            };
            MyNamedTile NewGameMediumBtn = new MyNamedTile();
            NewGameMediumBtn.Content = icon_quality_high;
            NewGameMediumBtn.Title = "Середній";
            NewGameMediumBtn.Height = Double.NaN;
            NewGameMediumBtn.Width = Double.NaN;
            NewGameMediumBtn.SetDificulty(3, 4, "Середній");
            NewGameMediumBtn.Background = new SolidColorBrush(Colors.DarkOrchid);
            //NewGameMediumBtn.Background = new SolidColorBrush(Colors.YellowGreen);
            NewGameMediumBtn.TiltFactor = 4;
            NewGameMediumBtn.Click += NewGameBtn_Click;
            Grid.SetColumn(NewGameMediumBtn, 2);
            Grid.SetRow(NewGameMediumBtn, 1);
            grid.Children.Add(NewGameMediumBtn);


            Rectangle icon_quality_veryhigh = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 45,
                Width = 45,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_connection_quality_veryhigh"] as Visual)
            };
            MyNamedTile NewGameHardBtn = new MyNamedTile();
            NewGameHardBtn.Content = icon_quality_veryhigh;
            NewGameHardBtn.Title = "Тяжкий";
            NewGameHardBtn.Height = Double.NaN;
            NewGameHardBtn.Width = Double.NaN;
            NewGameHardBtn.SetDificulty(4, 5, "Тяжкий");
            NewGameHardBtn.Background = new SolidColorBrush(Colors.DarkMagenta);
            NewGameHardBtn.TiltFactor = 4;
            NewGameHardBtn.Click += NewGameBtn_Click;
            Grid.SetColumn(NewGameHardBtn, 2);
            Grid.SetRow(NewGameHardBtn, 2);
            grid.Children.Add(NewGameHardBtn);



            Rectangle icon_control_play = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 55,
                Width = 55,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_control_play"] as Visual)
            };
            ContinueGameBtn = new MyTile();
            ContinueGameBtn.Content = icon_control_play;
            ContinueGameBtn.Title = "Продовжити";
            ContinueGameBtn.Height = Double.NaN;
            ContinueGameBtn.Width = Double.NaN;
            ContinueGameBtn.Background = new SolidColorBrush(Colors.Orange);
            ContinueGameBtn.TiltFactor = 4;

            ContinueGameBtn.IsEnabled = false;
            ContinueGameBtn.Visibility = Visibility.Hidden;

            ContinueGameBtn.Click += ResumeBtn_Click;
            Grid.SetColumn(ContinueGameBtn, 4);
            Grid.SetRow(ContinueGameBtn, 1);
            grid.Children.Add(ContinueGameBtn);



            Rectangle icon_disk_upload = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.White),
                Height = 55,
                Width = 55,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_disk_upload"] as Visual)
            };
            OpenGameBtn = new MyTile();
            OpenGameBtn.Content = icon_disk_upload;
            OpenGameBtn.Title = "Завантажити гру";
            OpenGameBtn.Height = Double.NaN;
            OpenGameBtn.Width = Double.NaN;
            OpenGameBtn.Background = new SolidColorBrush(Colors.Magenta);
            OpenGameBtn.TiltFactor = 4;
            OpenGameBtn.Click += OpenBtn_Click;
            Grid.SetColumn(OpenGameBtn, 4);
            Grid.SetRow(OpenGameBtn, 1);
            grid.Children.Add(OpenGameBtn);
        }
    }
}
