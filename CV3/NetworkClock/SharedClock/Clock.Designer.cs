namespace SharedClock
{
  partial class Clock
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.commitChangesButton = new System.Windows.Forms.Button();
      this.datePicker = new System.Windows.Forms.DateTimePicker();
      this.timePicker = new System.Windows.Forms.DateTimePicker();
      this.SuspendLayout();
      // 
      // commitChangesButton
      // 
      this.commitChangesButton.Location = new System.Drawing.Point(325, 12);
      this.commitChangesButton.Name = "commitChangesButton";
      this.commitChangesButton.Size = new System.Drawing.Size(114, 23);
      this.commitChangesButton.TabIndex = 0;
      this.commitChangesButton.Text = "Commit changes";
      this.commitChangesButton.UseVisualStyleBackColor = true;
      this.commitChangesButton.Click += new System.EventHandler(this.commitChangesButton_Click);
      // 
      // datePicker
      // 
      this.datePicker.Location = new System.Drawing.Point(13, 13);
      this.datePicker.Name = "datePicker";
      this.datePicker.Size = new System.Drawing.Size(200, 20);
      this.datePicker.TabIndex = 1;
      // 
      // timePicker
      // 
      this.timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
      this.timePicker.Location = new System.Drawing.Point(219, 13);
      this.timePicker.Name = "timePicker";
      this.timePicker.Size = new System.Drawing.Size(100, 20);
      this.timePicker.TabIndex = 2;
      // 
      // Clock
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(449, 48);
      this.Controls.Add(this.timePicker);
      this.Controls.Add(this.datePicker);
      this.Controls.Add(this.commitChangesButton);
      this.Name = "Clock";
      this.Text = "Clock";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button commitChangesButton;
    private System.Windows.Forms.DateTimePicker datePicker;
    private System.Windows.Forms.DateTimePicker timePicker;
  }
}

