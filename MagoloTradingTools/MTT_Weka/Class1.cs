using System;
using Microsoft.ML;
using Microsoft.ML.Data;


namespace MTT_Weka
{
    public class Class1
    {
        public void EntrenarModelo()
        {
            //int semilla = 1;

            //var mlContext = new MLContext(seed: semilla);

            //// Cargar los datos
            //var dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
            //    path: "C://PicNetML_FILE.csv",
            //    hasHeader: true,
            //    separatorChar: ',');

            //// Dividir en datos de entrenamiento y prueba
            //var dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.3);
            //var trainData = dataSplit.TrainSet;
            //var testData = dataSplit.TestSet;

            //// Definir el pipeline de entrenamiento
            //var pipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: "Label")
            //    .Append(mlContext.Transforms.Concatenate("Features", "Hora", "Mom7", "Mom14", "Mom28", "ADX7", "ADX14"))
            //    .Append(mlContext.MulticlassClassification.Trainers.DecisionTree(labelColumnName: "Label", featureColumnName: "Features"))
            //    .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            //// Entrenar el modelo
            //var model = pipeline.Fit(trainData);

            //// Evaluar el modelo
            //var predictions = model.Transform(testData);
            //var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

            //// Mostrar métricas
            //Console.WriteLine($"Log-loss: {metrics.LogLoss}");

            //// Guardar el modelo
            //mlContext.Model.Save(model, dataView.Schema, "model.zip");





            //// Carga el modelo entrenado
            //var trainedModel = mlContext.Model.Load("model.zip", out var modelInputSchema);

            //// Crea un motor de predicción
            //var predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

            //// Crea una instancia de datos de entrada para la predicción
            //var sampleData = new ModelInput()
            //{
            //    Hora = 2300,
            //    Mom7 = 99.89f,
            //    Mom14 = 100.03f,
            //    Mom28 = 99.61f,
            //    ADX7 = 33.87f,
            //    ADX14 = 31.54f
            //};

            //// Realiza la predicción
            //var predictionResult = predictionEngine.Predict(sampleData);

            //// Muestra los resultados de la predicción
            //Console.WriteLine($"Predicción: {predictionResult.Prediction}");
            //Console.WriteLine($"Probabilidad: {predictionResult.Probability:P2}");
            //Console.WriteLine($"Puntaje: {predictionResult.Score}");
        }
    }
}
