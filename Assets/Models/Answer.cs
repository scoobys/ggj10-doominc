using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    public class Answer
    {
        public string Name { get; set; }
        public float Doom { get; set; }
        public Question Result { get; set; }
    }
}
