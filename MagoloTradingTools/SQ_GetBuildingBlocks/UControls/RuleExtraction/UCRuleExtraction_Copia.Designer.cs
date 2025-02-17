namespace MTT01_winforms.UControls.RuleExtraction
{
    partial class UCRuleExtraction_Copia
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        /*
<!-- MainControl.xaml -->
<UserControl x:Class="RuleExtractor.MainControl"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:oxy="http://oxyplot.org/wpf"
             mc:Ignorable="d" 
             d:DesignHeight="768" d:DesignWidth="1024">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <!-- Header -->
        <StackPanel Grid.Row="0" Margin="10">
            <TextBlock Text="🤖 RULE EXTRACTOR" FontSize="24" FontWeight="Bold"/>
            <Separator/>
        </StackPanel>
        
        <!-- Main Content -->
        <TabControl Grid.Row="1" Margin="10">
            <!-- Data Tab -->
            <TabItem Header="Data" Name="tabData">
                <ScrollViewer>
                    <StackPanel Margin="10">
                        <Button Name="BtnLoadCSV" Content="Load CSV File" Click="BtnLoadCSV_Click" Width="150" HorizontalAlignment="Left"/>
                        <TextBlock Text="Data Preview" FontWeight="Bold" Margin="0,20,0,5"/>
                        <DataGrid Name="dataPreviewGrid" Height="200" IsReadOnly="True"/>
                        
                        <TextBlock Text="Period Selection" FontWeight="Bold" Margin="0,20,0,5"/>
                        <Label Name="lblDateRange" Content="Available date range: "/>
                        
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="*"/>
                                <ColumnDefinition Width="*"/>
                                <ColumnDefinition Width="*"/>
                            </Grid.ColumnDefinitions>
                            
                            <!-- Test Period -->
                            <StackPanel Grid.Column="0" Margin="5">
                                <TextBlock Text="Test" FontWeight="Bold"/>
                                <Label Content="Start:"/>
                                <DatePicker Name="dtpTestStart"/>
                                <Label Content="End:"/>
                                <DatePicker Name="dtpTestEnd"/>
                            </StackPanel>
                            
                            <!-- Train Period -->
                            <StackPanel Grid.Column="1" Margin="5">
                                <TextBlock Text="Train" FontWeight="Bold"/>
                                <Label Content="Start:"/>
                                <DatePicker Name="dtpTrainStart"/>
                                <Label Content="End:"/>
                                <DatePicker Name="dtpTrainEnd"/>
                            </StackPanel>
                            
                            <!-- Forward Period -->
                            <StackPanel Grid.Column="2" Margin="5">
                                <TextBlock Text="Forward" FontWeight="Bold"/>
                                <Label Content="Start:"/>
                                <DatePicker Name="dtpForwardStart"/>
                                <Label Content="End:"/>
                                <DatePicker Name="dtpForwardEnd"/>
                            </StackPanel>
                        </Grid>
                        
                        <TextBlock Text="Price Evolution" FontWeight="Bold" Margin="0,20,0,5"/>
                        <oxy:PlotView Name="priceEvolutionChart" Height="300"/>
                        
                        <TextBlock Text="Returns Statistics" FontWeight="Bold" Margin="0,20,0,5"/>
                        <DataGrid Name="returnsStatsGrid" Height="100" IsReadOnly="True"/>
                        
                        <TextBlock Text="Train-Test Distribution Comparison" FontWeight="Bold" Margin="0,20,0,5"/>
                        <oxy:PlotView Name="ksTestChart" Height="300"/>
                        
                        <TextBlock Text="Returns Magnitude Analysis (Train + Test)" FontWeight="Bold" Margin="0,20,0,5"/>
                        <oxy:PlotView Name="waterfallChart" Height="300"/>
                    </StackPanel>
                </ScrollViewer>
            </TabItem>
            
            <!-- Feature Selection Tab -->
            <TabItem Header="Feature Selection" Name="tabFeatures" IsEnabled="False">
                <Grid Margin="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="*"/>
                    </Grid.RowDefinitions>
                    
                    <StackPanel Grid.Row="0">
                        <Grid>
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="*"/>
                                <ColumnDefinition Width="*"/>
                            </Grid.ColumnDefinitions>
                            
                            <StackPanel Grid.Column="0" Margin="0,0,10,0">
                                <Label Content="Select Side:"/>
                                <ComboBox Name="cmbSide" SelectedIndex="0">
                                    <ComboBoxItem>long</ComboBoxItem>
                                    <ComboBoxItem>short</ComboBoxItem>
                                </ComboBox>
                            </StackPanel>
                            
                            <StackPanel Grid.Column="1">
                                <Label Content="Correlation Threshold:"/>
                                <Slider Name="sldCorrelation" Minimum="0.75" Maximum="1.0" Value="0.95" 
                                        TickFrequency="0.01" IsSnapToTickEnabled="True"/>
                                <TextBlock Text="{Binding ElementName=sldCorrelation, Path=Value, StringFormat=N2}"/>
                            </StackPanel>
                        </Grid>
                        
                        <Button Name="BtnAnalyzeFeatures" Content="Analyze Features" Click="BtnAnalyzeFeatures_Click" 
                                Width="150" HorizontalAlignment="Left" Margin="0,10,0,0"/>
                        <ProgressBar Name="progressFeatures" Height="5" Margin="0,5,0,0" Visibility="Collapsed"/>
                    </StackPanel>
                    
                    <TextBlock Grid.Row="1" Text="Selected Features:" FontWeight="Bold" Margin="0,20,0,5"/>
                    <DataGrid Grid.Row="2" Name="featuresGrid" IsReadOnly="True"/>
                </Grid>
            </TabItem>
            
            <!-- Rule Extraction Tab -->
            <TabItem Header="Rule Extraction" Name="tabRules" IsEnabled="False">
                <Grid Margin="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="*"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                    </Grid.RowDefinitions>
                    
                    <oxy:PlotView Grid.Row="0" Name="monkeyDistributionChart" Height="200"/>
                    
                    <StackPanel Grid.Row="1" Margin="0,10,0,0">
                        <Label Content="Select a base feature:"/>
                        <ComboBox Name="cmbBaseFeature"/>
                        <Button Name="BtnFindRules" Content="Find Rules" Click="BtnFindRules_Click" 
                                Width="150" HorizontalAlignment="Left" Margin="0,10,0,0"/>
                        <ProgressBar Name="progressRules" Height="5" Margin="0,5,0,0" Visibility="Collapsed"/>
                        <TextBlock Name="lblRuleProgress" Margin="0,5,0,0"/>
                    </StackPanel>
                    
                    <DataGrid Grid.Row="2" Name="rulesGrid" IsReadOnly="True" Margin="0,10,0,0"/>
                    
                    <Label Grid.Row="3" Content="Select a rule to view its evolution:" Margin="0,10,0,0"/>
                    <oxy:PlotView Grid.Row="4" Name="ruleEvolutionChart" Height="300"/>
                </Grid>
            </TabItem>
            
            <!-- Validation Tab -->
            <TabItem Header="Validation" Name="tabValidation" IsEnabled="False" Selected="TabValidation_Selected">
                <Grid Margin="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="*"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                    </Grid.RowDefinitions>
                    
                    <ProgressBar Grid.Row="0" Name="progressValidation" Height="5" Visibility="Collapsed"/>
                    
                    <DataGrid Grid.Row="1" Name="validationGrid" IsReadOnly="True" Margin="0,10,0,0"/>
                    
                    <Label Grid.Row="2" Content="Select a rule to view its evolution in test:" Margin="0,10,0,0"/>
                    <oxy:PlotView Grid.Row="3" Name="validationRuleChart" Height="300"/>
                </Grid>
            </TabItem>
            
            <!-- Forward Tab -->
            <TabItem Header="Forward" Name="tabForward" IsEnabled="False">
                <Grid Margin="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="*"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                    </Grid.RowDefinitions>
                    
                    <StackPanel Grid.Row="0">
                        <Label Content="Validation Threshold:"/>
                        <Slider Name="sldValidation" Minimum="0" Maximum="100" Value="90" 
                                TickFrequency="5" IsSnapToTickEnabled="True" ValueChanged="SldValidation_ValueChanged"/>
                        <TextBlock Text="{Binding ElementName=sldValidation, Path=Value, StringFormat=N1}%"/>
                    </StackPanel>
                    
                    <TextBlock Grid.Row="1" Text="Rules above validation threshold:" FontWeight="Bold" Margin="0,10,0,5"/>
                    <DataGrid Grid.Row="2" Name="filteredRulesGrid" IsReadOnly="True"/>
                    
                    <Label Grid.Row="3" Content="Select a rule to view its evolution in forward:" Margin="0,10,0,0"/>
                    <oxy:PlotView Grid.Row="4" Name="forwardRuleChart" Height="300"/>
                </Grid>
            </TabItem>
            
            <!-- Backtest Tab -->
            <TabItem Header="Backtest" Name="tabBacktest" IsEnabled="False">
                <Grid Margin="10">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="*"/>
                    </Grid.RowDefinitions>
                    
                    <StackPanel Grid.Row="0">
                        <Label Content="Select a rule for backtest:"/>
                        <ComboBox Name="cmbRuleForBacktest" SelectionChanged="CmbRuleForBacktest_SelectionChanged"/>
                    </StackPanel>
                    
                    <Grid Grid.Row="1" Margin="0,10,0,0">
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="*"/>
                        </Grid.ColumnDefinitions>
                        
                        <GroupBox Grid.Column="0" Header="Trading Metrics" Margin="0,0,5,0">
                            <StackPanel>
                                <TextBlock Name="lblTotalTrades" Text="Number of trades: "/>
                                <TextBlock Name="lblTotalReturn" Text="Total return: "/>
                                <TextBlock Name="lblAvgReturn" Text="Average return: "/>
                                <TextBlock Name="lblWinRate" Text="Win rate: "/>
                            </StackPanel>
                        </GroupBox>
                        
                        <GroupBox Grid.Column="1" Header="Risk Metrics" Margin="5,0,0,0">
                            <StackPanel>
                                <TextBlock Name="lblSharpe" Text="Sharpe ratio: "/>
                                <TextBlock Name="lblMaxDD" Text="Maximum drawdown: "/>
                                <TextBlock Name="lblBestTrade" Text="Best trade: "/>
                                <TextBlock Name="lblWorstTrade" Text="Worst trade: "/>
                            </StackPanel>
                        </GroupBox>
                    </Grid>
                    
                    <TextBlock Grid.Row="2" Text="Equity Curve" FontWeight="Bold" Margin="0,20,0,5"/>
                    <oxy:PlotView Grid.Row="2" Name="equityCurveChart" Height="250"/>
                    
                    <TextBlock Grid.Row="3" Text="Returns Distribution" FontWeight="Bold" Margin="0,20,0,5"/>
                    <oxy:PlotView Grid.Row="3" Name="returnsDistChart" Height="250"/>
                </Grid>
            </TabItem>
        </TabControl>
    </Grid>
</UserControl>         
         */
        #endregion
    }
}
