﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Main_work.HelpClasses;
using Main_work.Function;

namespace Main_work.MinimumSearchMethodExecutors
{
    public class MinimumSearchMethodExecutor
    {
        private readonly FunctionInformation _functionInfo;
        private readonly MinimumSearchForm _myForm;
        private MethodType _currentMathMethod;
        private Thread _thread;

        private double _xMin, _xMax;
        private double _yMin, _yMax;
        private double _stopSignal;
        private double _rParameter;
        private int _threadPauseSize;
        private List<Interval> _intervals;

        private double _currentMParameter;

        public MinimumSearchMethodExecutor(FunctionInformation fInfo, MinimumSearchForm form)
        {
            _functionInfo = fInfo;
            _myForm = form;
            _currentMathMethod = MethodType.ScanMethod;
            _thread = new Thread(StartScanMethodScan);
            MaxValueY = 1;
            MinValueY = -1;

            _intervals = new List<Interval>();
        }

        public void DrawFunction(double xMin, double xMax, bool isCorrectMinMax, System.Drawing.Brush color, double interval = 0.002)
        {
            while (xMin <= xMax)
            {
                var yValue = _functionInfo.GetValueByXCoord(xMin);
                if (isCorrectMinMax)
                    CorrectMaxAndMin(yValue);
                DrawSinglePoint(xMin, yValue, color);
                xMin += interval;
            }
        }

        public bool SetFunctionValue(string sinFirst, string sinSecond, string cosFirst, string cosSecond)
        {
            return _functionInfo.SetPrivateParameter(sinFirst, sinSecond, cosFirst, cosSecond);
        }

        public void ChangeSearchMethod(MethodType newMathMethod)
        {
            _currentMathMethod = newMathMethod;
        }

        public bool StartMinimumSearch(string xMin, string xMax, string stopSignal, string stopTimer, string rParameter)
        {
            IterationCount = 0;
            double xMi = 0, xMa = 0;
            double ss = 0;
            double rPar = 0;
            bool flag = true;
            flag = double.TryParse(xMin, out xMi) &&
                double.TryParse(xMax, out xMa) &&
                double.TryParse(stopSignal, out ss) &&
                double.TryParse(rParameter, out rPar);

            if (!flag)
                return false;

            if (!int.TryParse(stopTimer, out _threadPauseSize))
                _threadPauseSize = 100;

            _xMin = xMi;
            _xMax = xMa;
            _stopSignal = ss;
            _rParameter = rPar;
            
            // Подготовка для новой итерации
            if (_thread.IsAlive)
                _thread.Abort();
            // Очищение массива
            _intervals.Clear();
            //очищение поля для рисования
            _myForm.ClearDrawField(true);


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
            
            DrawFullPointsAgain();
            SetFindedMinimum();

            return true;
        }

        public double MaxValueY { get; private set; }
        public double MinValueY { get; private set; }

        public double MinValueX { get => _xMin; }
        public double MaxValueX { get => _xMax; }

        /// <summary>
        /// Счётчик количества циклов при поиске минимума
        /// </summary>
        public int IterationCount { get; private set; }

        /// <summary>
        /// отрисовать все точки заново
        /// </summary>
        public void DrawFullPointsAgain()
        {            
            _myForm.ClearDrawField();
            foreach (var inter in _intervals)
                DrawNewPoint(inter.XCoordValue, inter.StartValue);
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
            
            _intervals[0].Characteristic = _intervals[0].Size;

            var currentInterval = _intervals.First();

            while (currentInterval.Size > _stopSignal)
            {
                IterationCount++;
                Thread.Sleep(_threadPauseSize);

                // Преобразование интервала. Вычисление новой точки испытания
                CalculateAndAddNewPoint(currentInterval, position);   

                // Поиск следующего интервала для преобразования и проверка условия останова
                currentInterval = GetIntervalWithMaxCharacteristic(out position);
            }

        }

