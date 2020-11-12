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

        public bool StartMinimumSearch(string xMin, string xMax, string stopSignal, string stopTimer)
        {
            double xMi = 0, xMa = 0;
            double ss = 0;
            int psc = 0;
            bool flag = true;
            flag = double.TryParse(xMin, out xMi) &&
                double.TryParse(xMax, out xMa) &&
                double.TryParse(stopSignal, out ss);

            if (!flag)
                return false;

            if (!int.TryParse(stopTimer, out _threadPauseSize))
                _threadPauseSize = 100;
            

            _xMin = xMi;
            _xMax = xMa;
            _stopSignal = ss;
            

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
            int position = 0;

            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMin), _xMin, _xMax - _xMin));
            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMax), _xMax, 0.0));

            DrawNewPoint(_intervals.First().XCoordValue, _intervals.First().StartValue);
            DrawNewPoint(_intervals.Last().XCoordValue, _intervals.Last().StartValue);

            var currentInterval = _intervals.First();

            while (currentInterval.Size > _stopSignal)
            {
                Thread.Sleep(_threadPauseSize);

                // Преобразование интервала. Вычисление новой точки испытания
                CalculateAndAddNewPoint(currentInterval, position);   

                // Поиск следующего интервала для преобразования и проверка условия останова
                currentInterval = GetIntervalWithMaxCharacteristic(out position);
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
        
        private Interval GetIntervalWithMaxCharacteristic(out int position)
        {
            Interval result = _intervals.First();
            position = 0;
            switch (_currentMathMethod)
            {
                case MethodType.ScanMethod:

                    var maxParameterValue = _intervals.Max(it => it.Size);
                    result = _intervals.First(it => Equals(it.Size, maxParameterValue));

                    position = _intervals.FindIndex(new System.Predicate<Interval>(
                    it => it.Size == result.Size &&
                    it.StartValue == result.StartValue &&
                    it.XCoordValue == result.XCoordValue));

                    break;

                case MethodType.InfoStatisticAlgoritm:
                    
                    break;

                case MethodType.Polyline:
                    
                    break;

                default:
                    break;
            }

            return result;
        }

        private void CalculateAndAddNewPoint(Interval interval, int position)
        {
            Interval result = _intervals.First();
            var newCoord = 0.0;
            Interval newInterval = null;
            switch (_currentMathMethod)
            {
                case MethodType.ScanMethod:
                    {
                        // Находим координату нового испытания
                        newCoord = interval.XCoordValue + interval.Size / 2;
                        newInterval = new Interval(
                            _functionInfo.GetValueByXCoord(newCoord), newCoord, _intervals.ElementAt(position + 1).XCoordValue - newCoord);

                        // Меняем характеристику предыдущего интервала
                        _intervals.ElementAt(position).Size = newCoord - interval.XCoordValue;
                        break;
                    }

                case MethodType.InfoStatisticAlgoritm:
                    {

                        break;
                    }

                case MethodType.Polyline:
                    {

                        break;
                    }

                default:
                    break;
            }

            // Рисуем новую точку
            DrawNewPoint(newInterval.XCoordValue, newInterval.StartValue);
            // Добавляем очередное испытание
            _intervals.Insert(position + 1, newInterval);
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
