namespace RegistHelper
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.registerList = new System.Windows.Forms.ListBox();
            this.notifier = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // registerList
            // 
            this.registerList.BackColor = System.Drawing.SystemColors.Window;
            this.registerList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.registerList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.registerList.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.registerList.FormattingEnabled = true;
            this.registerList.HorizontalScrollbar = true;
            this.registerList.ItemHeight = 25;
            this.registerList.Location = new System.Drawing.Point(12, 12);
            this.registerList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.registerList.MinimumSize = new System.Drawing.Size(284, 402);
            this.registerList.Name = "registerList";
            this.registerList.Size = new System.Drawing.Size(284, 402);
            this.registerList.TabIndex = 1;
            this.registerList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.registerList_DrawItem);
            // 
            // notifier
            // 
            this.notifier.Icon = ((System.Drawing.Icon)(resources.GetObject("notifier.Icon")));
            this.notifier.Text = "double click to show";
            this.notifier.Visible = true;
            this.notifier.DoubleClick += new System.EventHandler(this.notifier_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 439);
            this.Controls.Add(this.registerList);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(324, 477);
            this.MinimumSize = new System.Drawing.Size(324, 477);
            this.Name = "MainForm";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistHelper";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox registerList;
        private System.Windows.Forms.NotifyIcon notifier;
    }
}

