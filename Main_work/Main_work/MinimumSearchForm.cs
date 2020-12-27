using Main_work.HelpClasses;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Main_work
{
    public partial class MinimumSearchForm : Form
    {
        private MinimumSearchMethodExecutor _minExecutor;

        // отношение количество пикселей на единицу значения X
        private double _pixelXValue;
        // отношение количество пикселей на единицу значения Y
        private double _pixelYValue;

        private int _pixelXCoordZero;
        private int _pixelYCoordZero;

        public MinimumSearchForm()
        {
            var function = new FunctionInformation();
            _minExecutor = new MinimumSearchMethodExecutor(function, this);
            InitializeComponent();
            RB_Scan.Select();

            RParameterLabel.Hide();
            RParameterValue.Hide();

            Save_is_correct_label.Hide();
            Save_is_not_correct_label.Hide();

            FSin_value.Text = "1";
            SSin_value.Text = "1";
            FCos_value.Text = "1";
            SCos_value.Text = "1";
        }

        private void RB_Piyavsky_CheckedChanged(object sender, EventArgs e)
        {
            RParameterLabel.Show();
            RParameterValue.Show();
            _minExecutor.ChangeSearchMethod(MethodType.Polyline);
        }

        private void RB_Strongin_CheckedChanged(object sender, EventArgs e)
        {
            RParameterLabel.Show();
            RParameterValue.Show();
            _minExecutor.ChangeSearchMethod(MethodType.InfoStatisticAlgoritm);
        }

        private void RB_Scan_CheckedChanged(object sender, EventArgs e)
        {
            RParameterLabel.Hide();
            RParameterValue.Hide();
            _minExecutor.ChangeSearchMethod(MethodType.ScanMethod);
        }

        private void ApplyNewParameter_Button_Click(object sender, EventArgs e)
        {
            var resBool = _minExecutor.SetFunctionValue(FSin_value.Text, SSin_value.Text, FCos_value.Text, SCos_value.Text);
            if (resBool)
            {
                Save_is_not_correct_label.Hide();
                Save_is_correct_label.Show();
                Thread.Sleep(500);
                Save_is_correct_label.Hide();
            }
            else
            {                
                Save_is_correct_label.Hide();
                Save_is_not_correct_label.Show();
                Thread.Sleep(500);
                Save_is_not_correct_label.Hide();
            }
        }
        
        private int GetCorrectYCoord(int currCoord)
        {
            return DrawField.Size.Height - currCoord;
        }

        private void StartSerchButton_Click(object sender, EventArgs e)
        {
            var isStarted = _minExecutor.StartMinimumSearch(MinValueX.Text, MaxValueX.Text, MaxSectionDistance.Text, TimeToPauseValue.Text, RParameterValue.Text);
        }

        public void ClearDrawField()
        {

            double step = Math.Round((_minExecutor.MaxValueX - _minExecutor.MinValueX) / 20, 2);
            double iterator = Math.Round((_minExecutor.MinValueX > 0) ? _minExecutor.MinValueX : _minExecutor.MinValueX + step, 1);

            int pixX, pixY;


            Graphics g = DrawField.CreateGraphics();
            g.Clear(Color.Black);

            _pixelXValue = ((_minExecutor.MaxValueX - _minExecutor.MinValueX) / DrawField.Width);
            _pixelYValue = ((_minExecutor.MaxValueY - _minExecutor.MinValueY) / DrawField.Height);

            if (_minExecutor.MinValueX > 0)
                _pixelXCoordZero = 2;
            else
                _pixelXCoordZero = (int)((0.0 - _minExecutor.MinValueX) / _pixelXValue);

            if (_minExecutor.MinValueY > 0)
                _pixelYCoordZero = 2;
            else
                _pixelYCoordZero = (int)((0.0 - _minExecutor.MinValueY) / _pixelYValue);

            #region Y-Ось
            // Ось Y
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(0)),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)));

            // + Стрелочки
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)),
                new Point(_pixelXCoordZero - 2, GetCorrectYCoord(DrawField.Size.Height - 5)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)),
                new Point(_pixelXCoordZero + 2, GetCorrectYCoord(DrawField.Size.Height - 5)));

            // + Координаты
            step = Math.Round((_minExecutor.MaxValueY - _minExecutor.MinValueY) / 20, 2);
            iterator = Math.Round((_minExecutor.MinValueY > 0) ? _minExecutor.MinValueY : _minExecutor.MinValueY + step, 1);

            while (iterator <= _minExecutor.MaxValueX - step)
            {
                GetCorrectPixelForDraw(0.0, iterator, out pixX, out pixY);
                g.DrawLine(new Pen(Brushes.Red, 2),
                    new Point(pixX - 2, GetCorrectYCoord(pixY)),
                    new Point(pixX + 2, GetCorrectYCoord(pixY)));
                g.DrawString(iterator.ToString(),
                    new Font(this.Font, FontStyle.Italic),
                    Brushes.OrangeRed,
                    pixX + 6, GetCorrectYCoord(pixY - 3));
                iterator += step;
                iterator = Math.Round(iterator, 2);
            }
            #endregion

            #region X-Ось
            // Ось X
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(0, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)));

            // + Стрелочки
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 5, GetCorrectYCoord(_pixelYCoordZero - 2)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 5, GetCorrectYCoord(_pixelYCoordZero + 2)));


            // + Координаты
            step = Math.Round((_minExecutor.MaxValueX - _minExecutor.MinValueX) / 20, 2);
            iterator = Math.Round((_minExecutor.MinValueX > 0) ? _minExecutor.MinValueX : _minExecutor.MinValueX + step, 1);

            while (iterator <= _minExecutor.MaxValueX - step)
            {
                GetCorrectPixelForDraw(iterator, 0.0, out pixX, out pixY);
                g.DrawLine(new Pen(Brushes.Red, 2),
                    new Point(pixX, GetCorrectYCoord(pixY + 4)),
                    new Point(pixX, GetCorrectYCoord(pixY - 2)));
                g.DrawString(iterator.ToString(),
                    new Font(this.Font, FontStyle.Italic),
                    Brushes.OrangeRed,
                    pixX - 3, GetCorrectYCoord(pixY - 5));
                iterator += step;
                iterator = Math.Round(iterator, 2);
            }
            #endregion

            // Сама Функция
            _minExecutor.DrawFunction(_minExecutor.MinValueX, _minExecutor.MaxValueX, Brushes.Pink);
        }

        void GetCorrectPixelForDraw(double valueX, double valueY, out int X, out int Y)
        {
            X = (int)(valueX / _pixelXValue) + _pixelXCoordZero;
            Y = (int)(valueY / _pixelYValue) + _pixelYCoordZero;

            if (Y < 0)
                Y = 2;
            if (Y > DrawField.Height)
                Y = DrawField.Height - 2;
        }

        public void DrawPoint(double XCoord, double YCoord)
        {
            int xPixel, yPixel;
            GetCorrectPixelForDraw(XCoord, YCoord, out xPixel, out yPixel);

            Graphics g = DrawField.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Gold, 1),
                new Point(xPixel, GetCorrectYCoord(_pixelYCoordZero - 2)),
                new Point(xPixel, GetCorrectYCoord(_pixelYCoordZero + 2)));  
        } 

        public void DrawSinglePoint(double XCoord, double YCoord, Brush color)
        {
            int xPixel, yPixel;
            GetCorrectPixelForDraw(XCoord, YCoord, out xPixel, out yPixel);

            Graphics g = DrawField.CreateGraphics();
            g.FillRectangle(color, xPixel, GetCorrectYCoord(yPixel), 1, 1);
        }

        public void SetFindedMinimum(double minimum, double xCoord)
        {
            int minPixX, minPixY;
            bool flagY = (minimum > 0);
            bool flagX = (xCoord > 0);
            GetCorrectPixelForDraw(xCoord, minimum, out minPixX, out minPixY);
            
            Graphics g = DrawField.CreateGraphics();
            var myPen = new Pen(Brushes.GreenYellow, 1);
            myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            // Отрисовка перпендикуляра к оси Х
            g.DrawLine(myPen,
                new Point(minPixX, GetCorrectYCoord(minPixY)),
                new Point(minPixX, GetCorrectYCoord(_pixelYCoordZero)));

            // Отображение значения
            g.DrawString(xCoord.ToString(),
                new Font(this.Font, FontStyle.Italic),
                Brushes.GreenYellow,
                minPixX - 3, GetCorrectYCoord((flagY) ? _pixelYCoordZero - 10 : _pixelYCoordZero + 20));


            // Отрисовка перпендикуляра к оси Y
            g.DrawLine(myPen,
                new Point(minPixX, GetCorrectYCoord(minPixY)),
                new Point(_pixelXCoordZero, GetCorrectYCoord(minPixY)));

            // Отображение значения
            g.DrawString(minimum.ToString(),
                new Font(this.Font, FontStyle.Italic),
                Brushes.GreenYellow,
                (flagX) ? _pixelXCoordZero + 12 : _pixelXCoordZero - 50, GetCorrectYCoord(minPixY - 4));
            
            SearchResultValue.Text = string.Format("{0:0.0000000}", minimum);
            FindedXValue.Text = string.Format("{0:0.0000000}", xCoord);

            IterationCountValue.Text = _minExecutor.IterationCount.ToString();
        }
    }
}
