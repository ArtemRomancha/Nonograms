namespace Nonograms
{
    partial class VisualBoard
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Nonogram_Board = new System.Windows.Forms.PictureBox();
            this.Control = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Nonogram_Board)).BeginInit();
            this.SuspendLayout();
            // 
            // Nonogram_Board
            // 
            this.Nonogram_Board.Location = new System.Drawing.Point(56, 50);
            this.Nonogram_Board.Margin = new System.Windows.Forms.Padding(3, 3, 50, 50);
            this.Nonogram_Board.Name = "Nonogram_Board";
            this.Nonogram_Board.Size = new System.Drawing.Size(147, 38);
            this.Nonogram_Board.TabIndex = 0;
            this.Nonogram_Board.TabStop = false;
            this.Nonogram_Board.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // Control
            // 
            this.Control.Location = new System.Drawing.Point(56, 12);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(75, 23);
            this.Control.TabIndex = 4;
            this.Control.Text = "Проверить";
            this.Control.UseVisualStyleBackColor = true;
            this.Control.Click += new System.EventHandler(this.Control_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(140, 12);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 5;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // VisualBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(284, 124);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Control);
            this.Controls.Add(this.Nonogram_Board);
            this.Name = "VisualBoard";
            this.Text = "VisualBoard";
            ((System.ComponentModel.ISupportInitialize)(this.Nonogram_Board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Nonogram_Board;
        private System.Windows.Forms.Button Control;
        private System.Windows.Forms.Button Clear;
    }
}

