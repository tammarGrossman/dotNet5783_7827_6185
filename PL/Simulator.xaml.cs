using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using sim= Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {

        BackgroundWorker worker;
        public Simulator()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            sim.Simulator.RegisterOrderStatusEvent(ChangeStatusOfOrder);
            sim.Simulator.UnRegisterEndEvent(SimulatorEnded)

            sim.Simulator.initilize();
            while(!worker.CancellationPending)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(0,DateTime.Now);
            }
        }
        private void ChangeStatusOfOrder(object? sender, OrderStatusUpdateEventArgs e)
        {
            worker.ReportProgress(1, e);
        }

        private void SimulatorEnded(object? sender, EventArgs e)
        {
            worker.CancelAsync();
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage==0)
            {
                clock.Text = e.UserState.ToString();
            }
            else if (e.ProgressPercentage == 1)
            {
                //orderID.Text =;
                //currentStatus.Text=;
                //nextStatus.Text=;
                //estimatedTime=;
                //update ui
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sim.Simulator.UnRegisterOrderStatusEvent(ChangeStatusOfOrder);
            sim.Simulator.UnRegisterEndEvent(ChangeStatusOfOrder);
            //unregister end event
        }

    }
}
