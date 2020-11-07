using Main_work.HelpClasses;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Main_work
{
    public partial class MinimumSearchForm : Form
    {
        private MinimumSearchMethodExecutor _minExecutor;

        private double _pixelXValue;
        private double _pixelYValue;

        private int _pixelXCoordZero;
        private int _pixelYCoordZero;

        public MinimumSearchForm()
        {
            var function = new FunctionInformation();
            _minExecutor = new MinimumSearchMethodExecutor(function, this);
            InitializeComponent();
            RB_Scan.Select();

            FSin_value.Text = "1";
            SSin_value.Text = "1";
            FCos_value.Text = "1";
            SCos_value.Text = "1";
        }

        private void RB_Piyavsky_CheckedChanged(object sender, EventArgs e)
        {
            _minExecutor.ChangeSearchMethod(MethodType.Polyline);
        }

        private void RB_Strongin_CheckedChanged(object sender, EventArgs e)
        {
            _minExecutor.ChangeSearchMethod(MethodType.InfoStatisticAlgoritm);
        }

        private void RB_Scan_CheckedChanged(object sender, EventArgs e)
        {
            _minExecutor.ChangeSearchMethod(MethodType.ScanMethod);
        }

        private void ApplyNewParameter_Button_Click(object sender, EventArgs e)
        {
            var resBool = _minExecutor.SetFunctionValue(FSin_value.Text, SSin_value.Text, FCos_value.Text, SCos_value.Text);
            /*
            if (resBool)
            {
                Save_is_not_correct_label.Visible = false;
                Save_is_correct_label.Visible = true;
                Thread.Sleep(800);
                Save_is_correct_label.Visible = false;
            }
            else
            {                
                Save_is_correct_label.Visible = false;
                Save_is_not_correct_label.Visible = true;
                Thread.Sleep(800);
                Save_is_not_correct_label.Visible = false;
            }
            */
        }
        
        private int GetCorrectYCoord(int currCoord)
        {
            return DrawField.Size.Height - currCoord;
        }

        private void StartSerchButton_Click(object sender, EventArgs e)
        {
            var isStarted = _minExecutor.StartMinimumSearch(MinValueX.Text, MaxValueX.Text, PieceCountValue.Text, MaxSectionDistance.Text, TimeToPauseValue.Text);
        }

        public void ClearDrawField()
        {
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

            // Ось Y
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(0)),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)),
                new Point(_pixelXCoordZero - 2, GetCorrectYCoord(DrawField.Size.Height - 5)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(_pixelXCoordZero, GetCorrectYCoord(DrawField.Size.Height - 1)),
                new Point(_pixelXCoordZero + 2, GetCorrectYCoord(DrawField.Size.Height - 5)));

            // Ось X
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(0, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 5, GetCorrectYCoord(_pixelYCoordZero - 2)));
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(DrawField.Size.Width - 1, GetCorrectYCoord(_pixelYCoordZero)),
                new Point(DrawField.Size.Width - 5, GetCorrectYCoord(_pixelYCoordZero + 2)));

        }

        public void DrawPoint(double XCoord, double YCoord)
        {
            int xPixel = (int)(XCoord / _pixelXValue) + _pixelXCoordZero;
            int yPixel = (int)(YCoord / _pixelYValue) + _pixelYCoordZero;

            Graphics g = DrawField.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(xPixel+1, GetCorrectYCoord(yPixel-1)),
                new Point(xPixel-1, GetCorrectYCoord(yPixel+1)));

            g.DrawLine(new Pen(Brushes.Red, 1),
                new Point(xPixel - 1, GetCorrectYCoord(yPixel - 1)),
                new Point(xPixel + 1, GetCorrectYCoord(yPixel + 1)));

            /*
            g.DrawString("The sizes are equal.",
            new Font(this.Font, FontStyle.Italic),
            Brushes.Indigo, 10.0F, 65.0F);
            */
        } 

        public void SetFindedMinimum(double minimum)
        {

            SearchResultValue.Text = string.Format("{0:0.0000000}", minimum);
        }


    }
}
