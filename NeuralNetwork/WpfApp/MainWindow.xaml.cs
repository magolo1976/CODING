using Microsoft.Win32;
using ScottPlot.Plottables;
using StudyCaseLibrary;
using StudyCaseLibrary.Models;
using System.Drawing;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Scatter[] PlotViewScatters;

    private DispatcherTimer Timer;

    private StudyCase StudyCase = new StudyCase();

    public MainWindow()
    {
        InitializeComponent();

        InitializePlotView();

        InitializeTimer(5);
    }

    private void btnTrain_Click(object sender, RoutedEventArgs e)
    {
        StudyCase.OnGetTrainingDataEvent += StudyCase_OnGetTrainingDataEvent;
        StudyCase.OnTrainingEvent += StudyCase_OnTrainingEvent;
        StudyCase.OnPredictionEvent += StudyCase_OnPredictionEvent;

        int Epochs = int.Parse(TxtEpochs.Text);
        double LearningRates = double.Parse(TxtLearningRate.Text);

        string filePath = "C:\\MAGOLO\\gitCODING\\NeuralNetwork\\StudyCaseLibrary\\Assets\\StudyData2.csv";

        Task.Run(() => StudyCase.Train(Epochs, LearningRates, filePath));
    }

    private void btnTest_Click(object sender, RoutedEventArgs e)
    {
        var Result = StudyCase.Test();

        WriteLine($"Total predictions: {Result.TotalPrediction}");
        WriteLine($"Correct predictions: {Result.CorrectPredictions}");
        WriteLine($"Accuracy: { Result.Accuracy}%");
    }

    private void btnPredict_Click(object sender, RoutedEventArgs e)
    {
        var Values = TxtValues.Text.Split(",", StringSplitOptions.TrimEntries);

        double Study = StudyCase.NormalizeDateTime(DateTime.Parse(Values[0]));
        double Sleep = StudyCase.NormalizeDateTime(DateTime.Parse(Values[1]));

        double Predicted = StudyCase.Predict(Study, Sleep);

        string Text = $"{(Predicted > 0.5 ? "Aprobado" : "Suspenso")} ({Predicted:f6})";

        WriteLine(Text);

        var Scatter = PlotView.Plot.Add.Scatter(
            Study * 595, Sleep * 595);
        Scatter.LineColor = ScottPlot.Color.FromColor(Color.DarkBlue);
        Scatter.MarkerSize = 7;

        PlotView.Refresh();
    }
    
    private void btnSavemodel_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dlg = new SaveFileDialog
        {
            Title = "Save model",
            Filter = "Models (*.json)|*.json",
            FileName = "StudyModel",
            DefaultExt = ".json"
        };

        if (dlg.ShowDialog() == true)
        {
            StudyCase.SaveModel(dlg.FileName);
        }
    }

    private void btnLoadmodel_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog dlg = new OpenFileDialog
        {
            Title = "Load model",
            Filter = "Models (*.json)|*.json",
            FileName = "StudyModel",
            DefaultExt = ".json",
            Multiselect = false
        };

        if (dlg.ShowDialog() == true)
        {
            StudyCase.LoadModel(dlg.FileName);

            InitializePlotView();
        }
    }

    private void StudyCase_OnPredictionEvent(int index, double[] prediction)
    {
        var Scatter = PlotViewScatters[index];
        Scatter.Color = ScottPlot.Color.FromColor(
            prediction[0] > .5 ? Color.Green : Color.Red
            );
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

            //var Line = PlotView.Plot.Add.Line(235, 415, 235, 600);
            //Line.Color = ScottPlot.Color.FromColor(Color.Orange);
            //Line.LineWidth = 5;

            //Line = PlotView.Plot.Add.Line(235, 415, 600, 415);
            //Line.Color = ScottPlot.Color.FromColor(Color.Orange);
            //Line.LineWidth = 5;

            PlotView.Refresh();
        });
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        double Size = PlotViewGrid.ActualHeight;
        PlotView.Width = Size;
        PlotView.Height = Size;
    }

    private void InitializeTimer(int seconds)
    {
        Timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(seconds)
        };

        Timer.Tick += (s, e) =>
        {
            Dispatcher.Invoke(() => PlotView.Refresh());
        };

        Timer.Start();
    }

    private void InitializePlotView()
    {
        PlotView.Plot.Axes.SetLimits(0, 600, 0, 600);
        PlotView.Plot.Clear();
        PlotView.Refresh();
    }

    private void Write(string text)
    {
        ConsoleTextBox.AppendText(text);
        ConsoleTextBox.ScrollToEnd();
    }

    private void WriteLine(string text) => Write($"{text}\n");

}