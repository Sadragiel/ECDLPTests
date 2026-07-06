using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using WeierstrassCurveTest.DLP;
using WeierstrassCurveTest.Performance;
using WeierstrassCurveTest.Performance.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WeierstrassCurveTest.UI
{
    public partial class UserInterface : Form
    {
        private PerformanceEvaluator evaluator;

        private Chart cpuChart;
        private PerformanceCounter cpuCounter;
        private System.Windows.Forms.Timer timer;

        private string datasetPath = "";
        public int heatingItemsToal = 0;
        public int heatingItemsCurrent = 0;
        public int proccessingItemsTotal = 0;
        public int proccessingItemsCurrent = 0;


        public UserInterface()
        {
            InitializeComponent();
        }

        public void SetHeatingTotalItems(int total)
        {
            heatingItemsToal = total;

            if (heatingProgressBar.InvokeRequired)
            {
                heatingProgressBar.Invoke(() => heatingProgressBar.Maximum = total);
            }
            else
            {
                heatingProgressBar.Maximum = total;
            }

            SetHeatingProgress(0);
            UpdateTotalItemsLabel();
        }

        public void IncrementHeatingProgress()
        {
            SetHeatingProgress(heatingItemsCurrent + 1);
        }

        public void SetHeatingProgress(int current)
        {
            heatingItemsCurrent = current;

            if (heatingProgressBar.InvokeRequired)
            {
                heatingProgressBar.Invoke(() => heatingProgressBar.Value = Math.Min(current, heatingProgressBar.Maximum));
            }
            else
            {
                heatingProgressBar.Value = Math.Min(current, heatingProgressBar.Maximum);
            }

            if (heatingProgressLabel.InvokeRequired)
            {
                heatingProgressLabel.Invoke(() => heatingProgressLabel.Text = $"{heatingItemsCurrent} / {heatingItemsToal}");
            }
            else
            {
                heatingProgressLabel.Text = $"{heatingItemsCurrent} / {heatingItemsToal}";
            }
        }

        public void SetProccessingTotalItems(int total)
        {
            proccessingItemsTotal = total;

            if (proccessingProgressBar.InvokeRequired)
            {
                proccessingProgressBar.Invoke(() => proccessingProgressBar.Maximum = total);
            }
            else
            {
                proccessingProgressBar.Maximum = total;
            }

            SetProccessingProgress(0);
            UpdateTotalItemsLabel();
        }

        public void IncrementProccessingProgress()
        {
            SetProccessingProgress(proccessingItemsCurrent + 1);
        }

        public void SetProccessingProgress(int current)
        {
            proccessingItemsCurrent = current;

            if (proccessingProgressBar.InvokeRequired)
            {
                proccessingProgressBar.Invoke(() => proccessingProgressBar.Value = Math.Min(current, proccessingProgressBar.Maximum));
            }
            else
            {
                proccessingProgressBar.Value = Math.Min(current, proccessingProgressBar.Maximum);
            }

            if (proccessingProgressLabel.InvokeRequired)
            {
                proccessingProgressLabel.Invoke(() => proccessingProgressLabel.Text = $"{proccessingItemsCurrent} / {proccessingItemsTotal}");
            }
            else
            {
                proccessingProgressLabel.Text = $"{proccessingItemsCurrent} / {proccessingItemsTotal}";
            }
        }

        private void UpdateTotalItemsLabel()
        {
            if (TotalItemsLabel.InvokeRequired)
            {
                TotalItemsLabel.Invoke(() => TotalItemsLabel.Text = $"{proccessingItemsTotal + heatingItemsToal}");
            }
            else
            {
                TotalItemsLabel.Text = $"{proccessingItemsTotal + heatingItemsToal}";
            }
        }

        private void UserInterface_Load(object sender, EventArgs e)
        {
            evaluator = new PerformanceEvaluator(this);
        }

        private void InitializeCpuChart()
        {
            // Create the chart
            cpuChart = new Chart
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(cpuChart);

            // Add a chart area
            ChartArea area = new ChartArea("MainArea");
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 100;
            area.AxisY.Title = "CPU Usage (%)";
            cpuChart.ChartAreas.Add(area);

            // Add a data series
            Series series = new Series("CPU")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2
            };
            cpuChart.Series.Add(series);

            panelChart.Controls.Add(cpuChart);

            // Setup performance counter
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            // Timer for live updates
            timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (s, e) =>
            {
                float value = cpuCounter.NextValue();
                series.Points.AddY(value);
                if (series.Points.Count > 60)
                    series.Points.RemoveAt(0);
            };
            timer.Start();
        }

        private void CleanupCpuChart()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            if (cpuChart != null)
            {
                panelChart.Controls.Remove(cpuChart);
                cpuChart.Dispose();
                cpuChart = null;
            }

            if (cpuCounter != null)
            {
                cpuCounter.Dispose();
                cpuCounter = null;
            }
        }

        private void autofillButton_Click(object sender, EventArgs e)
        {
            ParseCSVData();
        }

        private void ParseCSVData()
        {
            try
            {
                ClearResults();

                // Get input string
                string input = CSVParserData.Text.Trim();
                string[] parts = input.Split(',');

                if (parts.Length != 14)
                    throw new FormatException("Expected 14 comma-separated values.");

                // Trim and parse all values as BigIntegers
                var values = parts.Select(p => BigInteger.Parse(p.Trim())).ToArray();

                // Assign values to corresponding fields
                pValue.Text = values[0].ToString();
                aValue.Text = values[1].ToString();
                bValue.Text = values[2].ToString();
                // curveOrderValue.Text = values[3].ToString();
                curveOrderValue.Text = values[6].ToString();

                PxValue.Text = values[4].ToString();
                PyValue.Text = values[5].ToString();
                // POrderValue.Text = values[6].ToString();

                QxValue.Text = values[7].ToString();
                QyValue.Text = values[8].ToString();
                // QOrderValue.Text = values[9].ToString();

                expectedKValue.Text = values[10].ToString();

                Y1xValue.Text = values[11].ToString();
                Y2xValue.Text = values[12].ToString();
                Y3xValue.Text = values[13].ToString();
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
        }

        public void RunSingleTest()
        {
            try
            {
                ClearResults();

                DatastItem data = new DatastItem();

                data.modulo = BigInteger.Parse(pValue.Text);
                data.curveParam1 = BigInteger.Parse(aValue.Text);
                data.curveParam2 = BigInteger.Parse(bValue.Text);
                data.order = BigInteger.Parse(curveOrderValue.Text);

                data.point1_x = BigInteger.Parse(PxValue.Text);
                data.point1_y = BigInteger.Parse(PyValue.Text);
                data.point1_order = BigInteger.Parse(curveOrderValue.Text);
                data.point2_x = BigInteger.Parse(QxValue.Text);
                data.point2_y = BigInteger.Parse(QyValue.Text);

                data.dlpSolution = BigInteger.Parse(expectedKValue.Text);

                if (UseExtendedNegationMapsCheckbox.Checked)
                {
                    data.x0 = BigInteger.Parse(Y1xValue.Text);
                    data.x1 = BigInteger.Parse(Y2xValue.Text);
                    data.x2 = BigInteger.Parse(Y3xValue.Text);
                }

                Console.WriteLine($"Selected method: {GetMethodName()}");

                string result = evaluator.Evaluate(
                    data,
                    GetCurveName(),
                    GetMethodName(),
                    UsePohligHellmanCheckbox.Checked,
                    UseNegationMapsCheckbox.Checked,
                    UseExtendedNegationMapsCheckbox.Checked
                );

                Console.WriteLine(result);
                SetResult(result);
            }
            catch (Exception ex)
            {
                SetError(ex);
            } 
        }

        public void RunBulkTest()
        {
            try
            {
                Invoke(() => ClearResults(true));
                if (datasetPath == "" || datasetPath == null)
                {
                    throw new Exception("Path to the dataset is not specified");
                }

                Invoke(() => ShowProgressBlock());

                // Bulk testng is performed into two stpes:
                // first is to run test on the dataset and store results
                Console.WriteLine($"UsePohligHellmanCheckbox.Checked = {UsePohligHellmanCheckbox.Checked}");
                Console.WriteLine($"UseNegationMapsCheckbox.Checked = {UseNegationMapsCheckbox.Checked}");
                Console.WriteLine($"UseExtendedNegationMapsCheckbox.Checked = {UseExtendedNegationMapsCheckbox.Checked}");
                var resultsFilePath = evaluator.Evaluate(
                    datasetPath,
                    GetCurveName(),
                    GetMethodName(),
                    UsePohligHellmanCheckbox.Checked,
                    UseNegationMapsCheckbox.Checked,
                    UseExtendedNegationMapsCheckbox.Checked
                );

                // second is to read stored results and prepare statistics
                var result = evaluator.CalculateAndLogStatistics(resultsFilePath);

                
                Invoke(() => SetResult(result, true));
            }
            catch (Exception ex)
            {
                Invoke(() => SetError(ex, true));
            }
            finally
            {
                Invoke(() => HideProgressBlock());
            }
        }

        private MethodName GetMethodName()
        {
            foreach (Control control in MethodNameGroup.Controls)
            {
                if (control is RadioButton rb && rb.Checked)
                {
                    if (Enum.TryParse(typeof(MethodName), rb.Tag?.ToString(), out var result))
                    {
                        return (MethodName)result;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid method name selected.");
                    }
                }
            }
            throw new InvalidOperationException("No method radio button selected.");
        }

        private CurveName GetCurveName()
        {
            foreach (Control control in CurveNameGroup.Controls)
            {
                if (control is RadioButton rb && rb.Checked)
                {
                    if (Enum.TryParse(typeof(CurveName), rb.Tag?.ToString(), out var result))
                    {
                        return (CurveName)result;
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid curve name selected.");
                    }
                }
            }
            throw new InvalidOperationException("No curve radio button selected.");
        }

        private void ShowProgressBlock()
        {
            Console.WriteLine("Showing progress block");
            InitializeCpuChart();
            ProccessingGroup.Visible = true;
        }

        private void HideProgressBlock()
        {
            CleanupCpuChart();
            ProccessingGroup.Visible = false;
        }

        private void ClearResults(bool isBulk = false)
        {
            if (!isBulk)
            {
                resultsGroupBlock.Visible = false;
                ResultTextError.Visible = false;
                ResultText.Visible = false;
                ResultTextError.Text = "";
                ResultText.Text = "";
            } else
            {
                BulkResultsBlock.Visible = false;
                BulkResultTextError.Visible = false;
                BulkResultText.Visible = false;
                BulkResultTextError.Text = "";
                BulkResultText.Text = "";
            }
        }

        private void SetError(Exception ex, bool isBulk = false)
        {
            SetResult($"Error: {ex.Message}", isBulk, true);
        }

        private void SetResult(string message, bool isBulk = false, bool isError = false)
        {
            if (!isBulk)
            {
                resultsGroupBlock.Visible = true;
                var textField = isError ? ResultTextError : ResultText;
                textField.Visible = true;
                textField.Text = message;
            } else
            {
                if (BulkResultsBlock.InvokeRequired)
                {
                    BulkResultsBlock.Invoke(() => BulkResultsBlock.Visible = true);
                }
                else
                {
                    BulkResultsBlock.Visible = true;
                }

                var textField = isError ? BulkResultTextError : BulkResultText;
                if (textField.InvokeRequired)
                {
                    textField.Invoke(() => textField.Visible = true);
                    textField.Invoke(() => textField.Text = message);
                }
                else
                {
                    textField.Visible = true;
                    textField.Text = message;
                }
            }
        }

        private void PollardRho_CheckedChanged(object sender, EventArgs e)
        {
            UseNegationMapsCheckbox.Visible = PollardRho.Checked;
            UseExtendedNegationMapsCheckbox.Visible = PollardRho.Checked;
            YPointsGroup.Visible = PollardRho.Checked && UseExtendedNegationMapsCheckbox.Checked;
        }

        private void UseExtendedNegationMapsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            YPointsGroup.Visible = UseExtendedNegationMapsCheckbox.Checked;
        }

        private async void RunTest_Click(object sender, EventArgs e)
        {
            RunTest.Enabled = false;

            if (tabControl.SelectedTab == SingleCurveTest)
            {
                RunSingleTest();
            } 
            else
            {
                await Task.Run(() => RunBulkTest());
            }

            RunTest.Enabled = true;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;
                string initialFolder = Path.Combine(currentDir, "Performance", "Datasets");

                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.Title = "Select CSV File";
                openFileDialog.InitialDirectory = initialFolder;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    datasetPath = openFileDialog.FileName;
                    filenameLabel.Text = Path.GetFileName(datasetPath);
                }
            }
        }


    }
}
