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
using System.Globalization;
using System.Windows.Media.Animation;
using System.Windows.Controls.Primitives;
using System.Media;
using System.IO;





namespace SudokuWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Controller controller;
        MyLabel[,] CellsArray = new MyLabel[9, 9];

        SoundPlayer soundPlayer;
        int hints = 3;
        bool volumeIsEnable = true;
        

        public MainWindow()
        {
            InitializeComponent();
            

            StartGame();
            controller = new Controller(MyMainWindow, MainGrid, ref CellsArray, TimerLabel);
            controller.StartGame += ResetValues;
            MyMainWindow.Content = controller.contentControl;

            

            //controller.FinishGame();
        }


        void SetBindings()
        {
            if (BindingOperations.GetBinding(MainGrid.ColumnDefinitions[2], ColumnDefinition.WidthProperty) == null&&
                BindingOperations.GetBinding(MyMainWindow, MinWidthProperty) == null)
            {
                Binding CellsGridWidth = new Binding();
                CellsGridWidth.Path = new PropertyPath(Grid.ActualHeightProperty);
                CellsGridWidth.ElementName = "CellsGrid";
                CellsGridWidth.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(MainGrid.ColumnDefinitions[2], ColumnDefinition.WidthProperty, CellsGridWidth);

                SumTwoColumnsConverter sumTwoColumnsConverter = new SumTwoColumnsConverter();
                MultiBinding minWidth = new MultiBinding();
                minWidth.Converter = sumTwoColumnsConverter;

                Binding bnd1 = new Binding();
                bnd1.Mode = BindingMode.OneWay;
                bnd1.Source = MainGrid.ColumnDefinitions[1].Width.Value + 20;

                Binding bnd2 = new Binding();
                bnd2.Path = new PropertyPath(Grid.ActualHeightProperty);
                bnd2.ElementName = "CellsGrid";
                bnd2.Mode = BindingMode.OneWay;

                minWidth.Bindings.Add(bnd1);
                minWidth.Bindings.Add(bnd2);

                BindingOperations.SetBinding(MyMainWindow, MinWidthProperty, minWidth);
            }

        }
        void StartGame()
        {   
            soundPlayer = new SoundPlayer("Ori_and_The_Blind_forest_-_Main_Theme.wav");
            soundPlayer.PlayLooping();

            for (int i = 0; i < 9; i++)
            {
                CellsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                CellsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                for (int j = 0; j < 9; j++)
                {
                    MyLabel label = new MyLabel(i, j);
                    
                    label.KeyDown += new KeyEventHandler(Label_KeyDown);
                    label.LostFocus += Label_LostFocus;
                    label.MouseDown += new MouseButtonEventHandler(Label_MouseDown);
                    Grid.SetColumn(label, j % 3 + 4 * (j / 3));
                    Grid.SetRow(label, i % 3 + 4 * (i / 3));
                    CellsArray[i, j] = label;
                    CellsGrid.Children.Add(label);
                }
            }
            CellsGrid.ColumnDefinitions.Insert(3, new ColumnDefinition { Width = new GridLength(4) });
            CellsGrid.ColumnDefinitions.Insert(7, new ColumnDefinition { Width = new GridLength(4) });
            CellsGrid.RowDefinitions.Insert(3, new RowDefinition { Height = new GridLength(4) });
            CellsGrid.RowDefinitions.Insert(7, new RowDefinition { Height = new GridLength(4) });
        }

        void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.MenuBtn_Click(sender,e);
            DuplicateButton.IsChecked = false;
            ShowIncorect.IsChecked = false;
            Hint.IsChecked = false;
            Note.IsChecked = false;
            Erase.IsChecked = false;
        }
        void verifyTile_Click(object sender, RoutedEventArgs e)
        {
            controller.Verify((MyTile)sender);
        }

        void Label_KeyDown(object sender, KeyEventArgs e)
        {
            MyLabel lbl = (MyLabel)sender;
            if (!lbl.IsLock)
            {
                int charKey = (char)e.Key - 34 <= 9 && (char)e.Key - 34 >= 1 ?
                    (char)e.Key - 34 : (char)e.Key - 74 <= 9 && (char)e.Key - 74 >= 1 ?
                    (char)e.Key - 74 : 0;

                if (charKey != 0)
                {
                    if ((bool)Note.IsChecked)                        
                        controller.WriteNote(lbl, charKey);
                    else
                        controller.WriteDigit(lbl, charKey, (bool)DuplicateButton.IsChecked);
                }
                else if (e.Key == Key.Delete)
                {
                    controller.RemoveDigit(lbl);
                }
            }
            if (e.Key == Key.Escape)
            {
                lbl.Focusable = false;
            }
        }
        void Label_MouseDown(object sender, RoutedEventArgs e)
        {
            MyLabel lbl = (MyLabel)sender;
            if (lbl.IsFocused)
            {
                lbl.Focusable = false;
            }
            else
            {
                lbl.Focusable = true;
                lbl.Focus();
                if (!lbl.IsLock)
                {
                    if ((bool)Erase.IsChecked)
                    {
                        controller.RemoveDigit(lbl);
                        Erase.IsChecked = false;
                    }
                    else
                        if ((bool)Hint.IsChecked)
                        {
                            controller.OpenDigit(lbl);
                            hints--;
                            Hint.IsChecked = false;
                        }
                }
                controller.SelectLabels(lbl);
            }
        }
        void Label_LostFocus(object sender, RoutedEventArgs e)
        {
            MyLabel lbl = (MyLabel)sender;
            controller.UnSelectLabels(lbl);
        }

        private void ShowIncorect_Checked(object sender, RoutedEventArgs e)
        {
            controller.StartErrorCheck();
        }
        private void ShowIncorect_Unchecked(object sender, RoutedEventArgs e)
        {
            controller.StopErrorCheck();            
        }        
        private void Hint_Unchecked(object sender, RoutedEventArgs e)
        {
            Hint.Content = string.Format("Швидка допомога  x{0}", hints);
            if (hints == 0)
                Hint.IsEnabled = false;
        }
        void volumeButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            StackPanel sp = b.Content as StackPanel;
            TextBlock tb = sp.Children[1] as TextBlock;
            if (volumeIsEnable)
            {
                volumeIsEnable = false;
                soundPlayer.Stop();
                tb.Text = "Вимк";
            }
            else
            {
                volumeIsEnable = true;
                tb.Text = "Ввімк";
                soundPlayer.PlayLooping();
            }

        }
        void instruction_Click(object sender, RoutedEventArgs e)
        {
               ManualWindow  intsr = new ManualWindow();
        }

        public void ResetValues(object sender, EventArgs e)
        {
            SetBindings();

            hints = 3;
            Hint.IsEnabled = true;
            Hint.Content = string.Format("Швидка допомога  x{0}", hints);

            Rectangle icon = new Rectangle() { Fill = new SolidColorBrush(Colors.White),
                Height = 70,Width = 45,
                OpacityMask = new VisualBrush(Application.Current.Resources["appbar_question"] as Visual)};
            verifyTile.Content = icon;
            verifyTile.Background = new SolidColorBrush(Colors.DodgerBlue);
        }
    }
    class SumTwoColumnsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)values[0] + (double)values[1];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

        


        



        
         
}
