using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoveringTask.Models
{
    public static class Repository
    {
        private static List<Generator> responses = new List<Generator>();
        public static List<Generator> Responses
        {
            get { return responses; }
        }

        public static void AddResponse(Generator response)
        {
            responses.Add(response);
        }


        private static List<Algorithms> answers_list = new List<Algorithms>();
        public static List<Algorithms> Answers_list
        {
            get { return answers_list; }
        }

        public static void AddAnswer(Algorithms answer)
        {
            answers_list.Add(answer);
        }
    }
}


