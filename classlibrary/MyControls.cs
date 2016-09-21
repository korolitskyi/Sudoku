using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ClassLibrary
{
    /// <summary>
    /// Клас який наслідується від System.Windows.Controls.Label і включає в себе 
    /// метод задання позиції у двовимірному масиві, та метод отримання кольору
    /// </summary>
    public class MyLabel : Label
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        /// <summary>
        /// Задання позиції мітки в двовимірному масиві
        /// </summary>
        public void SetPosition(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
        }

        public MyLabel(int i, int j)
        {
            SetPosition(i, j);
            Name = "Label" + i + "_" + j;
            this.Margin = new Thickness(1);
        }

        void SetFonts(bool value)
        {
            if (!value)
            {
                this.Foreground = MyBrushes.SelectForgrBrush;
                this.FontSize = 28;
                this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                this.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            }
            else
            {
                this.Foreground = new SolidColorBrush(Colors.Red);
                this.FontSize = 12;
                this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                this.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            }
        }

        

        /// <summary>
        /// Властивість що показує чи наш Label замітка
        /// </summary>
        public bool IsNote
        {
            get { return isNote; }
            set
            {
                SetFonts(value);
                isNote = value;
            }
        }
        bool isNote;

        /// <summary>
        /// Властивість що показує чи можна змінювати данні в Label
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// Отримання кольору SolidColorBrush
        /// </summary>
        public Color GetColor()
        {
            return (Color)this.Background.GetValue(SolidColorBrush.ColorProperty);
        }
    }

    /// <summary>
    /// Клас який наслідується від System.Windows.Controls.Tile і включає в себе 
    /// анімацію при наведенні курсору
    /// </summary>
    public class MyTile : Tile
    {
        public MyTile()
        {
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }
        protected void OnMouseEnter(object obj, MouseEventArgs e)
        {
            this.Margin = new Thickness(this.Margin.Left + 2, this.Margin.Top + 2, this.Margin.Right + 2, this.Margin.Bottom + 2);
            e.Handled = true;

        }
        protected void OnMouseLeave(object obj, MouseEventArgs e)
        {
            this.Margin = new Thickness(this.Margin.Left - 2, this.Margin.Top - 2, this.Margin.Right - 2, this.Margin.Bottom - 2);
            e.Handled = true;
        }
    }

    /// <summary>
    /// Клас який наслідується від ClassLibrary.MyTile і включає в себе 
    /// інформацію про мін. і макс. к-сть прихованих елементів, а також назву важкості
    /// </summary>
    public class MyNamedTile : MyTile
    {
        public int MinDificulty { get; private set; }
        public int MaxDificulty { get; private set; }

        public void SetDificulty(int minValue, int maxValue, string nameDificulty)
        {
            MinDificulty = minValue;
            MaxDificulty = maxValue;
        }

    }
}
