using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Main_work.HelpClasses
{
    public class MinimumSearchMethodExecutor
    {
        private readonly FunctionInformation _functionInfo;
        private readonly MinimumSearchForm _myForm;
        private MethodType _currentMathMethod;
        private Thread _thread;

        private double _xMin, _xMax;
        private double _stopSignal;
        private int _pieceCount;
        private int _threadPauseSize;
        private List<Interval> _intervals;

        public MinimumSearchMethodExecutor(FunctionInformation fInfo, MinimumSearchForm form)
        {
            _functionInfo = fInfo;
            _myForm = form;
            _currentMathMethod = MethodType.ScanMethod;
            _thread = new Thread(StartScanMethodScan);
            MaxValueY = 0;
            MinValueY = 0;

            _intervals = new List<Interval>();
        }

        public bool SetFunctionValue(string sinFirst, string sinSecond, string cosFirst, string cosSecond)
        {
            return _functionInfo.SetPrivateParameter(sinFirst, sinSecond, cosFirst, cosSecond);
        }

        public void ChangeSearchMethod(MethodType newMathMethod)
        {
            _currentMathMethod = newMathMethod;
        }

        public bool StartMinimumSearch(string xMin, string xMax, string pieceStartCount, string stopSignal, string stopTimer)
        {
            double xMi = 0, xMa = 0;
            double ss = 0;
            int psc = 0;
            bool flag = true;
            flag = double.TryParse(xMin, out xMi) &&
                double.TryParse(xMax, out xMa) &&
                int.TryParse(pieceStartCount, out psc) &&
                double.TryParse(stopSignal, out ss);

            if (!flag)
                return false;

            if (!int.TryParse(stopTimer, out _threadPauseSize))
                _threadPauseSize = 100;
            

            _xMin = xMi;
            _xMax = xMa;
            _stopSignal = ss;
            _pieceCount = psc;
            

            SetMinMax();
            
            // Подготовка для новой итерации
            if (_thread.IsAlive)
                _thread.Abort();
            // Очищение массива
            _intervals.Clear();
            //очищение поля для рисования
            _myForm.ClearDrawField();


            switch (_currentMathMethod)
            {
                case MethodType.ScanMethod:
                    // _thread = new Thread(StartScanMethodScan);
                    // _thread.Start();
                    StartScanMethodScan();
                    break;
                case MethodType.InfoStatisticAlgoritm:
                    // _thread = new Thread(StartInfoStatisticAlgoritm);
                    // _thread.Start();
                    StartInfoStatisticAlgoritm();
                    break;
                case MethodType.Polyline:
                    // _thread = new Thread(StartPolyline);
                    // _thread.Start();
                    StartPolyline();
                    break;

                default:
                    break;
            }

            return true;
        }

        public double MaxValueY { get; private set; }
        public double MinValueY { get; private set; }

        public double MinValueX { get => _xMin; }
        public double MaxValueX { get => _xMax; }

        /// <summary>
        /// отрисовать все точки заново
        /// </summary>
        public void DrawFullPointsAgain()
        {

        }

        private void SetMinMax()
        {
            var tmp = Math.Max(_functionInfo.GetValueByXCoord(_xMin), _functionInfo.GetValueByXCoord(_xMin));
            MaxValueY = (MaxValueY > tmp + 10.0) ? MaxValueY : tmp + 10.0;
            
            tmp = Math.Min(_functionInfo.GetValueByXCoord(_xMin), _functionInfo.GetValueByXCoord(_xMin));
            MinValueY = (MinValueY < tmp - 10.0) ? MinValueY : tmp - 10.0;
        }

        /// <summary>
        /// Метод сканирования
        /// </summary>
        private void StartScanMethodScan()
        {
            double pieSize = (_xMax - _xMin) / _pieceCount;
            double tmpValue = _xMin;

            // Заполнение начального массива
            for (int iter = 0; iter < _pieceCount; iter++)
            {
                Thread.Sleep(_threadPauseSize);
                var currentInterval = new Interval(_functionInfo.GetValueByXCoord(tmpValue), tmpValue, pieSize);
                DrawNewPoint(currentInterval.XCoordValue, currentInterval.StartValue);

                _intervals.Add(currentInterval);
                tmpValue += pieSize;
            }

            _intervals.Last().Size = _xMax - _intervals.Last().XCoordValue;
            // добавляю значение на крайней точке
            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(tmpValue), _xMax, 0.0));

            tmpValue = _intervals.Max(it => it.Size);

            while (tmpValue > _stopSignal)
            {
                Thread.Sleep(_threadPauseSize);

                // Возможно необходим рефакторинг

                var intervalForMath = _intervals.First(it => Equals(it.Size, tmpValue));
                var position = _intervals.FindIndex(new System.Predicate<Interval>(
                    it=>it.Size == intervalForMath.Size &&
                    it.StartValue == intervalForMath.StartValue &&
                    it.XCoordValue == intervalForMath.XCoordValue));

                var newCoord = (_intervals.ElementAt(position + 1).XCoordValue + intervalForMath.XCoordValue)/2;

                var newInterval = new Interval(
                    _functionInfo.GetValueByXCoord(newCoord), newCoord, _intervals.ElementAt(position + 1).XCoordValue - newCoord);

                _intervals.ElementAt(position).Size = newCoord - intervalForMath.XCoordValue;

                DrawNewPoint(newInterval.XCoordValue, newInterval.StartValue);

                _intervals.Insert(position + 1, newInterval);

                // Условие останова
                tmpValue = _intervals.Max(it => it.Size);
            }
            MaxValueY = _intervals.Max(it => it.StartValue);
            MinValueY = _intervals.Min(it => it.StartValue);

            SetFindedMinimum(_intervals.Min(it => it.StartValue));
        }

        /// <summary>
        /// Метод Стронгина
        /// </summary>
        private void StartInfoStatisticAlgoritm()
        {

        }

        /// <summary>
        /// Метод Ломаных(метод Пиявского)
        /// </summary>
        private void StartPolyline()
        {

        }

        private void DrawNewPoint(double x, double y)
        {
            _myForm.DrawPoint(x, y);
        }

        private void SetFindedMinimum(double minimum)
        {
            _myForm.SetFindedMinimum(minimum);
        }

    }
}
