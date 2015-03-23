namespace TicketRobot
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.SpiderButton = new System.Windows.Forms.Button();
            this.OrderButton = new System.Windows.Forms.Button();
            this.timeTableGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.timeTableGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.webBrowser.Location = new System.Drawing.Point(0, 157);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 120);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1182, 616);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.Visible = false;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // SpiderButton
            // 
            this.SpiderButton.Location = new System.Drawing.Point(501, 56);
            this.SpiderButton.Name = "SpiderButton";
            this.SpiderButton.Size = new System.Drawing.Size(120, 23);
            this.SpiderButton.TabIndex = 2;
            this.SpiderButton.Text = "Execute - Flight Table";
            this.SpiderButton.UseVisualStyleBackColor = true;
            this.SpiderButton.Click += new System.EventHandler(this.SpiderButton_Click);
            // 
            // OrderButton
            // 
            this.OrderButton.Location = new System.Drawing.Point(501, 12);
            this.OrderButton.Name = "OrderButton";
            this.OrderButton.Size = new System.Drawing.Size(120, 23);
            this.OrderButton.TabIndex = 3;
            this.OrderButton.Text = "Execute-Order";
            this.OrderButton.UseVisualStyleBackColor = true;
            this.OrderButton.Click += new System.EventHandler(this.OrderButton_Click);
            // 
            // timeTableGridView
            // 
            this.timeTableGridView.Location = new System.Drawing.Point(0, 113);
            this.timeTableGridView.Name = "timeTableGridView";
            this.timeTableGridView.RowTemplate.Height = 24;
            this.timeTableGridView.Size = new System.Drawing.Size(1170, 660);
            this.timeTableGridView.TabIndex = 4;
            this.timeTableGridView.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 773);
            this.Controls.Add(this.timeTableGridView);
            this.Controls.Add(this.OrderButton);
            this.Controls.Add(this.SpiderButton);
            this.Controls.Add(this.webBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TicketRobot";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeTableGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button SpiderButton;
        private System.Windows.Forms.Button OrderButton;
        private System.Windows.Forms.DataGridView timeTableGridView;
    }
}

