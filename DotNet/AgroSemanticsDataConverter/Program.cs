using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace AgroSemanticsDataConverter
{
    public class FlareRegister
    {
        private double temp;

        public double Tem
        {
            get { return temp; }

            set { temp = (double) Math.Round(value, 2); }
        }

        public double Hum { get; set; }
    }

    public class OpenWeatherRegister
    {
        public double Dew_Point { get; set; }
        public int Pressure { get; set; }
        public int Wind_deg { get; set; }
        public double Temp { get; set; }
        public int Clouds { get; set; }
        public double Feels_like { get; set; }
        public double Uvi { get; set; }
        
        public int Humidity { get; set; }
        public bool isForecast { get; set; }
        public double Wind_Speed { get; set; }
        public double Pop { get; set; }
        public string Local_Dt { get; set; }
    }


    public class DayRegister
    {
        public string Date { get; set; }

        public IList<FlareRegister> Fs { get; set; }

        public IList<OpenWeatherRegister> Owm { get; set; }
    }


    internal class Program
    {
        public static void Main(string[] args)
        {
            var json = File.ReadAllText(@"/Users/arturo/dev/Stream/agrosemantics2.json");
            var tem = Math.Round(16.222, 2);
            IList<DayRegister> registers = JsonConvert.DeserializeObject<IList<DayRegister>>(json);

            var csv = new StringBuilder();
           
            foreach (var register in registers)
            {
             if (  register.Fs != null && register.Owm != null )
                for (int i = 0; i <= 23; i++)
                {
                    if (register.Owm[i] != null && register.Fs[i] != null)
                    {

                        if (register.Owm[i].isForecast == false)
                        {
                            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6}",
                               
                                register.Owm[i].Temp.ToString(),
                                register.Owm[i].Humidity.ToString(),
                                //register.Owm[i].Feels_like.ToString(),
                                register.Owm[i].Clouds.ToString(),
                                
                                register.Owm[i].Pressure.ToString(),
                                register.Owm[i].Wind_Speed.ToString(),
                                register.Owm[i].Uvi.ToString(),

                                
                                register.Fs[i].Tem);
                       

                            csv.AppendLine(newLine);
                        
                        }
                    }
                }
                
            }
            
            File.WriteAllText("/Users/arturo/dev/Stream/regression0428.csv", csv.ToString());
            
        }
    }
}