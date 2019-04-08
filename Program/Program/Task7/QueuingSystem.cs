using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    /// <summary>
    /// M|M|1
    /// Первая M - задержка между поступлениями = экспоненциальная случайная величина - мю
    /// Вторая M - время обслуживания = экспоненциальная случайная величина - лямбда
    /// 1 - количество приборов
    /// PS (разделение процессора)
    /// </summary>
    class QueuingSystem
    {
        /// <summary>
        /// Флаг, сигнализирующий о выходе
        /// </summary>
        public bool FlagOfExit { get; set; }

        /// <summary>
        /// Текущий момент модельного времени
        /// </summary>
        public double CurrentModelTime { get; set; }

        /// <summary>
        /// Момент поступления требования в очередь
        /// </summary>
        public double ActivationOfServiceRequirementTime { get; set; }

        /// <summary>
        ///Момент начала обслуживания
        /// </summary>
        public double StartOfServiceRequirementTime { get; set; }

        /// <summary>
        /// Момент завершения обслуживания требования
        /// </summary>
        public double CompletionOfServiceRequirementsTime { get; set; }

        /// <summary>
        /// Занятость прибора
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// Имитация очереди
        /// </summary>
        public Queue<Requirement> ImitationQueue { get; set; }

        /// <summary>
        /// Имитация очереди для требований, которые были обслужены
        /// </summary>
        public Queue<Requirement> ImitationQueueForServedRequirements { get; set; }

        /// <summary>
        /// Имитация источника требований
        /// </summary>
        public List<Requirement> Requirements { get; set; }
        private readonly Random random;

        /// <summary>
        /// Значение лямбды для вычисление экспоненциальной случайной величины
        /// </summary>
        private const double lambda = 3;

        private const double mu = 4;

        /// <summary>
        /// Длительность модельного времени проведения эксперимента с моделью,
        /// </summary>
        private readonly double T;

        /// <summary>
        /// Требование
        /// </summary>
        public Requirement GetRequirement { get; set; }

        /// <summary>
        /// Второе требование
        /// </summary>
        public Requirement GetSecondRequirement { get; set; }

        /// <summary>
        /// Список, в котором содержатся разницы межуд моментами завершения требования и поступления требования в очередь
        /// </summary>
        public List<double> ListOFDifferenceBetweenCompletionAndActivationOfServiceRequirementTime { get; set; }

        /// <summary>
        /// Список, в котором содержатся разницы межуд моментами завершения требования и начала обслуживания
        /// </summary>
        public List<double> ListForOneRequirement { get; set; }

        public List<double> ListForZeroRequirement { get; set; }

        public int CountOfRequirements { get; set; }

        /// <summary>
        /// Конструктор для СМО
        /// </summary>
        /// <param name="T">Конечное время</param>
        /// <param name="countOfRequirements">Количество требований</param>
        public QueuingSystem(double T, int countOfRequirements)
        {
            this.CountOfRequirements = countOfRequirements;
            this.T = T;
            random = new Random();
            ListOFDifferenceBetweenCompletionAndActivationOfServiceRequirementTime = new List<double>();
            ListForOneRequirement = new List<double>();
            ListForZeroRequirement = new List<double>();
        }

        /// <summary>
        /// Метод для запуска главной программы
        /// </summary>
        public void Main()
        {
            LeadProgram();
            Console.WriteLine("Current time: " + CurrentModelTime);
            var temp = GetExpectedValue();
            Console.WriteLine("u : " + temp);
            Console.WriteLine("True n : " + temp*lambda);
            Console.WriteLine("n : " + GetExpectedValueOfKRequirement());
        }

        /// <summary>
        /// Сегмент процесса, связанный с поступлением требования в СМО.
        /// </summary>
        private void SourceClaim(Requirement requirement)
        {
            //Console.WriteLine("Событие 1: поступление требования из источника");
            if (CurrentModelTime == ActivationOfServiceRequirementTime)
            {
                Console.WriteLine($"В очередь поступило {requirement.Name}");
                requirement.EntryPointTime = CurrentModelTime;
                ImitationQueue.Enqueue(requirement);
                ActivationOfServiceRequirementTime = CurrentModelTime + GetExponentionValue(mu);
            }
        } 

        /// <summary>
        /// Сегмент процесса, сязанный с началом обслуживания требования в СМО.
        /// </summary>
        private void ServiceStartRequirements()
        {
            //Console.WriteLine("Событие 2: начало обслуживания требования");
            if (ImitationQueue.Count > 0 && !IsBusy)
            {
                Console.WriteLine($"Требование обслуживается");
                IsBusy = true;
                StartOfServiceRequirementTime = CurrentModelTime;
                GetRequirement = ImitationQueue.Dequeue();
                GetRequirement.StartServiceTime = CurrentModelTime;
                CompletionOfServiceRequirementsTime = CurrentModelTime + GetExponentionValue(lambda);
                ListOFDifferenceBetweenCompletionAndActivationOfServiceRequirementTime.Add(Math.Abs(CompletionOfServiceRequirementsTime - ActivationOfServiceRequirementTime));
            }
        }

        /// <summary>
        /// Событие 3: завершение обслуживания требования
        /// </summary>
        private void CompletionOfServiceRequirements()
        {
            //Console.WriteLine("Событие 3: завершение обслуживания требования");
            if (CompletionOfServiceRequirementsTime == CurrentModelTime)
            {
                Console.WriteLine($"Требование завершилось");
                Console.WriteLine();
                GetRequirement.ExitPointTime = CurrentModelTime;
                ImitationQueueForServedRequirements.Enqueue(GetRequirement);
                IsBusy = false;
                ImitationQueueForServedRequirements.Enqueue(GetRequirement);
                ActivationOfServiceRequirementTime = CurrentModelTime;
            }
        }

        /// <summary>
        /// Ведущая программа
        /// </summary>
        private void LeadProgram()
        {
            Requirements = new List<Requirement>();
            for (int i = 0; i < CountOfRequirements; i++)
            {
                Requirements.Add(new Requirement($"Требование №{i}"));
            }

            CurrentModelTime = 0;
            IsBusy = false;
            ActivationOfServiceRequirementTime = 0;
            StartOfServiceRequirementTime = GetPseudoInfinity();
            CompletionOfServiceRequirementsTime = GetPseudoInfinity();
            ImitationQueue = new Queue<Requirement>();
            ImitationQueueForServedRequirements = new Queue<Requirement>();
            while (CurrentModelTime < T)
            {
                if (CurrentModelTime == ActivationOfServiceRequirementTime)
                {
                    if (Requirements.Count() != 0)
                    {
                        GetRequirement = GetRandomRequirement();
                        Requirements.Remove(GetRequirement);
                        SourceClaim(GetRequirement);
                    }
                    else
                    {
                        break;
                    }
                }
                if (ImitationQueue.Count() > 0 && !IsBusy)
                {
                    ServiceStartRequirements();
                }
                if (CompletionOfServiceRequirementsTime == CurrentModelTime)
                {
                    CompletionOfServiceRequirements();
                }

                CurrentModelTime = CompletionOfServiceRequirementsTime != GetPseudoInfinity() ? CompletionOfServiceRequirementsTime : ActivationOfServiceRequirementTime;
                ListForOneRequirement.Add(Math.Abs(CompletionOfServiceRequirementsTime - StartOfServiceRequirementTime));
                ListForZeroRequirement.Add(Math.Abs(CompletionOfServiceRequirementsTime - ActivationOfServiceRequirementTime));
            }
        }

        /// <summary>
        /// Метод для случайного выбора требования
        /// </summary>
        /// <returns>Требование</returns>
        private Requirement GetRandomRequirement()
        {
            int indexOfRequirement = random.Next(0, Requirements.Count());
            return Requirements[indexOfRequirement];
        }

        /// <summary>
        /// Метод для генерирования псевдобесконечности
        /// </summary>
        /// <returns>Псевдобесконечность</returns>
        private double GetPseudoInfinity()
        {
            return T + Math.Pow(10, 6);
        }

        /// <summary>
        /// Метод для генерирования экспоненциальной случайной величины
        /// </summary>
        /// <returns>Экспоненциальная случайная величина</returns>
        private double GetExponentionValue(double x)
        {
            return -(1 / x * Math.Log(random.NextDouble(), Math.E));
        }

        /// <summary>
        /// Метод для получения оценки математического ожидания длительности пребывания требований в системе обслуживания.
        /// </summary>
        /// <returns>Оценка математического ожидания длительности пребывания требований в системе обслуживания</returns>
        private double GetExpectedValue()
        {
            double sum = 0;
            foreach (var item in ListOFDifferenceBetweenCompletionAndActivationOfServiceRequirementTime)
            {
                sum += item;
            }
            double temp = sum / ListOFDifferenceBetweenCompletionAndActivationOfServiceRequirementTime.Count();
            return temp;
        }

        /// <summary>
        /// Метод для получения математического ожидания числа требований в системе обслуживания.
        /// </summary>
        /// <returns></returns>
        private double GetExpectedValueOfKRequirement()
        {
            return ((1 * GetPForOneRequirement()) + (2*GetPForZeroRequirement()));
        }

        private double GetPForZeroRequirement()
        {
            double sum = 0;
            foreach (var item in ListForZeroRequirement)
            {
                sum += item;
            }
            //for (int i = 0; i < ListForZeroRequirement.Count(); i++)
            //{
            //    sum += GetExponentionValue(mu);
            //}
            return sum / (ListForOneRequirement.Count() + ListForZeroRequirement.Count());
        }

        private double GetPForOneRequirement()
        {
            double sum = 0;
            foreach (var item in ListForOneRequirement)
            {
                sum += item;
            }
            //for (int i = 0; i < ListForOneRequirement.Count(); i++)
            //{
            //    sum += GetExponentionValue(lambda);
            //}
            return sum / (ListForOneRequirement.Count() + ListForZeroRequirement.Count());
        }
    }
}
