using KeyecneLadderGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyecneLadderGenerator
{
    public partial class Form1 : Form
    {
        private List<KeyenceIOGeneratorModel> generatorDic = new List<KeyenceIOGeneratorModel>();
        public Form1()
        {
            InitializeComponent();            
            //主箱
            ProfileRegister(0, 4);
            //B01            
            ProfileRegister(1, 8);
            //B02
            ProfileRegister(2, 2);
            //B03
            ProfileRegister(3, 3);
            //B04
            ProfileRegister(4, 7);
            //B05
            ProfileRegister(5, 4);
            //B06
            ProfileRegister(6 ,8);
            //B07
            ProfileRegister(7, 0);
            //B08
            ProfileRegister(8, 2);
            //B09
            ProfileRegister(9, 3);
            //B10
            ProfileRegister(10, 4);
            //Generator Code            
            GeneratorStart();
        }
        /// <summary>
        /// 註冊點位 註冊者
        /// </summary>
        /// <param name="id">站號</param>
        /// <param name="count">數量</param>
        public void ProfileRegister(int id , int count)
        {
            for (int i = 0; i < count; i++)
            {
                generatorDic.Add(new KeyenceIOGeneratorModel()
                {
                    Address = "D" + ((30050 + i) + id * 100),
                    BitCount = 4,
                    OutBit = "R" + ((500 + i) + id * 1000),
                    Area = "B" + id
                });
            }
        }
        public Task GeneratorStart()
        {
            return Task.Run(() => {
                var result = IOGenerator(generatorDic);
                this.BeginInvoke(new Action(() => {
                    richTextBox1.AppendText(result.ToString());
                }));                
            });            
        }
        /// <summary>
        /// IO快速生成
        /// </summary>
        public string IOGenerator(List<KeyenceIOGeneratorModel> generatorDic)
        {
            string command = "";
            bool first = false;
            string areaText = "";
            generatorDic.ForEach(x =>
            {
                if (areaText != x.Area)
                {
                    areaText = x.Area;
                    command += "**********--" + areaText + "--**********\n";
                }
                
                for (int i = 0; i < x.BitCount; i++)
                {
                    first = i == 0 ? true : false;
                    string keyBit = string.Format("{0}.{1}", x.Address, i);
                    if (first)
                    {
                        command += string.Format("{0} {1}\n", "LD", keyBit);                        
                    }
                    else
                    {
                        command += string.Format("{0} {1}\n", "OR", keyBit);
                    }                                        
                }
                command += string.Format("{0} {1}\n", "Out", x.OutBit);                
            });
            
            return command;
        }
    }
}
