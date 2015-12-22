using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SharedClock
{
  public partial class Clock : Form
  {
    public Clock()
    {
      InitializeComponent();
    }

    private void commitChangesButton_Click(object sender, EventArgs e)
    {
      DateTime dateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
      DateTimeSetter.SetDateTime(TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc));
    }
  }
}
