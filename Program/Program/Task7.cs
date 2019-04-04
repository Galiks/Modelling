using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /// <summary>
    /// Класс требование
    /// </summary>
    class Requirement
    {
        public string Name { get; set; }

        public Requirement(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Name: {Name}{Environment.NewLine}";
        }
    }


    /// <summary>
    /// M|M|1
    /// Первая M - задержка между поступлениями
    /// Вторая M - время обслуживания
    /// 1 - количество приборов
    /// </summary>
    class Task7
    {
        public double EntryPointTime { get; set; }
        public double ExitPointTime { get; set; }
        public double CurrentModelTime { get; set; }
        public double ActivationTimeSource { get; set; }
        public double CompletionOfServiceRequirementsTime { get; set; }
        public bool IsBusy { get; set; }
        public Queue<Requirement> ImitationQueue { get; set; }
        public Queue<Requirement> ImitationQueueForSeevedRequirements { get; set; }

        /// <summary>
        /// Событие 1: поступление требования из источника
        /// </summary>
        private void SourceClaim()
        {

        }

        /// <summary>
        /// Событие 2: начало обслуживания требования
        /// </summary>
        private void ServiceStartRequirements()
        {

        }

        /// <summary>
        /// Событие 3: завершение обслуживания требования
        /// </summary>
        private void CompletionOfServiceRequirements()
        {

        }
    }
}
