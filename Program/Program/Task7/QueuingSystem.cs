using System;
using System.Collections.Generic;
using System.Linq;

namespace Program
{
    /// <summary>
    /// M|M|1
    /// Первая M - задержка между поступлениями = экспоненциальная случайная величина
    /// Вторая M - время обслуживания = экспоненциальная случайная величина
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
        /// Момент активизации источника / поступления требования
        /// </summary>
        public double ActivationTimeSource { get; set; }
        
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
        private const double lambda = 85.35;
        private readonly double T = 1000;
        public Requirement GetRequirement { get; set; }
        public Requirement GetSecondRequirement { get; set; }

        /// <summary>
        /// Конструктор для СМО
        /// </summary>
        /// <param name="T">Конечное время</param>
        public QueuingSystem(double T)
        {
            this.T = T;
            random = new Random();
        }

        /// <summary>
        /// Метод для запуска главной программы
        /// </summary>
        public void Main()
        {
            LeadProgram();
            Console.WriteLine(CurrentModelTime);
        }

        /// <summary>
        /// Сегмент процесса, связанный с поступлением требования в СМО.
        /// </summary>
        private void SourceClaim(Requirement requirement)
        {
            //Console.WriteLine("Событие 1: поступление требования из источника");
            if (CurrentModelTime == ActivationTimeSource)
            {

                Console.WriteLine($"В очередь поступило {requirement.Name}");
                requirement.EntryPointTime = CurrentModelTime;
                ImitationQueue.Enqueue(requirement);
                ActivationTimeSource = CurrentModelTime + GetExponentionValue();

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
                GetRequirement = ImitationQueue.Dequeue();
                GetRequirement.StartServiceTime = CurrentModelTime;
                //с какой-то вероятностью появляется второе требование
                //if (random.Next(1, 2) == 1)
                //{
                //    GetSecondRequirement = Requirements.Dequeue();
                //}
                CompletionOfServiceRequirementsTime = CurrentModelTime + GetExponentionValue();
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
                ActivationTimeSource = CurrentModelTime;
            }
        }

        /// <summary>
        /// Ведущая программа
        /// </summary>
        private void LeadProgram()
        {
            Requirements = new List<Requirement>();
            for (int i = 0; i < 5; i++)
            {
                Requirements.Add(new Requirement($"Требование №{i}"));
            }

            CurrentModelTime = 0;
            IsBusy = false;
            ActivationTimeSource = 0;
            StartOfServiceRequirementTime = double.NaN;
            CompletionOfServiceRequirementsTime = double.NaN;
            ImitationQueue = new Queue<Requirement>();
            ImitationQueueForServedRequirements = new Queue<Requirement>();
            while (CurrentModelTime < T)
            {
                if (CurrentModelTime == ActivationTimeSource)
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

                CurrentModelTime = CompletionOfServiceRequirementsTime != GetPseudoInfinity() ? CompletionOfServiceRequirementsTime : ActivationTimeSource;

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
        private double GetExponentionValue()
        {
            return -(1/lambda * Math.Log(random.NextDouble(), Math.E));
        }
    }
}
