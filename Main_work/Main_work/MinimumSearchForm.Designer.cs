namespace Main_work
{
    partial class MinimumSearchForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawField = new System.Windows.Forms.PictureBox();
            this.EnterTypeLabel = new System.Windows.Forms.Label();
            this.RB_Piyavsky = new System.Windows.Forms.RadioButton();
            this.RB_Scan = new System.Windows.Forms.RadioButton();
            this.RB_Strongin = new System.Windows.Forms.RadioButton();
            this.FunctionLabel = new System.Windows.Forms.Label();
            this.FunctionLabelValue = new System.Windows.Forms.Label();
            this.FSin_value = new System.Windows.Forms.TextBox();
            this.SSin_value = new System.Windows.Forms.TextBox();
            this.SCos_value = new System.Windows.Forms.TextBox();
            this.FCos_value = new System.Windows.Forms.TextBox();
            this.FSin_parameter = new System.Windows.Forms.Label();
            this.SSin_parameter = new System.Windows.Forms.Label();
            this.SCos_parameter = new System.Windows.Forms.Label();
            this.FCos_parameter = new System.Windows.Forms.Label();
            this.EnterParametersLabel = new System.Windows.Forms.Label();
            this.Save_is_correct_label = new System.Windows.Forms.Label();
            this.Save_is_not_correct_label = new System.Windows.Forms.Label();
            this.ApplyNewParameter_Button = new System.Windows.Forms.Button();
            this.minimumXLabel = new System.Windows.Forms.Label();
            this.maximumXLabel = new System.Windows.Forms.Label();
            this.MinValueX = new System.Windows.Forms.TextBox();
            this.MaxValueX = new System.Windows.Forms.TextBox();
            this.StopLabel = new System.Windows.Forms.Label();
            this.MaxSectionDistance = new System.Windows.Forms.TextBox();
            this.StartSerchButton = new System.Windows.Forms.Button();
            this.TimeToPauseLabel = new System.Windows.Forms.Label();
            this.TimeToPauseValue = new System.Windows.Forms.TextBox();
            this.SearchResultLabel = new System.Windows.Forms.Label();
            this.SearchResultValue = new System.Windows.Forms.TextBox();
            this.RParameterLabel = new System.Windows.Forms.Label();
            this.RParameterValue = new System.Windows.Forms.TextBox();
            this.FindedXValue = new System.Windows.Forms.TextBox();
            this.FindedXLabel = new System.Windows.Forms.Label();
            this.IterationCountValue = new System.Windows.Forms.TextBox();
            this.IterationCountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DrawField)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawField
            // 
            this.DrawField.BackColor = System.Drawing.SystemColors.Desktop;
            this.DrawField.Location = new System.Drawing.Point(12, 12);
            this.DrawField.Name = "DrawField";
            this.DrawField.Size = new System.Drawing.Size(1163, 717);
            this.DrawField.TabIndex = 0;
            this.DrawField.TabStop = false;
            // 
            // EnterTypeLabel
            // 
            this.EnterTypeLabel.AutoSize = true;
            this.EnterTypeLabel.Location = new System.Drawing.Point(1181, 12);
            this.EnterTypeLabel.Name = "EnterTypeLabel";
            this.EnterTypeLabel.Size = new System.Drawing.Size(249, 13);
            this.EnterTypeLabel.TabIndex = 1;
            this.EnterTypeLabel.Text = "Выберите тип характерестического алгоритма:";
            // 
            // RB_Piyavsky
            // 
            this.RB_Piyavsky.AutoSize = true;
            this.RB_Piyavsky.Location = new System.Drawing.Point(1181, 74);
            this.RB_Piyavsky.Name = "RB_Piyavsky";
            this.RB_Piyavsky.Size = new System.Drawing.Size(220, 17);
            this.RB_Piyavsky.TabIndex = 2;
            this.RB_Piyavsky.TabStop = true;
            this.RB_Piyavsky.Text = "Метод ломаных (алгоритм Пиявского)";
            this.RB_Piyavsky.UseVisualStyleBackColor = true;
            this.RB_Piyavsky.CheckedChanged += new System.EventHandler(this.RB_Piyavsky_CheckedChanged);
            // 
            // RB_Scan
            // 
            this.RB_Scan.AutoSize = true;
            this.RB_Scan.Location = new System.Drawing.Point(1181, 28);
            this.RB_Scan.Name = "RB_Scan";
            this.RB_Scan.Size = new System.Drawing.Size(132, 17);
            this.RB_Scan.TabIndex = 3;
            this.RB_Scan.TabStop = true;
            this.RB_Scan.Text = "Метод сканирования";
            this.RB_Scan.UseVisualStyleBackColor = true;
            this.RB_Scan.CheckedChanged += new System.EventHandler(this.RB_Scan_CheckedChanged);
            // 
            // RB_Strongin
            // 
            this.RB_Strongin.AutoSize = true;
            this.RB_Strongin.Location = new System.Drawing.Point(1181, 51);
            this.RB_Strongin.Name = "RB_Strongin";
            this.RB_Strongin.Size = new System.Drawing.Size(273, 17);
            this.RB_Strongin.TabIndex = 4;
            this.RB_Strongin.TabStop = true;
            this.RB_Strongin.Text = "Базовый инфо-статич. АГП(Алгоритм Стронгина)";
            this.RB_Strongin.UseVisualStyleBackColor = true;
            this.RB_Strongin.CheckedChanged += new System.EventHandler(this.RB_Strongin_CheckedChanged);
            // 
            // FunctionLabel
            // 
            this.FunctionLabel.AutoSize = true;
            this.FunctionLabel.Location = new System.Drawing.Point(12, 774);
            this.FunctionLabel.Name = "FunctionLabel";
            this.FunctionLabel.Size = new System.Drawing.Size(150, 13);
            this.FunctionLabel.TabIndex = 5;
            this.FunctionLabel.Text = "Рассматриваемая функция:";
            // 
            // FunctionLabelValue
            // 
            this.FunctionLabelValue.AutoSize = true;
            this.FunctionLabelValue.Location = new System.Drawing.Point(169, 774);
            this.FunctionLabelValue.Name = "FunctionLabelValue";
            this.FunctionLabelValue.Size = new System.Drawing.Size(169, 13);
            this.FunctionLabelValue.TabIndex = 6;
            this.FunctionLabelValue.Text = "f(x) = A * sin (b * x) + B * cos (a * x)";
            // 
            // FSin_value
            // 
            this.FSin_value.Location = new System.Drawing.Point(1229, 158);
            this.FSin_value.Name = "FSin_value";
            this.FSin_value.Size = new System.Drawing.Size(100, 20);
            this.FSin_value.TabIndex = 7;
            // 
            // SSin_value
            // 
            this.SSin_value.Location = new System.Drawing.Point(1363, 158);
            this.SSin_value.Name = "SSin_value";
            this.SSin_value.Size = new System.Drawing.Size(100, 20);
            this.SSin_value.TabIndex = 8;
            // 
            // SCos_value
            // 
            this.SCos_value.Location = new System.Drawing.Point(1363, 184);
            this.SCos_value.Name = "SCos_value";
            this.SCos_value.Size = new System.Drawing.Size(100, 20);
            this.SCos_value.TabIndex = 10;
            // 
            // FCos_value
            // 
            this.FCos_value.Location = new System.Drawing.Point(1229, 184);
            this.FCos_value.Name = "FCos_value";
            this.FCos_value.Size = new System.Drawing.Size(100, 20);
            this.FCos_value.TabIndex = 9;
            // 
            // FSin_parameter
            // 
            this.FSin_parameter.AutoSize = true;
            this.FSin_parameter.Location = new System.Drawing.Point(1200, 161);
            this.FSin_parameter.Name = "FSin_parameter";
            this.FSin_parameter.Size = new System.Drawing.Size(23, 13);
            this.FSin_parameter.TabIndex = 11;
            this.FSin_parameter.Text = "A =";
            // 
            // SSin_parameter
            // 
            this.SSin_parameter.AutoSize = true;
            this.SSin_parameter.Location = new System.Drawing.Point(1335, 161);
            this.SSin_parameter.Name = "SSin_parameter";
            this.SSin_parameter.Size = new System.Drawing.Size(22, 13);
            this.SSin_parameter.TabIndex = 12;
            this.SSin_parameter.Text = "b =";
            // 
            // SCos_parameter
            // 
            this.SCos_parameter.AutoSize = true;
            this.SCos_parameter.Location = new System.Drawing.Point(1335, 187);
            this.SCos_parameter.Name = "SCos_parameter";
            this.SCos_parameter.Size = new System.Drawing.Size(22, 13);
            this.SCos_parameter.TabIndex = 14;
            this.SCos_parameter.Text = "a =";
            // 
            // FCos_parameter
            // 
            this.FCos_parameter.AutoSize = true;
            this.FCos_parameter.Location = new System.Drawing.Point(1200, 187);
            this.FCos_parameter.Name = "FCos_parameter";
            this.FCos_parameter.Size = new System.Drawing.Size(23, 13);
            this.FCos_parameter.TabIndex = 13;
            this.FCos_parameter.Text = "B =";
            // 
            // EnterParametersLabel
            // 
            this.EnterParametersLabel.AutoSize = true;
            this.EnterParametersLabel.Location = new System.Drawing.Point(1226, 131);
            this.EnterParametersLabel.Name = "EnterParametersLabel";
            this.EnterParametersLabel.Size = new System.Drawing.Size(228, 13);
            this.EnterParametersLabel.TabIndex = 15;
            this.EnterParametersLabel.Text = "Введите параметры исследуемой функции:";
            // 
            // Save_is_correct_label
            // 
            this.Save_is_correct_label.AutoSize = true;
            this.Save_is_correct_label.ForeColor = System.Drawing.Color.LimeGreen;
            this.Save_is_correct_label.Location = new System.Drawing.Point(1245, 207);
            this.Save_is_correct_label.Name = "Save_is_correct_label";
            this.Save_is_correct_label.Size = new System.Drawing.Size(170, 13);
            this.Save_is_correct_label.TabIndex = 16;
            this.Save_is_correct_label.Text = "Параметры успешно сохранены";
            // 
            // Save_is_not_correct_label
            // 
            this.Save_is_not_correct_label.AutoSize = true;
            this.Save_is_not_correct_label.ForeColor = System.Drawing.Color.Red;
            this.Save_is_not_correct_label.Location = new System.Drawing.Point(1214, 207);
            this.Save_is_not_correct_label.Name = "Save_is_not_correct_label";
            this.Save_is_not_correct_label.Size = new System.Drawing.Size(240, 13);
            this.Save_is_not_correct_label.TabIndex = 17;
            this.Save_is_not_correct_label.Text = "Ошибка. Проверьте корректность парамеров";
            // 
            // ApplyNewParameter_Button
            // 
            this.ApplyNewParameter_Button.Location = new System.Drawing.Point(1203, 223);
            this.ApplyNewParameter_Button.Name = "ApplyNewParameter_Button";
            this.ApplyNewParameter_Button.Size = new System.Drawing.Size(260, 23);
            this.ApplyNewParameter_Button.TabIndex = 18;
            this.ApplyNewParameter_Button.Text = "Установить новые параметры";
            this.ApplyNewParameter_Button.UseVisualStyleBackColor = true;
            this.ApplyNewParameter_Button.Click += new System.EventHandler(this.ApplyNewParameter_Button_Click);
            // 
            // minimumXLabel
            // 
            this.minimumXLabel.AutoSize = true;
            this.minimumXLabel.Location = new System.Drawing.Point(1200, 261);
            this.minimumXLabel.Name = "minimumXLabel";
            this.minimumXLabel.Size = new System.Drawing.Size(69, 13);
            this.minimumXLabel.TabIndex = 19;
            this.minimumXLabel.Text = "minValueX = ";
            // 
            // maximumXLabel
            // 
            this.maximumXLabel.AutoSize = true;
            this.maximumXLabel.Location = new System.Drawing.Point(1335, 261);
            this.maximumXLabel.Name = "maximumXLabel";
            this.maximumXLabel.Size = new System.Drawing.Size(72, 13);
            this.maximumXLabel.TabIndex = 20;
            this.maximumXLabel.Text = "maxValueX = ";
            // 
            // MinValueX
            // 
            this.MinValueX.Location = new System.Drawing.Point(1266, 258);
            this.MinValueX.Name = "MinValueX";
            this.MinValueX.Size = new System.Drawing.Size(63, 20);
            this.MinValueX.TabIndex = 21;
            this.MinValueX.Text = "1";
            // 
            // MaxValueX
            // 
            this.MaxValueX.Location = new System.Drawing.Point(1400, 258);
            this.MaxValueX.Name = "MaxValueX";
            this.MaxValueX.Size = new System.Drawing.Size(63, 20);
            this.MaxValueX.TabIndex = 22;
            this.MaxValueX.Text = "10";
            // 
            // StopLabel
            // 
            this.StopLabel.AutoSize = true;
            this.StopLabel.Location = new System.Drawing.Point(1200, 287);
            this.StopLabel.Name = "StopLabel";
            this.StopLabel.Size = new System.Drawing.Size(104, 13);
            this.StopLabel.TabIndex = 25;
            this.StopLabel.Text = "Признак останова:";
            // 
            // MaxSectionDistance
            // 
            this.MaxSectionDistance.Location = new System.Drawing.Point(1400, 284);
            this.MaxSectionDistance.Name = "MaxSectionDistance";
            this.MaxSectionDistance.Size = new System.Drawing.Size(63, 20);
            this.MaxSectionDistance.TabIndex = 27;
            this.MaxSectionDistance.Text = "0,1";
            // 
            // StartSerchButton
            // 
            this.StartSerchButton.Location = new System.Drawing.Point(1203, 336);
            this.StartSerchButton.Name = "StartSerchButton";
            this.StartSerchButton.Size = new System.Drawing.Size(260, 23);
            this.StartSerchButton.TabIndex = 28;
            this.StartSerchButton.Text = "Поиск минимума с данными параметрами";
            this.StartSerchButton.UseVisualStyleBackColor = true;
            this.StartSerchButton.Click += new System.EventHandler(this.StartSerchButton_Click);
            // 
            // TimeToPauseLabel
            // 
            this.TimeToPauseLabel.AutoSize = true;
            this.TimeToPauseLabel.Location = new System.Drawing.Point(1200, 313);
            this.TimeToPauseLabel.Name = "TimeToPauseLabel";
            this.TimeToPauseLabel.Size = new System.Drawing.Size(161, 13);
            this.TimeToPauseLabel.TabIndex = 29;
            this.TimeToPauseLabel.Text = "Замедление выполнения (мс):";
            // 
            // TimeToPauseValue
            // 
            this.TimeToPauseValue.Location = new System.Drawing.Point(1400, 310);
            this.TimeToPauseValue.Name = "TimeToPauseValue";
            this.TimeToPauseValue.Size = new System.Drawing.Size(63, 20);
            this.TimeToPauseValue.TabIndex = 30;
            this.TimeToPauseValue.Text = "0";
            // 
            // SearchResultLabel
            // 
            this.SearchResultLabel.AutoSize = true;
            this.SearchResultLabel.Location = new System.Drawing.Point(1200, 371);
            this.SearchResultLabel.Name = "SearchResultLabel";
            this.SearchResultLabel.Size = new System.Drawing.Size(118, 13);
            this.SearchResultLabel.TabIndex = 31;
            this.SearchResultLabel.Text = "Найденный минимум:";
            // 
            // SearchResultValue
            // 
            this.SearchResultValue.Location = new System.Drawing.Point(1354, 368);
            this.SearchResultValue.Name = "SearchResultValue";
            this.SearchResultValue.ReadOnly = true;
            this.SearchResultValue.Size = new System.Drawing.Size(109, 20);
            this.SearchResultValue.TabIndex = 32;
            // 
            // RParameterLabel
            // 
            this.RParameterLabel.AutoSize = true;
            this.RParameterLabel.Location = new System.Drawing.Point(1190, 103);
            this.RParameterLabel.Name = "RParameterLabel";
            this.RParameterLabel.Size = new System.Drawing.Size(104, 13);
            this.RParameterLabel.TabIndex = 33;
            this.RParameterLabel.Text = "Введите параметр ";
            // 
            // RParameterValue
            // 
            this.RParameterValue.Location = new System.Drawing.Point(1300, 100);
            this.RParameterValue.Name = "RParameterValue";
            this.RParameterValue.Size = new System.Drawing.Size(107, 20);
            this.RParameterValue.TabIndex = 34;
            this.RParameterValue.Text = "2,5";
            // 
            // FindedXValue
            // 
            this.FindedXValue.Location = new System.Drawing.Point(1354, 392);
            this.FindedXValue.Name = "FindedXValue";
            this.FindedXValue.ReadOnly = true;
            this.FindedXValue.Size = new System.Drawing.Size(109, 20);
            this.FindedXValue.TabIndex = 36;
            // 
            // FindedXLabel
            // 
            this.FindedXLabel.AutoSize = true;
            this.FindedXLabel.Location = new System.Drawing.Point(1201, 395);
            this.FindedXLabel.Name = "FindedXLabel";
            this.FindedXLabel.Size = new System.Drawing.Size(112, 13);
            this.FindedXLabel.TabIndex = 35;
            this.FindedXLabel.Text = "Соответствующий Х:";
            // 
            // IterationCountValue
            // 
            this.IterationCountValue.Location = new System.Drawing.Point(1354, 418);
            this.IterationCountValue.Name = "IterationCountValue";
            this.IterationCountValue.ReadOnly = true;
            this.IterationCountValue.Size = new System.Drawing.Size(109, 20);
            this.IterationCountValue.TabIndex = 38;
            // 
            // IterationCountLabel
            // 
            this.IterationCountLabel.AutoSize = true;
            this.IterationCountLabel.Location = new System.Drawing.Point(1201, 421);
            this.IterationCountLabel.Name = "IterationCountLabel";
            this.IterationCountLabel.Size = new System.Drawing.Size(149, 13);
            this.IterationCountLabel.TabIndex = 37;
            this.IterationCountLabel.Text = "Число итераций алгоритма:";
            // 
            // MinimumSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 796);
            this.Controls.Add(this.IterationCountValue);
            this.Controls.Add(this.IterationCountLabel);
            this.Controls.Add(this.FindedXValue);
            this.Controls.Add(this.FindedXLabel);
            this.Controls.Add(this.RParameterValue);
            this.Controls.Add(this.RParameterLabel);
            this.Controls.Add(this.SearchResultValue);
            this.Controls.Add(this.SearchResultLabel);
            this.Controls.Add(this.TimeToPauseValue);
            this.Controls.Add(this.TimeToPauseLabel);
            this.Controls.Add(this.StartSerchButton);
            this.Controls.Add(this.MaxSectionDistance);
            this.Controls.Add(this.StopLabel);
            this.Controls.Add(this.MaxValueX);
            this.Controls.Add(this.MinValueX);
            this.Controls.Add(this.maximumXLabel);
            this.Controls.Add(this.minimumXLabel);
            this.Controls.Add(this.ApplyNewParameter_Button);
            this.Controls.Add(this.Save_is_not_correct_label);
            this.Controls.Add(this.Save_is_correct_label);
            this.Controls.Add(this.EnterParametersLabel);
            this.Controls.Add(this.SCos_parameter);
            this.Controls.Add(this.FCos_parameter);
            this.Controls.Add(this.SSin_parameter);
            this.Controls.Add(this.FSin_parameter);
            this.Controls.Add(this.SCos_value);
            this.Controls.Add(this.FCos_value);
            this.Controls.Add(this.SSin_value);
            this.Controls.Add(this.FSin_value);
            this.Controls.Add(this.FunctionLabelValue);
            this.Controls.Add(this.FunctionLabel);
            this.Controls.Add(this.RB_Strongin);
            this.Controls.Add(this.RB_Scan);
            this.Controls.Add(this.RB_Piyavsky);
            this.Controls.Add(this.EnterTypeLabel);
            this.Controls.Add(this.DrawField);
            this.Name = "MinimumSearchForm";
            this.Text = "Вычисление минимума функции";
            ((System.ComponentModel.ISupportInitialize)(this.DrawField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawField;
        private System.Windows.Forms.Label EnterTypeLabel;
        private System.Windows.Forms.RadioButton RB_Piyavsky;
        private System.Windows.Forms.RadioButton RB_Scan;
        private System.Windows.Forms.RadioButton RB_Strongin;
        private System.Windows.Forms.Label FunctionLabel;
        private System.Windows.Forms.Label FunctionLabelValue;
        private System.Windows.Forms.TextBox FSin_value;
        private System.Windows.Forms.TextBox SSin_value;
        private System.Windows.Forms.TextBox SCos_value;
        private System.Windows.Forms.TextBox FCos_value;
        private System.Windows.Forms.Label FSin_parameter;
        private System.Windows.Forms.Label SSin_parameter;
        private System.Windows.Forms.Label SCos_parameter;
        private System.Windows.Forms.Label FCos_parameter;
        private System.Windows.Forms.Label EnterParametersLabel;
        private System.Windows.Forms.Label Save_is_correct_label;
        private System.Windows.Forms.Label Save_is_not_correct_label;
        private System.Windows.Forms.Button ApplyNewParameter_Button;
        private System.Windows.Forms.Label minimumXLabel;
        private System.Windows.Forms.Label maximumXLabel;
        private System.Windows.Forms.TextBox MinValueX;
        private System.Windows.Forms.TextBox MaxValueX;
        private System.Windows.Forms.Label StopLabel;
        private System.Windows.Forms.TextBox MaxSectionDistance;
        private System.Windows.Forms.Button StartSerchButton;
        private System.Windows.Forms.Label TimeToPauseLabel;
        private System.Windows.Forms.TextBox TimeToPauseValue;
        private System.Windows.Forms.Label SearchResultLabel;
        private System.Windows.Forms.TextBox SearchResultValue;
        private System.Windows.Forms.Label RParameterLabel;
        private System.Windows.Forms.TextBox RParameterValue;
        private System.Windows.Forms.TextBox FindedXValue;
        private System.Windows.Forms.Label FindedXLabel;
        private System.Windows.Forms.TextBox IterationCountValue;
        private System.Windows.Forms.Label IterationCountLabel;
    }
}

