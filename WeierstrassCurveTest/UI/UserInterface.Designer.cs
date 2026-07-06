namespace WeierstrassCurveTest.UI
{
    partial class UserInterface
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
            label1 = new Label();
            tabControl = new TabControl();
            SingleCurveTest = new TabPage();
            groupBox5 = new GroupBox();
            autofillButton = new Button();
            CSVParserData = new TextBox();
            label15 = new Label();
            label14 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            YPointsGroup = new GroupBox();
            Y3xValue = new TextBox();
            label19 = new Label();
            label17 = new Label();
            label18 = new Label();
            Y2xValue = new TextBox();
            Y1xValue = new TextBox();
            label16 = new Label();
            groupBox4 = new GroupBox();
            label20 = new Label();
            expectedKValue = new TextBox();
            PyValue = new TextBox();
            PxValue = new TextBox();
            QyValue = new TextBox();
            QxValue = new TextBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            groupBox3 = new GroupBox();
            curveOrderValue = new TextBox();
            label3 = new Label();
            bValue = new TextBox();
            label2 = new Label();
            aValue = new TextBox();
            pValue = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            resultsGroupBlock = new GroupBox();
            ResultText = new Label();
            ResultTextError = new Label();
            BulkTest = new TabPage();
            BulkResultsBlock = new GroupBox();
            BulkResultText = new Label();
            BulkResultTextError = new Label();
            ProccessingGroup = new GroupBox();
            TotalItemsLabel = new Label();
            label30 = new Label();
            panelChart = new Panel();
            proccessingProgressBar = new ProgressBar();
            proccessingProgressLabel = new Label();
            label28 = new Label();
            heatingProgressLabel = new Label();
            label26 = new Label();
            heatingProgressBar = new ProgressBar();
            groupBox8 = new GroupBox();
            filenameLabel = new Label();
            btnSelectFile = new Button();
            label24 = new Label();
            label23 = new Label();
            MethodNameGroup = new GroupBox();
            LasVegas = new RadioButton();
            GrympyGiants = new RadioButton();
            Kangaroo = new RadioButton();
            BSGS = new RadioButton();
            PollardRho = new RadioButton();
            CurveNameGroup = new GroupBox();
            Edwards = new RadioButton();
            Weierstrass = new RadioButton();
            UsePohligHellmanCheckbox = new CheckBox();
            UseNegationMapsCheckbox = new CheckBox();
            UseExtendedNegationMapsCheckbox = new CheckBox();
            RunTest = new Button();
            tabControl.SuspendLayout();
            SingleCurveTest.SuspendLayout();
            groupBox5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            YPointsGroup.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            resultsGroupBlock.SuspendLayout();
            BulkTest.SuspendLayout();
            BulkResultsBlock.SuspendLayout();
            ProccessingGroup.SuspendLayout();
            groupBox8.SuspendLayout();
            MethodNameGroup.SuspendLayout();
            CurveNameGroup.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(116, 59);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl.Controls.Add(SingleCurveTest);
            tabControl.Controls.Add(BulkTest);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(644, 581);
            tabControl.TabIndex = 1;
            // 
            // SingleCurveTest
            // 
            SingleCurveTest.Controls.Add(groupBox5);
            SingleCurveTest.Controls.Add(tableLayoutPanel1);
            SingleCurveTest.Controls.Add(resultsGroupBlock);
            SingleCurveTest.Location = new Point(4, 24);
            SingleCurveTest.Name = "SingleCurveTest";
            SingleCurveTest.Padding = new Padding(3);
            SingleCurveTest.Size = new Size(636, 553);
            SingleCurveTest.TabIndex = 0;
            SingleCurveTest.Text = "Single Curve Test";
            SingleCurveTest.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(autofillButton);
            groupBox5.Controls.Add(CSVParserData);
            groupBox5.Controls.Add(label15);
            groupBox5.Controls.Add(label14);
            groupBox5.Location = new Point(6, 222);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(624, 100);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Autofill from CSV parser";
            // 
            // autofillButton
            // 
            autofillButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            autofillButton.Location = new Point(542, 69);
            autofillButton.Name = "autofillButton";
            autofillButton.Size = new Size(75, 23);
            autofillButton.TabIndex = 3;
            autofillButton.Text = "Autofill";
            autofillButton.UseVisualStyleBackColor = true;
            autofillButton.Click += autofillButton_Click;
            // 
            // CSVParserData
            // 
            CSVParserData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            CSVParserData.Location = new Point(7, 69);
            CSVParserData.Name = "CSVParserData";
            CSVParserData.Size = new Size(530, 23);
            CSVParserData.TabIndex = 2;
            CSVParserData.Text = "33554393,11305073,22334309,33557180,2837608,21112599,16778590,22158128,12605812,8389295,4733824,27738392,25992085,13378309\r\n";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label15.Location = new Point(7, 49);
            label15.Name = "label15";
            label15.Size = new Size(376, 17);
            label15.TabIndex = 1;
            label15.Text = "p, a, b, curve order, P.x, P.y, P order, Q.x, Q.y, Q order, expected k";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(5, 19);
            label14.Name = "label14";
            label14.Size = new Size(348, 30);
            label14.TabIndex = 0;
            label14.Text = "This feature allows you to autofill other fields from one CSV line. \r\nPlease, provide the CSV data in this format: \r\n";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.58232F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.41768F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(groupBox3, 0, 0);
            tableLayoutPanel1.Location = new Point(6, 6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 95.12195F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4.878049F));
            tableLayoutPanel1.Size = new Size(624, 235);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(YPointsGroup, 1, 0);
            tableLayoutPanel2.Controls.Add(groupBox4, 0, 0);
            tableLayoutPanel2.Location = new Point(206, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 94.6666641F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5.33333349F));
            tableLayoutPanel2.Size = new Size(415, 217);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // YPointsGroup
            // 
            YPointsGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            YPointsGroup.Controls.Add(Y3xValue);
            YPointsGroup.Controls.Add(label19);
            YPointsGroup.Controls.Add(label17);
            YPointsGroup.Controls.Add(label18);
            YPointsGroup.Controls.Add(Y2xValue);
            YPointsGroup.Controls.Add(Y1xValue);
            YPointsGroup.Controls.Add(label16);
            YPointsGroup.Location = new Point(210, 3);
            YPointsGroup.Name = "YPointsGroup";
            YPointsGroup.Size = new Size(202, 196);
            YPointsGroup.TabIndex = 5;
            YPointsGroup.TabStop = false;
            YPointsGroup.Text = "Points on abscissa axis";
            YPointsGroup.Visible = false;
            // 
            // Y3xValue
            // 
            Y3xValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Y3xValue.Location = new Point(58, 128);
            Y3xValue.Name = "Y3xValue";
            Y3xValue.Size = new Size(138, 23);
            Y3xValue.TabIndex = 7;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(10, 131);
            label19.Name = "label19";
            label19.Size = new Size(42, 15);
            label19.TabIndex = 9;
            label19.Text = "Y3.x = ";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(11, 73);
            label17.Name = "label17";
            label17.Size = new Size(42, 15);
            label17.TabIndex = 1;
            label17.Text = "Y1.x = ";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(10, 102);
            label18.Name = "label18";
            label18.Size = new Size(42, 15);
            label18.TabIndex = 8;
            label18.Text = "Y2.x = ";
            // 
            // Y2xValue
            // 
            Y2xValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Y2xValue.Location = new Point(58, 99);
            Y2xValue.Name = "Y2xValue";
            Y2xValue.Size = new Size(138, 23);
            Y2xValue.TabIndex = 6;
            // 
            // Y1xValue
            // 
            Y1xValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Y1xValue.Location = new Point(59, 70);
            Y1xValue.Name = "Y1xValue";
            Y1xValue.Size = new Size(137, 23);
            Y1xValue.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(6, 19);
            label16.Name = "label16";
            label16.Size = new Size(194, 45);
            label16.TabIndex = 0;
            label16.Text = "Points Y lie on the x-axis (abscissa). \r\nThey are used in the extended \r\nnegation map technique.";
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(label20);
            groupBox4.Controls.Add(expectedKValue);
            groupBox4.Controls.Add(PyValue);
            groupBox4.Controls.Add(PxValue);
            groupBox4.Controls.Add(QyValue);
            groupBox4.Controls.Add(QxValue);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Location = new Point(3, 3);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(201, 196);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Enter points coordinates";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(6, 157);
            label20.Name = "label20";
            label20.Size = new Size(77, 15);
            label20.TabIndex = 11;
            label20.Text = "Expected k = ";
            // 
            // expectedKValue
            // 
            expectedKValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            expectedKValue.Location = new Point(83, 154);
            expectedKValue.Name = "expectedKValue";
            expectedKValue.Size = new Size(111, 23);
            expectedKValue.TabIndex = 10;
            // 
            // PyValue
            // 
            PyValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PyValue.Location = new Point(44, 125);
            PyValue.Name = "PyValue";
            PyValue.Size = new Size(150, 23);
            PyValue.TabIndex = 9;
            // 
            // PxValue
            // 
            PxValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            PxValue.Location = new Point(44, 96);
            PxValue.Name = "PxValue";
            PxValue.Size = new Size(150, 23);
            PxValue.TabIndex = 8;
            // 
            // QyValue
            // 
            QyValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            QyValue.Location = new Point(44, 67);
            QyValue.Name = "QyValue";
            QyValue.Size = new Size(150, 23);
            QyValue.TabIndex = 7;
            // 
            // QxValue
            // 
            QxValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            QxValue.Location = new Point(44, 38);
            QxValue.Name = "QxValue";
            QxValue.Size = new Size(150, 23);
            QxValue.TabIndex = 6;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 129);
            label13.Name = "label13";
            label13.Size = new Size(37, 15);
            label13.TabIndex = 5;
            label13.Text = "P.y = ";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 99);
            label12.Name = "label12";
            label12.Size = new Size(36, 15);
            label12.TabIndex = 4;
            label12.Text = "P.x = ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 70);
            label11.Name = "label11";
            label11.Size = new Size(39, 15);
            label11.TabIndex = 3;
            label11.Text = "Q.y = ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 41);
            label10.Name = "label10";
            label10.Size = new Size(38, 15);
            label10.TabIndex = 2;
            label10.Text = "Q.x = ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(44, 17);
            label9.Name = "label9";
            label9.Size = new Size(43, 15);
            label9.TabIndex = 1;
            label9.Text = "Q = kP";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(6, 17);
            label8.Name = "label8";
            label8.Size = new Size(32, 15);
            label8.TabIndex = 0;
            label8.Text = "DLP:";
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(curveOrderValue);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(bValue);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(aValue);
            groupBox3.Controls.Add(pValue);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(3, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(197, 199);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Enter curve params";
            // 
            // curveOrderValue
            // 
            curveOrderValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            curveOrderValue.Location = new Point(94, 140);
            curveOrderValue.Name = "curveOrderValue";
            curveOrderValue.Size = new Size(98, 23);
            curveOrderValue.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(7, 19);
            label3.Name = "label3";
            label3.Size = new Size(146, 15);
            label3.TabIndex = 1;
            label3.Text = "Selected curve equation:";
            // 
            // bValue
            // 
            bValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            bValue.Location = new Point(39, 110);
            bValue.Name = "bValue";
            bValue.Size = new Size(153, 23);
            bValue.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(8, 34);
            label2.Name = "label2";
            label2.Size = new Size(130, 15);
            label2.TabIndex = 0;
            label2.Text = "y² = x³ + ax + b   mod p";
            // 
            // aValue
            // 
            aValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            aValue.Location = new Point(39, 81);
            aValue.Name = "aValue";
            aValue.Size = new Size(153, 23);
            aValue.TabIndex = 5;
            // 
            // pValue
            // 
            pValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pValue.Location = new Point(39, 52);
            pValue.Name = "pValue";
            pValue.Size = new Size(154, 23);
            pValue.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 142);
            label7.Name = "label7";
            label7.Size = new Size(83, 15);
            label7.TabIndex = 3;
            label7.Text = "Curve order = ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 113);
            label6.Name = "label6";
            label6.Size = new Size(28, 15);
            label6.TabIndex = 2;
            label6.Text = "b = ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(5, 84);
            label5.Name = "label5";
            label5.Size = new Size(27, 15);
            label5.TabIndex = 1;
            label5.Text = "a = ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(5, 55);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 0;
            label4.Text = "p = ";
            // 
            // resultsGroupBlock
            // 
            resultsGroupBlock.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            resultsGroupBlock.Controls.Add(ResultText);
            resultsGroupBlock.Controls.Add(ResultTextError);
            resultsGroupBlock.Location = new Point(6, 328);
            resultsGroupBlock.Name = "resultsGroupBlock";
            resultsGroupBlock.Size = new Size(618, 219);
            resultsGroupBlock.TabIndex = 6;
            resultsGroupBlock.TabStop = false;
            resultsGroupBlock.Text = "Results";
            resultsGroupBlock.Visible = false;
            // 
            // ResultText
            // 
            ResultText.AutoSize = true;
            ResultText.Location = new Point(11, 19);
            ResultText.Name = "ResultText";
            ResultText.Size = new Size(66, 15);
            ResultText.TabIndex = 1;
            ResultText.Text = "Results text";
            ResultText.Visible = false;
            // 
            // ResultTextError
            // 
            ResultTextError.AutoSize = true;
            ResultTextError.ForeColor = Color.Red;
            ResultTextError.Location = new Point(11, 19);
            ResultTextError.Name = "ResultTextError";
            ResultTextError.Size = new Size(56, 15);
            ResultTextError.TabIndex = 0;
            ResultTextError.Text = "Error Text";
            ResultTextError.Visible = false;
            // 
            // BulkTest
            // 
            BulkTest.Controls.Add(BulkResultsBlock);
            BulkTest.Controls.Add(ProccessingGroup);
            BulkTest.Controls.Add(groupBox8);
            BulkTest.Location = new Point(4, 24);
            BulkTest.Name = "BulkTest";
            BulkTest.Padding = new Padding(3);
            BulkTest.Size = new Size(636, 553);
            BulkTest.TabIndex = 1;
            BulkTest.Text = "Bulk Test";
            BulkTest.UseVisualStyleBackColor = true;
            // 
            // BulkResultsBlock
            // 
            BulkResultsBlock.Controls.Add(BulkResultText);
            BulkResultsBlock.Controls.Add(BulkResultTextError);
            BulkResultsBlock.Location = new Point(6, 122);
            BulkResultsBlock.Name = "BulkResultsBlock";
            BulkResultsBlock.Size = new Size(619, 419);
            BulkResultsBlock.TabIndex = 0;
            BulkResultsBlock.TabStop = false;
            BulkResultsBlock.Text = "Results";
            BulkResultsBlock.Visible = false;
            // 
            // BulkResultText
            // 
            BulkResultText.AutoSize = true;
            BulkResultText.Location = new Point(6, 19);
            BulkResultText.Name = "BulkResultText";
            BulkResultText.Size = new Size(60, 15);
            BulkResultText.TabIndex = 1;
            BulkResultText.Text = "ResultText";
            // 
            // BulkResultTextError
            // 
            BulkResultTextError.AutoSize = true;
            BulkResultTextError.ForeColor = Color.Red;
            BulkResultTextError.Location = new Point(6, 19);
            BulkResultTextError.Name = "BulkResultTextError";
            BulkResultTextError.Size = new Size(85, 15);
            BulkResultTextError.TabIndex = 0;
            BulkResultTextError.Text = "ResultErrorText";
            BulkResultTextError.Visible = false;
            // 
            // ProccessingGroup
            // 
            ProccessingGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProccessingGroup.Controls.Add(TotalItemsLabel);
            ProccessingGroup.Controls.Add(label30);
            ProccessingGroup.Controls.Add(panelChart);
            ProccessingGroup.Controls.Add(proccessingProgressBar);
            ProccessingGroup.Controls.Add(proccessingProgressLabel);
            ProccessingGroup.Controls.Add(label28);
            ProccessingGroup.Controls.Add(heatingProgressLabel);
            ProccessingGroup.Controls.Add(label26);
            ProccessingGroup.Controls.Add(heatingProgressBar);
            ProccessingGroup.Location = new Point(6, 122);
            ProccessingGroup.Name = "ProccessingGroup";
            ProccessingGroup.Size = new Size(625, 425);
            ProccessingGroup.TabIndex = 1;
            ProccessingGroup.TabStop = false;
            ProccessingGroup.Text = "Process";
            ProccessingGroup.Visible = false;
            // 
            // TotalItemsLabel
            // 
            TotalItemsLabel.AutoSize = true;
            TotalItemsLabel.Location = new Point(48, 19);
            TotalItemsLabel.Name = "TotalItemsLabel";
            TotalItemsLabel.Size = new Size(17, 15);
            TotalItemsLabel.TabIndex = 8;
            TotalItemsLabel.Text = "??";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(6, 19);
            label30.Name = "label30";
            label30.Size = new Size(36, 15);
            label30.TabIndex = 7;
            label30.Text = "Total:";
            // 
            // panelChart
            // 
            panelChart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChart.Location = new Point(6, 140);
            panelChart.Name = "panelChart";
            panelChart.Size = new Size(613, 279);
            panelChart.TabIndex = 6;
            // 
            // proccessingProgressBar
            // 
            proccessingProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            proccessingProgressBar.Location = new Point(6, 111);
            proccessingProgressBar.Name = "proccessingProgressBar";
            proccessingProgressBar.Size = new Size(613, 23);
            proccessingProgressBar.TabIndex = 5;
            // 
            // proccessingProgressLabel
            // 
            proccessingProgressLabel.AutoSize = true;
            proccessingProgressLabel.Location = new Point(58, 93);
            proccessingProgressLabel.Name = "proccessingProgressLabel";
            proccessingProgressLabel.Size = new Size(38, 15);
            proccessingProgressLabel.TabIndex = 4;
            proccessingProgressLabel.Text = "?? / ??";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(6, 93);
            label28.Name = "label28";
            label28.Size = new Size(51, 15);
            label28.TabIndex = 3;
            label28.Text = "Testing: ";
            // 
            // heatingProgressLabel
            // 
            heatingProgressLabel.AutoSize = true;
            heatingProgressLabel.Location = new Point(64, 42);
            heatingProgressLabel.Name = "heatingProgressLabel";
            heatingProgressLabel.Size = new Size(38, 15);
            heatingProgressLabel.TabIndex = 2;
            heatingProgressLabel.Text = "?? / ??";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(6, 42);
            label26.Name = "label26";
            label26.Size = new Size(52, 15);
            label26.TabIndex = 1;
            label26.Text = "Heating:";
            // 
            // heatingProgressBar
            // 
            heatingProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            heatingProgressBar.Location = new Point(6, 60);
            heatingProgressBar.Name = "heatingProgressBar";
            heatingProgressBar.Size = new Size(613, 23);
            heatingProgressBar.TabIndex = 0;
            // 
            // groupBox8
            // 
            groupBox8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox8.Controls.Add(filenameLabel);
            groupBox8.Controls.Add(btnSelectFile);
            groupBox8.Controls.Add(label24);
            groupBox8.Controls.Add(label23);
            groupBox8.Location = new Point(6, 6);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(624, 110);
            groupBox8.TabIndex = 0;
            groupBox8.TabStop = false;
            groupBox8.Text = "Input";
            // 
            // filenameLabel
            // 
            filenameLabel.AutoSize = true;
            filenameLabel.Location = new Point(119, 82);
            filenameLabel.Name = "filenameLabel";
            filenameLabel.Size = new Size(83, 15);
            filenameLabel.TabIndex = 4;
            filenameLabel.Text = "No file chosen";
            // 
            // btnSelectFile
            // 
            btnSelectFile.Location = new Point(6, 78);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new Size(107, 23);
            btnSelectFile.TabIndex = 3;
            btnSelectFile.Text = "Choose CSV file";
            btnSelectFile.UseVisualStyleBackColor = true;
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label24.Location = new Point(6, 55);
            label24.Name = "label24";
            label24.Size = new Size(376, 34);
            label24.TabIndex = 2;
            label24.Text = "p, a, b, curve order, P.x, P.y, P order, Q.x, Q.y, Q order, expected k\r\n\r\n";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(6, 20);
            label23.Name = "label23";
            label23.Size = new Size(341, 30);
            label23.TabIndex = 0;
            label23.Text = "Select CSV file with the dataset to use in test. \r\nPlease, note that the data set should be in the following format:\r\n";
            // 
            // MethodNameGroup
            // 
            MethodNameGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MethodNameGroup.Controls.Add(LasVegas);
            MethodNameGroup.Controls.Add(GrympyGiants);
            MethodNameGroup.Controls.Add(Kangaroo);
            MethodNameGroup.Controls.Add(BSGS);
            MethodNameGroup.Controls.Add(PollardRho);
            MethodNameGroup.Location = new Point(662, 120);
            MethodNameGroup.Name = "MethodNameGroup";
            MethodNameGroup.Size = new Size(163, 171);
            MethodNameGroup.TabIndex = 2;
            MethodNameGroup.TabStop = false;
            MethodNameGroup.Text = "DLP Solving Method";
            // 
            // LasVegas
            // 
            LasVegas.AutoSize = true;
            LasVegas.Location = new Point(6, 138);
            LasVegas.Name = "LasVegas";
            LasVegas.Size = new Size(75, 19);
            LasVegas.TabIndex = 4;
            LasVegas.TabStop = true;
            LasVegas.Tag = "LasVegas";
            LasVegas.Text = "Las Vegas";
            LasVegas.UseVisualStyleBackColor = true;
            // 
            // GrympyGiants
            // 
            GrympyGiants.AllowDrop = true;
            GrympyGiants.AutoSize = true;
            GrympyGiants.Location = new Point(6, 98);
            GrympyGiants.Name = "GrympyGiants";
            GrympyGiants.Size = new Size(132, 34);
            GrympyGiants.TabIndex = 3;
            GrympyGiants.TabStop = true;
            GrympyGiants.Tag = "GrympyGiants";
            GrympyGiants.Text = "Two Grumpy Giants \r\nand a Baby";
            GrympyGiants.UseVisualStyleBackColor = true;
            // 
            // Kangaroo
            // 
            Kangaroo.AutoSize = true;
            Kangaroo.Location = new Point(6, 73);
            Kangaroo.Name = "Kangaroo";
            Kangaroo.Size = new Size(76, 19);
            Kangaroo.TabIndex = 2;
            Kangaroo.TabStop = true;
            Kangaroo.Tag = "Kangaroo";
            Kangaroo.Text = "Kangaroo";
            Kangaroo.UseVisualStyleBackColor = true;
            // 
            // BSGS
            // 
            BSGS.AutoSize = true;
            BSGS.Location = new Point(6, 48);
            BSGS.Name = "BSGS";
            BSGS.Size = new Size(134, 19);
            BSGS.TabIndex = 1;
            BSGS.TabStop = true;
            BSGS.Tag = "BSGS";
            BSGS.Text = "Baby Step Giant Step";
            BSGS.UseVisualStyleBackColor = true;
            // 
            // PollardRho
            // 
            PollardRho.AutoSize = true;
            PollardRho.Checked = true;
            PollardRho.Location = new Point(6, 23);
            PollardRho.Name = "PollardRho";
            PollardRho.Size = new Size(86, 19);
            PollardRho.TabIndex = 0;
            PollardRho.TabStop = true;
            PollardRho.Tag = "PollardRho";
            PollardRho.Text = "Pollard Rho";
            PollardRho.UseVisualStyleBackColor = true;
            PollardRho.CheckedChanged += PollardRho_CheckedChanged;
            // 
            // CurveNameGroup
            // 
            CurveNameGroup.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CurveNameGroup.Controls.Add(Edwards);
            CurveNameGroup.Controls.Add(Weierstrass);
            CurveNameGroup.Location = new Point(662, 36);
            CurveNameGroup.Name = "CurveNameGroup";
            CurveNameGroup.Size = new Size(163, 78);
            CurveNameGroup.TabIndex = 3;
            CurveNameGroup.TabStop = false;
            CurveNameGroup.Text = "Elliptic Curve";
            // 
            // Edwards
            // 
            Edwards.AutoSize = true;
            Edwards.Enabled = false;
            Edwards.Location = new Point(6, 47);
            Edwards.Name = "Edwards";
            Edwards.Size = new Size(69, 19);
            Edwards.TabIndex = 1;
            Edwards.Tag = "Edwards";
            Edwards.Text = "Edwards";
            Edwards.UseVisualStyleBackColor = true;
            // 
            // Weierstrass
            // 
            Weierstrass.AutoSize = true;
            Weierstrass.Checked = true;
            Weierstrass.Location = new Point(6, 22);
            Weierstrass.Name = "Weierstrass";
            Weierstrass.Size = new Size(84, 19);
            Weierstrass.TabIndex = 0;
            Weierstrass.TabStop = true;
            Weierstrass.Tag = "Weierstrass";
            Weierstrass.Text = "Weierstrass";
            Weierstrass.UseVisualStyleBackColor = true;
            // 
            // UsePohligHellmanCheckbox
            // 
            UsePohligHellmanCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UsePohligHellmanCheckbox.AutoSize = true;
            UsePohligHellmanCheckbox.Checked = true;
            UsePohligHellmanCheckbox.CheckState = CheckState.Checked;
            UsePohligHellmanCheckbox.Location = new Point(658, 297);
            UsePohligHellmanCheckbox.Name = "UsePohligHellmanCheckbox";
            UsePohligHellmanCheckbox.Size = new Size(177, 19);
            UsePohligHellmanCheckbox.TabIndex = 5;
            UsePohligHellmanCheckbox.Text = "Use Pohlig-Hellman method";
            UsePohligHellmanCheckbox.UseVisualStyleBackColor = true;
            // 
            // UseNegationMapsCheckbox
            // 
            UseNegationMapsCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UseNegationMapsCheckbox.AutoSize = true;
            UseNegationMapsCheckbox.Location = new Point(658, 322);
            UseNegationMapsCheckbox.Name = "UseNegationMapsCheckbox";
            UseNegationMapsCheckbox.Size = new Size(127, 19);
            UseNegationMapsCheckbox.TabIndex = 4;
            UseNegationMapsCheckbox.Text = "Use negation maps";
            UseNegationMapsCheckbox.UseVisualStyleBackColor = true;
            // 
            // UseExtendedNegationMapsCheckbox
            // 
            UseExtendedNegationMapsCheckbox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UseExtendedNegationMapsCheckbox.AutoSize = true;
            UseExtendedNegationMapsCheckbox.Location = new Point(658, 347);
            UseExtendedNegationMapsCheckbox.Name = "UseExtendedNegationMapsCheckbox";
            UseExtendedNegationMapsCheckbox.Size = new Size(178, 19);
            UseExtendedNegationMapsCheckbox.TabIndex = 6;
            UseExtendedNegationMapsCheckbox.Text = "Use extended negation maps";
            UseExtendedNegationMapsCheckbox.UseVisualStyleBackColor = true;
            UseExtendedNegationMapsCheckbox.CheckedChanged += UseExtendedNegationMapsCheckbox_CheckedChanged;
            // 
            // RunTest
            // 
            RunTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            RunTest.Location = new Point(662, 566);
            RunTest.Name = "RunTest";
            RunTest.Size = new Size(163, 23);
            RunTest.TabIndex = 7;
            RunTest.Text = "Run test";
            RunTest.UseVisualStyleBackColor = true;
            RunTest.Click += RunTest_Click;
            // 
            // UserInterface
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(837, 605);
            Controls.Add(RunTest);
            Controls.Add(UseExtendedNegationMapsCheckbox);
            Controls.Add(UseNegationMapsCheckbox);
            Controls.Add(UsePohligHellmanCheckbox);
            Controls.Add(CurveNameGroup);
            Controls.Add(MethodNameGroup);
            Controls.Add(tabControl);
            Controls.Add(label1);
            Name = "UserInterface";
            Text = "UserInterface";
            Load += UserInterface_Load;
            tabControl.ResumeLayout(false);
            SingleCurveTest.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            YPointsGroup.ResumeLayout(false);
            YPointsGroup.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            resultsGroupBlock.ResumeLayout(false);
            resultsGroupBlock.PerformLayout();
            BulkTest.ResumeLayout(false);
            BulkResultsBlock.ResumeLayout(false);
            BulkResultsBlock.PerformLayout();
            ProccessingGroup.ResumeLayout(false);
            ProccessingGroup.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            MethodNameGroup.ResumeLayout(false);
            MethodNameGroup.PerformLayout();
            CurveNameGroup.ResumeLayout(false);
            CurveNameGroup.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TabControl tabControl;
        private TabPage SingleCurveTest;
        private TabPage BulkTest;
        private GroupBox MethodNameGroup;
        private RadioButton Kangaroo;
        private RadioButton BSGS;
        private RadioButton PollardRho;
        private GroupBox CurveNameGroup;
        private RadioButton Edwards;
        private RadioButton Weierstrass;
        private RadioButton GrympyGiants;
        private CheckBox UsePohligHellmanCheckbox;
        private RadioButton LasVegas;
        private Label label2;
        private GroupBox groupBox3;
        private Label label3;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox curveOrderValue;
        private TextBox bValue;
        private TextBox aValue;
        private TextBox pValue;
        private GroupBox groupBox4;
        private Label label9;
        private Label label8;
        private CheckBox UseNegationMapsCheckbox;
        private CheckBox UseExtendedNegationMapsCheckbox;
        private Button RunTest;
        private TextBox QxValue;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private TextBox PyValue;
        private TextBox PxValue;
        private TextBox QyValue;
        private GroupBox groupBox5;
        private Label label14;
        private Label label15;
        private GroupBox YPointsGroup;
        private Label label16;
        private TextBox CSVParserData;
        private Button autofillButton;
        private TextBox Y3xValue;
        private Label label19;
        private Label label17;
        private Label label18;
        private TextBox Y2xValue;
        private TextBox Y1xValue;
        private Label label20;
        private TextBox expectedKValue;
        private GroupBox resultsGroupBlock;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label ResultTextError;
        private Label ResultText;
        private GroupBox groupBox8;
        private Label label23;
        private Button btnSelectFile;
        private Label label24;
        private Label filenameLabel;
        private GroupBox ProccessingGroup;
        private ProgressBar proccessingProgressBar;
        private Label proccessingProgressLabel;
        private Label label28;
        private Label heatingProgressLabel;
        private Label label26;
        private ProgressBar heatingProgressBar;
        private Label TotalItemsLabel;
        private Label label30;
        private Panel panelChart;
        private GroupBox BulkResultsBlock;
        private Label BulkResultText;
        private Label BulkResultTextError;
    }
}