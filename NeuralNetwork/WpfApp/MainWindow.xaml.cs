using ScottPlot.Plottables;
using StudyCaseLibrary;
using StudyCaseLibrary.Models;
using System.Drawing;
using System.Windows;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Scatter[] PlotViewScatters;

    public MainWindow()
    {
        InitializeComponent();

        PlotView.Plot.Axes.SetLimits(0, 600, 0, 600);
    }

    private void btnTrain_Click(object sender, RoutedEventArgs e)
    {
        var StudyCase = new StudyCaseNeuralNetworkExample();
        StudyCase.OnGetTrainingDataEvent += StudyCase_OnGetTrainingDataEvent;
        StudyCase.OnTrainingEvent += StudyCase_OnTrainingEvent;
        StudyCase.OnPredictionEvent += StudyCase_OnPredictionEvent;

        Task.Run(() => StudyCase.ExecuteExample());
    }

    private void StudyCase_OnPredictionEvent(int index, double[] prediction)
    {
        Dispatcher.Invoke(() =>
        {
            var Scatter = PlotViewScatters[index];
            Scatter.Color = ScottPlot.Color.FromColor(
                prediction[0] > .5 ? Color.Green : Color.Red
                );

            if (index % 3500 == 0)
                PlotView.Refresh();
        });
    }

    private void StudyCase_OnTrainingEvent(OnTrainingEventArgs args)
    {
        Dispatcher.Invoke(() =>
        {
            string Text = string.Format("Epochs: {0}, Current Epoch: {1}, MSE: {2:F4}\n",
                args.TotalEpochs, args.CurrentEpoch, args.Mse
            );

            ConsoleTextBox.AppendText(Text);
            ConsoleTextBox.ScrollToEnd();
        });
    }

    private void StudyCase_OnGetTrainingDataEvent(double[][] trainigData)
    {
        Dispatcher.Invoke(() =>
        {
            PlotViewScatters = new Scatter[trainigData.Length];
            for(int i=0; i< trainigData.Length; i++)
            {
                var Scatter = PlotView.Plot.Add.Scatter(
                    trainigData[i][0] * 595, trainigData[i][1] * 595);
                Scatter.MarkerSize = 5;
                Scatter.Color = ScottPlot.Color.FromColor(Color.Black);
                PlotViewScatters[i] = Scatter;
            }


            var Line = PlotView.Plot.Add.Line(235, 415, 235, 600);
            Line.Color = ScottPlot.Color.FromColor(Color.Orange);
            Line.LineWidth = 5;

            Line = PlotView.Plot.Add.Line(235, 415, 600, 415);
            Line.Color = ScottPlot.Color.FromColor(Color.Orange);
            Line.LineWidth = 5;

            PlotView.Refresh();
        });
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double Size = PlotViewGrid.ActualHeight;
        PlotView.Width = Size;
        PlotView.Height = Size;
    }
}