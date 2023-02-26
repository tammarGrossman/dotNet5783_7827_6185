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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using sim = Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {

        BackgroundWorker worker;
        public SimulatorWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            sim.Simulator.RegisterOrderStatusEvent(ChangeStatusOfOrder);
            sim.Simulator.RegisterEndEvent(SimulatorEnded);

            sim.Simulator.initilize();
            while (!worker.CancellationPending)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(0, DateTime.Now);
            }
        }


        private void ChangeStatusOfOrder(object? sender, OrderStatusUpdateEventArgs e)
        {
            worker.ReportProgress(1, e);
        }

        private void SimulatorEnded(object? sender, EventArgs e)
        {
            worker.CancelAsync();
            MessageBox.Show("Bye Bye");
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
            if (e.ProgressPercentage == 0)
            {
                clock.Text = DateTime.Now.ToLongTimeString();
            }
            else if (e.ProgressPercentage == 1)
            {
                OrderStatusUpdateEventArgs e2 = (OrderStatusUpdateEventArgs)e.UserState!;

                if (e2.OrderId != 0)
                {
                    noUpdateOrders.Visibility = Visibility.Hidden;
                    orderID.Visibility = Visibility.Visible;
                    currentStatus.Visibility = Visibility.Visible;
                    nextStatus.Visibility = Visibility.Visible;
                    estimatedTime.Visibility = Visibility.Visible;
                    lblorderID.Visibility = Visibility.Visible;
                    lblcurrentStatus.Visibility = Visibility.Visible;
                    lblnextStatus.Visibility = Visibility.Visible;
                    lblestimatedTime.Visibility = Visibility.Visible;
                    orderID.Text = e2.OrderId.ToString();
                    currentStatus.Text = e2.CurrentStatus.ToString();
                    nextStatus.Text = e2.NextStatus.ToString();
                    estimatedTime.Text = e2.EstimatedTime.ToLongTimeString();
                }
                else
                {
                    noUpdateOrders.Visibility = Visibility.Visible;
                    orderID.Visibility = Visibility.Hidden;
                    currentStatus.Visibility = Visibility.Hidden;
                    nextStatus.Visibility = Visibility.Hidden;
                    estimatedTime.Visibility = Visibility.Hidden;
                    lblorderID.Visibility = Visibility.Hidden;
                    lblcurrentStatus.Visibility = Visibility.Hidden;
                    lblnextStatus.Visibility = Visibility.Hidden;
                    lblestimatedTime.Visibility = Visibility.Hidden;
                }
            }
           
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sim.Simulator.UnRegisterOrderStatusEvent(ChangeStatusOfOrder);
            sim.Simulator.UnRegisterEndEvent(SimulatorEnded);

        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            sim.Simulator.stop();
            this.Close();
            new Thanks().Show();
            
        }

        private void currentStatus_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
