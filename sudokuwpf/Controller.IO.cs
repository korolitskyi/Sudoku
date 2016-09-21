using ClassLibrary;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SudokuWPF
{
    public partial class Controller
    {
        BinaryFormatter binFormat = new BinaryFormatter();
        Label timeLabel;
        List<string> stringNoteList = new List<string>();

        bool OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    OpenGrid(openFileDialog.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    mainWindow.ShowMessageAsync("Помилка", "При відкритті файла сталася помилка.\nПеревірте цілісність файла та спробуйте ще раз");
                    return false;
                }
            }
            return false;

        }

        void OpenGrid(string fileName)
        {
                Stream fStream = File.OpenRead(fileName);
                ArrayList objGraph = (ArrayList)binFormat.Deserialize(fStream);
                values = (int[,])objGraph[0];
                result = (int[,])objGraph[1];
                timer = (MyTimer)objGraph[2];
                timer.InitializeMyTimer(timeLabel);
                finishBanner.Timer = timer;
                stringNoteList = (List<string>)objGraph[3];
                errorCount = (int)objGraph[4];
                finishBanner.dificulty = objGraph[5].ToString();
            
        }

        void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "dat files (*.dat)|*.dat|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            if (saveFileDialog.ShowDialog() == true)
            {
                int[,] copyValues = new int[9,9];
                Array.Copy(values, copyValues, values.Length);
                foreach (MyLabel e in CellsArray)
                {
                    if (!e.IsLock)
                        copyValues[e.Row, e.Column] *= 10;
                }
                List<string> noteStringList = new List<string>();
                foreach (MyLabel e in NoteList)
                {
                    noteStringList.Add(e.Row.ToString()+ ", " + e.Column.ToString()+ ", "  + e.Content.ToString());
                }
                ArrayList serValues = new ArrayList(3) { copyValues, result, timer, noteStringList, errorCount, finishBanner.dificulty };
                SaveValues(serValues, saveFileDialog.FileName);
            }
        }

        void SaveValues(object objValues, string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            binFormat.Serialize(fStream, objValues);
            
        }
    }
}
