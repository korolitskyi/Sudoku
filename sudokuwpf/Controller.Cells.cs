using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;
using System.IO;

namespace SudokuWPF
{    
    public partial class Controller
    {
        public int errorCount = 0;
        int[,] result;
        int[,] values;
        List<MyLabel> UnlockCells = new List<MyLabel>();
        List<MyLabel> NoteList = new List<MyLabel>();

        private void FillValues(MyNamedTile tile)
        {            
            ValueGeneretor valuegen = new ValueGeneretor();
            values = new int[9, 9];
            valuegen.Initialize(ref result);
            Array.Copy(result, values, result.Length);
            //string[] s = new string[9];
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        s[i] += result[i, j].ToString();
            //    }
            //}
            //File.WriteAllLines(@"D:\\NICE.txt", s);
            
            valuegen.HideCells(ref values, tile.MinDificulty, tile.MaxDificulty);
            FillCellsArray(values);
            NoteList.Clear();
        }

        void FillCellsArray(int[,] values)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CellsArray[i, j].IsNote = false;
                    if (values[i, j] == 0 || values[i, j] >= 10)
                    {
                        if (CellsArray[i, j].Content != null)
                            CellsArray[i, j].Content = null;
                        if (values[i, j] >= 10)
                        {
                            CellsArray[i, j].Foreground = MyBrushes.SelectForgrBrush;
                            values[i, j] /= 10;
                            CellsArray[i, j].Content = values[i, j];
                            //CheckLabelOnError(CellsArray[i, j]);
                        }
                        CellsArray[i, j].Background = MyBrushes.UnLockBackBrush;
                        CellsArray[i, j].IsLock = false;
                        UnlockCells.Add(CellsArray[i, j]);
                    }
                    else
                    {
                        CellsArray[i, j].Background = MyBrushes.LockBackBrush;
                        CellsArray[i, j].Foreground = MyBrushes.UnSelectForgrBrush;
                        CellsArray[i, j].IsLock = true;
                        CellsArray[i, j].Content = values[i, j];                        
                    }
                }
            }
            FillNotes();
        }

        void FillNotes()
        {
            if (stringNoteList.Count != 0)
            {
                NoteList.Clear();
                foreach (string e in stringNoteList)
                {
                    Match m = Regex.Match(e, @"(\d*)(\D*)(\d*)(\D*)([\S\s]*)");
                    int i = Int32.Parse(m.Groups[1].ToString());
                    int j = Int32.Parse(m.Groups[3].ToString());
                    CellsArray[i, j].IsNote = true;
                    CellsArray[i, j].Content = m.Groups[5];
                    NoteList.Add(CellsArray[i, j]);
                }
                stringNoteList.Clear();
            }
        }

        public void WriteNote(MyLabel lbl, int key)
        {
            if (!lbl.IsNote)
            {
                RemoveDigit(lbl);
                NoteList.Add(lbl);
                lbl.IsNote = true;
            }
                
            if (lbl.Content == null || lbl.Content.ToString().Length == 0)
            {
                lbl.Content = key;
            }
            else if (lbl.Content.ToString().Length >= 1 && lbl.Content.ToString().Length < 10)
                lbl.Content = lbl.Content + ", " + key;
            else if (lbl.Content.ToString().Length == 10)
                lbl.Content = lbl.Content + "\n" + key;
            else if (lbl.Content.ToString().Length > 10 && lbl.Content.ToString().Length < 20)
                lbl.Content = lbl.Content + ", " + key;
            else if (lbl.Content.ToString().Length == 21)
            {
                lbl.FontSize = 10;
                lbl.Content = lbl.Content + "\n" + key;
            }
        }

        public void WriteDigit(MyLabel lbl, int key, bool duplicateEnable)
        {
            if (lbl.IsNote)
            {
                NoteList.Remove(lbl);
                lbl.IsNote = false;
            }
            int value = key;
            if (DuplicateCheck(duplicateEnable, lbl, value))
            {
                lbl.Content = value;
                values[lbl.Row, lbl.Column] = value;
                errorCount++;
                //if (ErrorCheck(lbl, value))
                CheckLabelOnError(lbl);
            }
            else
            {
                if (CellsArray[lbl.Row, lbl.Column].GetColor() != MyColors.ErrorBackColor)
                    AnimatedColorCell(lbl, true, MyColors.SelectBackColor, MyColors.ErrorBackColor);
            }

        }

        public void OpenDigit(MyLabel lbl)
        {            
            WriteDigit(lbl, result[lbl.Row, lbl.Column], false);
        }

        public void RemoveDigit(MyLabel lbl)
        {
            lbl.Content = "";
            values[lbl.Row, lbl.Column] = 0;
            if (errList.Contains(lbl))
            {
                AnimatedColorCell(lbl, false,MyColors.ErrorBackColor, MyColors.SelectBackColor);
                errList.Remove(lbl);
            }

        }

        void AnimatedColorCell(MyLabel lbl, bool autoReverse, Color fromcolor, Color tocolor)
        {
            ColorAnimation ca = new ColorAnimation();
            ca.From = fromcolor;
            ca.To = tocolor;
            ca.Duration = TimeSpan.FromSeconds(0.5);
            ca.AutoReverse = autoReverse;
            lbl.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }
        

        #region selections
        public void SelectLabels(MyLabel lbl)
        {
            for (int a = 0; a < 9; a++)
            {
                SelectLabel(CellsArray[a, lbl.Column]);
                SelectLabel(CellsArray[lbl.Row, a]);
            }
        }
        public void UnSelectLabels(MyLabel lbl)
        {
            for (int a = 0; a < 9; a++)
            {
                UnSelectLabel(CellsArray[a, lbl.Column]);
                UnSelectLabel(CellsArray[lbl.Row, a]);
            }
        }
        void SelectLabel(MyLabel label)
        {

            if (errList.Contains(label) && errorcheck) { label.Background = MyBrushes.ErrorBackBrush; }
            else { label.Background = MyBrushes.SelectBackBrush; }

            if(!label.IsNote)
                label.Foreground = MyBrushes.SelectForgrBrush;
            
        }
        void UnSelectLabel(MyLabel label)
        {
            if (label.IsLock)
            {
                label.Background = MyBrushes.LockBackBrush;
                label.Foreground = MyBrushes.UnSelectForgrBrush;
            }
            else
            {
                if (errList.Contains(label) && errorcheck)
                {
                    label.Background = MyBrushes.ErrorBackBrush;
                }
                else
                    label.Background = MyBrushes.UnLockBackBrush;
            }
        }
        #endregion

        

        
    }
}
