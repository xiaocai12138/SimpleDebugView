namespace SimpleDebugView
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 —— 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblPid = new System.Windows.Forms.Label();
            this.txtPidFilter = new System.Windows.Forms.TextBox();
            this.lblKeyword = new System.Windows.Forms.Label();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(9, 10);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(68, 24);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(81, 10);
            this.btnStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(68, 24);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(153, 10);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(68, 24);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(225, 10);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(68, 24);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblPid
            // 
            this.lblPid.AutoSize = true;
            this.lblPid.Location = new System.Drawing.Point(308, 16);
            this.lblPid.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPid.Name = "lblPid";
            this.lblPid.Size = new System.Drawing.Size(35, 12);
            this.lblPid.TabIndex = 4;
            this.lblPid.Text = "PID：";
            // 
            // txtPidFilter
            // 
            this.txtPidFilter.Location = new System.Drawing.Point(341, 13);
            this.txtPidFilter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPidFilter.Name = "txtPidFilter";
            this.txtPidFilter.Size = new System.Drawing.Size(61, 21);
            this.txtPidFilter.TabIndex = 5;
            // 
            // lblKeyword
            // 
            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new System.Drawing.Point(412, 16);
            this.lblKeyword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new System.Drawing.Size(53, 12);
            this.lblKeyword.TabIndex = 6;
            this.lblKeyword.Text = "关键字：";
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(465, 13);
            this.txtKeyword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(136, 21);
            this.txtKeyword.TabIndex = 7;
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLogs.Font = new System.Drawing.Font("Consolas", 9F);
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.HorizontalScrollbar = true;
            this.listBoxLogs.ItemHeight = 14;
            this.listBoxLogs.Location = new System.Drawing.Point(9, 45);
            this.listBoxLogs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(666, 410);
            this.listBoxLogs.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.txtKeyword);
            this.Controls.Add(this.lblKeyword);
            this.Controls.Add(this.txtPidFilter);
            this.Controls.Add(this.lblPid);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(626, 408);
            this.Name = "MainForm";
            this.Text = "Simple DebugView（DBWIN Monitor）";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblPid;
        private System.Windows.Forms.TextBox txtPidFilter;
        private System.Windows.Forms.Label lblKeyword;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.ListBox listBoxLogs;
    }
}
