using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClassLibrary
{
    public class FinishBanner
    {
        Grid MainGrid;
        MyTimer timer;
        public MyTimer Timer { set { timer = value; } }
        public readonly Grid grid;

        TextBlock dificultyTxtBlck;
        TextBlock timeTxtBlck;
        TextBlock errorsTxtBlck;
        public string dificulty;
        MyTile verifyTile;

        public FinishBanner(Grid MainGrid, MyTimer timer)
        {
            this.MainGrid = MainGrid;
            this.timer = timer;
            grid = new Grid() { Background = new SolidColorBrush(Color.FromArgb(245, 218, 202, 151)), Margin = new Thickness(0, 30, 0, 30) };

            ColorAnimation ca = new ColorAnimation();
            ca.From = Color.FromArgb(0, 218, 202, 151);
            ca.To = Color.FromArgb(245, 218, 202, 151);
            ca.Duration = TimeSpan.FromSeconds(0.5);
            grid.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);

            //FinishGameGrid.ShowGridLines = true;

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Star) });

            TextBlock winnerTxtBlck = new TextBlock() { Text = "Перемога", FontSize = 75, Foreground = MyBrushes.SelectForgrBrush, TextAlignment = TextAlignment.Center };
            Grid.SetRow(winnerTxtBlck, 1);
            Grid.SetColumn(winnerTxtBlck, 1);
            grid.Children.Add(winnerTxtBlck);

            dificultyTxtBlck = new TextBlock() { FontSize = 25, Foreground = MyBrushes.SelectForgrBrush, TextAlignment = TextAlignment.Left };
            Grid.SetRow(dificultyTxtBlck, 2);
            Grid.SetColumn(dificultyTxtBlck, 1);
            grid.Children.Add(dificultyTxtBlck);

            timeTxtBlck = new TextBlock() { FontSize = 25, Foreground = MyBrushes.SelectForgrBrush, TextAlignment = TextAlignment.Left };
            Grid.SetRow(timeTxtBlck, 3);
            Grid.SetColumn(timeTxtBlck, 1);
            grid.Children.Add(timeTxtBlck);

            errorsTxtBlck = new TextBlock() { FontSize = 25, Foreground = MyBrushes.SelectForgrBrush, TextAlignment = TextAlignment.Left };
            Grid.SetRow(errorsTxtBlck, 4);
            Grid.SetColumn(errorsTxtBlck, 1);
            grid.Children.Add(errorsTxtBlck);

            Grid.SetRow(grid, 1);
            Grid.SetColumn(grid, 2);
            Grid.SetZIndex(grid, 1);            
        }

        public void Hide()
        {
            if (MainGrid.Children.Contains(grid))
            {
                timer.TimerLabel.Visibility = Visibility.Visible;
                verifyTile.IsEnabled = true;
                MainGrid.Children.Remove(grid);
            }
            
        }

        public void Show(MyTile tile, int errorCount)
        {
            verifyTile = tile;
            verifyTile.IsEnabled = false;
            timer.Stop();
            timer.TimerLabel.Visibility = Visibility.Hidden;

            dificultyTxtBlck.Text = "Важкість:  " + dificulty;
            timeTxtBlck.Text = "Час:  " + timer.ToString();
            errorsTxtBlck.Text = "Кількість помилок:  " + errorCount;
            MainGrid.Children.Add(grid);
        }

    }
}