        /// <summary>
        /// Метод Стронгина
        /// </summary>
        private void StartInfoStatisticAlgoritm()
        {
            int position = 0;

            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMin), _xMin, _xMax - _xMin));
            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMax), _xMax, 0.0));

            _intervals[0].CalculatePotential(_intervals[1].StartValue, _intervals[1].XCoordValue);

            DrawNewPoint(_intervals.First().XCoordValue, _intervals.First().StartValue);
            DrawNewPoint(_intervals.Last().XCoordValue, _intervals.Last().StartValue);
            
            // Установка M-параметра
            SetMParameter();

            var currentInterval = GetIntervalWithMaxCharacteristic(out position);

            while (currentInterval.Size > _stopSignal)
            {
                IterationCount++;
                Thread.Sleep(_threadPauseSize);
                
                // Преобразование интервала. Вычисление новой точки испытания
                CalculateAndAddNewPoint(currentInterval, position);
                
                // Установка M-параметра
                SetMParameter();

                // Поиск следующего интервала для преобразования и проверка условия останова
                currentInterval = GetIntervalWithMaxCharacteristic(out position);
            }

        }

        /// <summary>
        /// Метод Ломаных(метод Пиявского)
        /// </summary>
        private void StartPolyline()
        {
            int position = 0;

            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMin), _xMin, _xMax - _xMin));
            _intervals.Add(new Interval(_functionInfo.GetValueByXCoord(_xMax), _xMax, 0.0));

            _intervals[0].CalculatePotential(_intervals[1].StartValue, _intervals[1].XCoordValue);
                

            DrawNewPoint(_intervals.First().XCoordValue, _intervals.First().StartValue);
            DrawNewPoint(_intervals.Last().XCoordValue, _intervals.Last().StartValue);
            
            // Установка M-параметра
            SetMParameter();

            var currentInterval = GetIntervalWithMaxCharacteristic(out position);
            
            while (currentInterval.Size > _stopSignal)
            {
                IterationCount++;
                Thread.Sleep(_threadPauseSize);

                // Преобразование интервала. Вычисление новой точки испытания
                CalculateAndAddNewPoint(currentInterval, position);

                // Установка M-параметра
                SetMParameter();

                // Поиск следующего интервала для преобразования и проверка условия останова
                currentInterval = GetIntervalWithMaxCharacteristic(out position);
            }

        }

        private Interval GetIntervalWithMaxCharacteristic(out int position)
        {
            Interval result = _intervals.First();
            position = 0;
            switch (_currentMathMethod)
            {
                case MethodType.ScanMethod:                    
                    break;

                case MethodType.InfoStatisticAlgoritm:
                    {
                        var tmpCount = _intervals.Count - 1;
                        for (int i = 0; i < tmpCount; i++)
                        {
                            var nextInterval = _intervals[i + 1];
                            var currentInterval = _intervals[i];
                            var tmpValue = _currentMParameter * (currentInterval.Size);
                            _intervals[i].Characteristic = tmpValue +
                                Math.Pow((nextInterval.StartValue - currentInterval.StartValue), 2) / tmpValue -
                                2 * (nextInterval.StartValue + currentInterval.StartValue);
                        }
                        break;
                    }
                case MethodType.Polyline:
                    {
                        var tmpCount = _intervals.Count - 1;
                        for (int i = 0; i < tmpCount; i++)
                        {
                            var nextInterval = _intervals[i + 1];
                            var currentInterval = _intervals[i];
                            _intervals[i].Characteristic = 0.5 * _currentMParameter * (nextInterval.XCoordValue - currentInterval.XCoordValue) -
                                (nextInterval.StartValue + currentInterval.StartValue) / 2;
                        }
                        break;
                    }
                default:
                    break;
            }

            var maxCharacteristicsValue = _intervals.Take(_intervals.Count-1).Max(it => it.Characteristic);
            result = _intervals.First(it => Equals(it.Characteristic, maxCharacteristicsValue));

            position = _intervals.FindIndex(new System.Predicate<Interval>(
            it => it.Size == result.Size &&
            it.StartValue == result.StartValue &&
            it.XCoordValue == result.XCoordValue));

            return result;
        }

        #region Вспомогательные функции:

        private double GetPotensial(double finishValue, double startValue, double finishCoord, double startCooord)
        {
            return Math.Abs((finishValue - startValue)) / (finishCoord - startCooord);
        }

        private void SetMParameter()
        {
            var MaxPotential = (_currentMathMethod == MethodType.InfoStatisticAlgoritm || _currentMathMethod == MethodType.Polyline) ?
                _intervals.Max(it => it.Potential) : 0.0;
            _currentMParameter = (_currentMathMethod == MethodType.ScanMethod) ? 0.0 :
                (MaxPotential > 0 && _rParameter > 1) ? _rParameter * MaxPotential : 1.0;
        }

        #endregion


        #region Взаимодействие и графикой:

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
                        var tmpSize = _intervals.ElementAt(position + 1).XCoordValue - newCoord;
                        newInterval = new Interval(
                            _functionInfo.GetValueByXCoord(newCoord), newCoord, tmpSize);

                        // CorrectMaxAndMin(newInterval.StartValue);

                        // Меняем характеристику предыдущего интервала
                        _intervals.ElementAt(position).Characteristic = newCoord - interval.XCoordValue;
                        _intervals.ElementAt(position).Size = newCoord - interval.XCoordValue;
                        break;
                    }

                case MethodType.InfoStatisticAlgoritm:
                case MethodType.Polyline:
                    {
                        newCoord = 0.5 * (_intervals[position + 1].XCoordValue + interval.XCoordValue) - (_intervals[position + 1].StartValue - interval.StartValue) / ( 2 * _currentMParameter);

                        var tmpSize = _intervals.ElementAt(position + 1).XCoordValue - newCoord;
                        newInterval = new Interval(
                            _functionInfo.GetValueByXCoord(newCoord), newCoord, tmpSize);
                        
                        // CorrectMaxAndMin(newInterval.StartValue);

                        _intervals.ElementAt(position).Size = newCoord - interval.XCoordValue;
                        break;
                    }
                default:
                    break;
            }

            // Рисуем новую точку
            DrawNewPoint(newInterval.XCoordValue, newInterval.StartValue);
            // Добавляем очередное испытание
            _intervals.Insert(position + 1, newInterval);

            // Меняем потенциалы обоих интервалов
            _intervals[position].CalculatePotential(newInterval.StartValue, newInterval.XCoordValue);
            _intervals[position+1].CalculatePotential(_intervals[position + 2].StartValue, _intervals[position + 2].XCoordValue);
        }

        private void CorrectMaxAndMin(double value)
        {
            if (value >= MaxValueY)
                MaxValueY = value + 0.5;

            if (value <= MinValueY)
                MinValueY = value - 0.5;
        }

        private void DrawSinglePoint(double x, double y, System.Drawing.Brush color)
        {
            _myForm.DrawSinglePoint(x, y, color);
        }

        private void DrawNewPoint(double x, double y)
        {
            _myForm.DrawPoint(x, y);
        }

        private void SetFindedMinimum()
        {
            double minimum = _intervals.Min(it => it.StartValue);
            var minimumValue = _intervals.First(it=>it.StartValue == minimum);
            _myForm.SetFindedMinimum(minimumValue.StartValue, minimumValue.XCoordValue);
        }

        #endregion
    }
}
