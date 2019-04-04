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
        public Queue<Requirement> Requirements { get; set; }
        private readonly Random random;

        /// <summary>
        /// Значение лямбды для вычисление экспоненциальной случайной величины
        /// </summary>
        private const double lambda = 85.35;
        private readonly double T = 1000;
        public Requirement GetRequirement { get; set; }

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
        }

        /// <summary>
        /// Сегмент процесса, связанный с поступлением требования в СМО.
        /// </summary>
        private void SourceClaim()
        {
            //Console.WriteLine("Событие 1: поступление требования из источника");
            if (CurrentModelTime == ActivationTimeSource)
            {
                if (Requirements.Count() != 0)
                {
                    GetRequirement = Requirements.Dequeue();
                    Console.WriteLine($"В очередь поступило требование");
                    GetRequirement.EntryPointTime = CurrentModelTime;
                    ImitationQueue.Enqueue(GetRequirement);
                    ActivationTimeSource = CurrentModelTime + GetExponentionValue(); 
                }
                else
                {
                    Console.WriteLine($"Требования кончились!");
                    FlagOfExit = true;
                }
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
            Requirements = new Queue<Requirement>();
            for (int i = 0; i < 1000; i++)
            {
                Requirements.Enqueue(new Requirement($"Требование №{i + 1}"));
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
                    SourceClaim();
                }
                if (FlagOfExit)
                {
                    break;
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

        private double GetPseudoInfinity()
        {
            return T + Math.Pow(10, 6);
        }

        private double GetExponentionValue()
        {
            return -(1/lambda * Math.Log(random.NextDouble(), Math.E));
        }
    }
}
