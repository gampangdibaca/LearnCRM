using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class KafkaEmailStatus
    {
        public string id { get; set; } = string.Empty;
        public string source { get; set; } = string.Empty;
        public string flagStatus { get; set; } = string.Empty;
        public string remark { get; set; } = string.Empty;
        public string emailSender { get; set; } = string.Empty;
        public string entryDate { get; set; } = string.Empty;
        public string modifiedDate { get; set; } = string.Empty;
        public string to { get; set; } = string.Empty;
        public string cc { get; set; } = string.Empty;
        public string bcc { get; set; }
    }
}
