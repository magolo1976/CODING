using Microsoft.ML.Data;

namespace MTT_Weka
{
    public class ModelInput
    {
        [LoadColumn(0)]
        public int Hora { get; set; }

        [LoadColumn(1)]
        public double Mom7 { get; set; }

        [LoadColumn(2)]
        public double Mom14 { get; set; }

        [LoadColumn(3)]
        public double Mom28 { get; set; }

        [LoadColumn(4)]
        public double ADX7 { get; set; }

        [LoadColumn(5)]
        public double ADX14 { get; set; }

        [LoadColumn(6)]
        public double ADX21 { get; set; }

        [LoadColumn(7)]
        public double MA7 { get; set; }

        [LoadColumn(8)]
        public double MA14 { get; set; }

        [LoadColumn(9)]
        public double MA21 { get; set; }

        [LoadColumn(10)]
        public double CCI7 { get; set; }

        [LoadColumn(11)]
        public double CCI14 { get; set; }

        [LoadColumn(12)]
        public double CCI21 { get; set; }

        [LoadColumn(13)]
        public double RSI2 { get; set; }

        [LoadColumn(14)]
        public double RSI14 { get; set; }

        [LoadColumn(15)]
        public double Close1_Close2 { get; set; }

        [LoadColumn(16)]
        public double Close1_Close3 { get; set; }

        [LoadColumn(17)]
        public double Close1_Close4 { get; set; }

        [LoadColumn(18)]
        public double Low1_Low2 { get; set; }

        [LoadColumn(19)]
        public double Low1_Low3 { get; set; }

        [LoadColumn(20)]
        public double Low1_Low4 { get; set; }

        [LoadColumn(21)]
        public double High1_High2 { get; set; }

        [LoadColumn(22)]
        public double High1_High3 { get; set; }

        [LoadColumn(23)]
        public double High1_High4 { get; set; }

        [LoadColumn(24)]
        public double Vol1_Vol2 { get; set; }

        [LoadColumn(25)]
        public double EaseOfMovement { get; set; }

        [LoadColumn(26)]            
        public double Volatility30 { get; set; }

        [LoadColumn(27)]
        public double VolStdDev30 { get; set; }

        [LoadColumn(28)]
        public double PriceVolTrend21 { get; set; }

        [LoadColumn(29)]
        public double PriceVolTrend2 { get; set; }

        [LoadColumn(30)]
        public double Aroon9 { get; set; }

        [LoadColumn(31)]
        public double Aroon14 { get; set; }

        [LoadColumn(32)]
        public double Aroon21 { get; set; }

        [LoadColumn(33)]
        public double Aroon25 { get; set; }

        [LoadColumn(34)]
        public double ChaikinOsc3_10 { get; set; }

        [LoadColumn(35)]
        public double ChaikinMF9 { get; set; }

        [LoadColumn(36)]
        public double ChaikinMF14 { get; set; }

        [LoadColumn(37)]
        public double Close1_Close15 { get; set; }

        [LoadColumn(38)]
        public double Close1_Close22 { get; set; }

        [LoadColumn(39)]
        public double Close1_Close29 { get; set; }

        [LoadColumn(40)]
        public double TRIX9 { get; set; }

        [LoadColumn(41)]
        public double TRIX21 { get; set; }

        [LoadColumn(42)]
        public double C_L14 { get; set; }

        [LoadColumn(43)]
        public double C_H14 { get; set; }

        [LoadColumn(44)]
        public double DI_plus_14 { get; set; }

        [LoadColumn(45)]
        public double DI_minus_14 { get; set; }

        [LoadColumn(46)]
        public double DI_plus_21 { get; set; }

        [LoadColumn(47)]
        public double DI_minus_21 { get; set; }

        [LoadColumn(48)]
        public double DI_plus_7 { get; set; }

        [LoadColumn(49)]
        public double DI_minus_7 { get; set; }

        [LoadColumn(50)]
        public double Percentile7 { get; set; }

        [LoadColumn(51)]
        public double Percentile14 { get; set; }

        [LoadColumn(52)]
        public double Percentile21 { get; set; }

        [LoadColumn(53)]
        public double Kurtosis7 { get; set; }

        [LoadColumn(54)]
        public double Kurtosis14 { get; set; }


        [LoadColumn(55)]
        public double Kurtosis21 { get; set; }

        [LoadColumn(56)]
        public double LReg7 { get; set; }

        [LoadColumn(57)]
        public double LReg14 { get; set; }

        [LoadColumn(58)]
        public double LReg21 { get; set; }

        [LoadColumn(59)]
        public double HeikenLine1_7 { get; set; }

        [LoadColumn(60)]
        public double VWAPday { get; set; }

        [LoadColumn(61)]
        public double VWAPweek { get; set; }

        [LoadColumn(62)]
        public double VWAPmonth { get; set; }

        [LoadColumn(62)]
        public double WP { get; set; }

        [LoadColumn(64), ColumnName("Label")]
        public string LABEL { get; set; }
    }
}
