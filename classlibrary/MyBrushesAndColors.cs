using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClassLibrary
{
    public static class MyBrushes
    {
        public static SolidColorBrush ErrorBackBrush { get { return new SolidColorBrush(MyColors.ErrorBackColor); } }
        public static SolidColorBrush LockBackBrush { get { return new SolidColorBrush(MyColors.LockBackColor); } }
        public static SolidColorBrush UnLockBackBrush { get { return new SolidColorBrush(MyColors.UnLockBackColor); } }
        public static SolidColorBrush SelectBackBrush { get { return new SolidColorBrush(MyColors.SelectBackColor); } }
        public static SolidColorBrush SelectForgrBrush { get { return new SolidColorBrush(MyColors.SelectForgrColor); } }
        public static SolidColorBrush UnSelectForgrBrush { get { return new SolidColorBrush(MyColors.UnSelectForgrColor); } }
    }

    public static class MyColors
    {
        public static Color ErrorBackColor { get { return Colors.Red; } }
        public static Color LockBackColor { get { return Colors.DodgerBlue; } }
        public static Color UnLockBackColor { get { return Color.FromRgb(166, 207, 232); } }
        public static Color SelectBackColor { get { return Color.FromRgb(228, 221, 156); } }
        public static Color SelectForgrColor { get { return Color.FromRgb(92, 92, 92); } }
        public static Color UnSelectForgrColor { get { return Colors.White; } }
    }
}
