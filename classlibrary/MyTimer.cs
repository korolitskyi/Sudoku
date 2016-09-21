using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ClassLibrary
{
    [Serializable]
    public class MyTimer
    {
        [NonSerialized]
        Label timerLabel;
        [NonSerialized]
        DispatcherTimer dispatcherTime;

        public Label TimerLabel { get { return timerLabel; } }

        private int minute = 0;
        private int second = 0;
        private int clock = 0;
                
        
        public MyTimer(Label timeLabel)
        {
            InitializeMyTimer(timeLabel);
        }

        public void InitializeMyTimer(Label timeLabel)
        {
            this.timerLabel = timeLabel;
            timerLabel.Content = ToString();
            dispatcherTime = new DispatcherTimer();
            dispatcherTime.Interval = new TimeSpan(0, 0, 1);
            dispatcherTime.Tick += new EventHandler(dispatcherTimer_Tick);
        }

        public void Start()
        {
            dispatcherTime.Start();
        }

        public void Stop()
        {
            dispatcherTime.Stop();
        }

        public void Clear()
        {
            clock = 0;
            second = 0;
            minute = 0;
            timerLabel.Content = ToString();
        }

        public override string ToString()
        {
            string secondstring = second < 10 ? "0" + second.ToString() : second.ToString();
            string minutestring = minute < 10 ? "0" + minute.ToString() : minute.ToString();
            return minutestring + ":" + secondstring;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            clock++;
            minute = clock / 60;
            second = clock % 60;

            timerLabel.Content = ToString();
        }
    }
}
