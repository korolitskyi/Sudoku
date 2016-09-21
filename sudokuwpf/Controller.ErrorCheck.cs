using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SudokuWPF
{
    public partial class Controller
    {
        List<MyLabel> errList = new List<MyLabel>();
        bool errorcheck = false;
        

        public void Verify(MyTile tile)
        {
            if ((Color)tile.Background.GetValue(SolidColorBrush.ColorProperty) != Colors.Green)
            {

                foreach (MyLabel e in UnlockCells)
                {
                    if (values[e.Row, e.Column] == 0)
                        return;
                }

                Rectangle icon = new Rectangle()
                {
                    Fill = new SolidColorBrush(Colors.White),
                    Height = 45,
                    Width = 45
                };

                foreach (MyLabel e in UnlockCells)
                {
                    if (values[e.Row, e.Column] != result[e.Row, e.Column] && values[e.Row, e.Column] != 0)
                    {
                        icon.OpacityMask = new VisualBrush(Application.Current.Resources["appbar_close"] as Visual);
                        tile.Content = icon;
                        tile.Background = new SolidColorBrush(Colors.Red);
                        return;
                    }
                }              
                icon.OpacityMask = new VisualBrush(Application.Current.Resources["appbar_check"] as Visual);
                tile.Content = icon;
                tile.Background = new SolidColorBrush(Colors.Green);

                finishBanner.Show(tile, errorCount);
                
            }
        }

        public void StartErrorCheck()
        {
            errorcheck = true;
            foreach (MyLabel e in errList)
            {
                AnimatedColorCell(CellsArray[e.Row, e.Column], false, CellsArray[e.Row, e.Column].GetColor(), MyColors.ErrorBackColor);
            }
        }
        public void StopErrorCheck()
        {
            errorcheck = false;
            foreach (MyLabel e in errList)
            {
                AnimatedColorCell(e, false, MyColors.ErrorBackColor, MyColors.UnLockBackColor);
            }
        }

        void CheckLabelOnError(MyLabel lbl)
        {
            if (ErrorCheck(lbl))
            {
                errorCount--;
                if (errList.Contains(lbl))
                    errList.Remove(lbl);
            }
        }

        bool ErrorCheck(MyLabel lbl)
        {
            int i = lbl.Row, j = lbl.Column;
            if (values[i, j] != result[i, j] && values[i, j] != 0)
            {
                if (!errList.Contains(lbl))
                    errList.Add(lbl);
                if (CellsArray[lbl.Row, lbl.Column].GetColor() != MyColors.ErrorBackColor && errorcheck)
                    AnimatedColorCell(CellsArray[lbl.Row, lbl.Column], false, CellsArray[lbl.Row, lbl.Column].GetColor(), MyColors.ErrorBackColor);

                return false;
            }
            if (CellsArray[lbl.Row, lbl.Column].GetColor() != MyColors.SelectBackColor)
                AnimatedColorCell(CellsArray[lbl.Row, lbl.Column], false, CellsArray[lbl.Row, lbl.Column].GetColor(), MyColors.SelectBackColor);
            return true;
        }

        bool DuplicateCheck(bool duplicateEnable,MyLabel lbl,int value)
        {
            if (duplicateEnable)
            {
                if (DuplicateHorisontalCheck(value, lbl.Row, lbl.Column) &&
                    DuplicateVerticalCheck(value, lbl.Row, lbl.Column) &&
                    DuplicateSquareCheck(value, lbl.Row, lbl.Column))
                    return true;
            }
            else
                return true;
            return false;
        }
        bool DuplicateHorisontalCheck(int value, int i, int j)
        {
            for (int a = 0; a < 9; a++)
            {
                if (values[i, a] == value)
                {
                    if (j == a)
                        continue;
                    return false;
                }
            }
            return true;
        }
        bool DuplicateVerticalCheck(int value, int i, int j)
        {
            for (int a = 0; a < 9; a++)
            {
                if (values[a, j] == value)
                {
                    if (i == a)
                        continue;
                    return false;
                }
            }
            return true;
        }
        bool DuplicateSquareCheck(int value, int i, int j)
        {
            for (int a = i - i % 3; a < 3; a++)
            {
                for (int b = j - j % 3; b < 3; b++)
                {
                    if (values[a, b] == value)
                    {
                        if (a == i && b == j)
                            continue;
                        return false;
                    }
                }
            }

            return true;
        }        
    }
}
