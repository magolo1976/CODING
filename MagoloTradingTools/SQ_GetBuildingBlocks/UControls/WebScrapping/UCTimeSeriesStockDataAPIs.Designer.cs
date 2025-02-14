namespace MTT01_winforms.UControls.WebScrapping
{
    partial class UCTimeSeriesStockDataAPIs
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
            ListSearchEndpoint = new ListBox();
            TxtSearchEndpoint = new TextBox();
            SuspendLayout();
            // 
            // ListSearchEndpoint
            // 
            ListSearchEndpoint.FormattingEnabled = true;
            ListSearchEndpoint.HorizontalScrollbar = true;
            ListSearchEndpoint.ItemHeight = 15;
            ListSearchEndpoint.Location = new Point(20, 97);
            ListSearchEndpoint.Name = "ListSearchEndpoint";
            ListSearchEndpoint.Size = new Size(310, 394);
            ListSearchEndpoint.TabIndex = 1;
            // 
            // TxtSearchEndpoint
            // 
            TxtSearchEndpoint.Location = new Point(19, 69);
            TxtSearchEndpoint.Name = "TxtSearchEndpoint";
            TxtSearchEndpoint.Size = new Size(311, 23);
            TxtSearchEndpoint.TabIndex = 2;
            TxtSearchEndpoint.KeyUp += TxtSearchEndpoint_KeyUp;
            // 
            // UCTimeSeriesStockDataAPIs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TxtSearchEndpoint);
            Controls.Add(ListSearchEndpoint);
            Name = "UCTimeSeriesStockDataAPIs";
            Size = new Size(1193, 868);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ListSearchEndpoint;
        private TextBox TxtSearchEndpoint;
    }
}
