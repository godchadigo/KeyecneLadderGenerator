using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyecneLadderGenerator.Models
{
    public class KeyenceIOGeneratorModel
    {
        /// <summary>
        /// 並連接點
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 並聯次數
        /// </summary>
        public int BitCount { get; set; }
        /// <summary>
        /// 指向輸出接點
        /// </summary>
        public string OutBit { get; set; }
        public string Area { get; set; }
    }
}
