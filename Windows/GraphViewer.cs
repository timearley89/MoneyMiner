using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MoneyMiner.Windows
{
    public partial class GraphViewer : Form
    {
        public Chart myChart;
        public GraphViewer()
        {
            InitializeComponent();
            myChart = chart1;

            myChart.Series.Add("Wood Miner");
            myChart.Series.Add("Stone Miner");
            myChart.Series.Add("Iron Miner");
            myChart.Series.Add("Steel Miner");
            myChart.Series.Add("Diamond Miner");
            myChart.Series.Add("Uranium Miner");
            myChart.Series.Add("Antimatter Miner");
            myChart.Series.Add("Black Hole Miner");
            myChart.Series[0].Color = Color.Red;
            myChart.Series[1].Color = Color.Orange;
            myChart.Series[2].Color = Color.Yellow;
            myChart.Series[3].Color = Color.Green;
            myChart.Series[4].Color = Color.Blue;
            myChart.Series[5].Color = Color.Purple;
            myChart.Series[6].Color = Color.Brown;
            myChart.Series[7].Color = Color.Black;
            foreach (Series srs in myChart.Series)
            {
                srs.ChartType = SeriesChartType.Line;
                srs.ChartArea = "Miner $/Sec";
            }
            chart1 = myChart;
            chart1.MouseWheel += Chart1_MouseWheel;
        }

        private void Chart1_MouseWheel(object? sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    chart1.ChartAreas[0].Axes[0].ScaleView.ZoomReset();
                    chart1.ChartAreas[0].Axes[1].ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = chart1.ChartAreas[0].Axes[0].ScaleView.ViewMinimum;
                    var xMax = chart1.ChartAreas[0].Axes[0].ScaleView.ViewMaximum;
                    var yMin = chart1.ChartAreas[0].Axes[1].ScaleView.ViewMinimum;
                    var yMax = chart1.ChartAreas[0].Axes[1].ScaleView.ViewMaximum;

                    var posXStart = chart1.ChartAreas[0].Axes[0].PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = chart1.ChartAreas[0].Axes[0].PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = chart1.ChartAreas[0].Axes[1].PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = chart1.ChartAreas[0].Axes[1].PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    chart1.ChartAreas[0].Axes[0].ScaleView.Zoom(posXStart, posXFinish);
                    chart1.ChartAreas[0].Axes[1].ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void GraphViewer_Load(object sender, EventArgs e)
        {
            
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
