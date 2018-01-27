using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    public class Question
    {
        public string Name { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Triger> Trigers { get; set; }
    }
}
