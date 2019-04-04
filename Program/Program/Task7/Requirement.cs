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
        /// <summary>
        /// Момент поступления требования в очередь системы из источника
        /// </summary>
        public double EntryPointTime { get; set; }

        /// <summary>
        /// Момент начала обслуживания требования
        /// </summary>
        public double StartServiceTime { get; set; }

        /// <summary>
        /// Момент завершения обслуживания требования
        /// </summary>
        public double ExitPointTime { get; set; }

        /// <summary>
        /// Название требования
        /// </summary>
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
}
