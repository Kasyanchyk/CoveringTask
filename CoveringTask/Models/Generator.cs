using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoveringTask.Models
{
    public class Generator
    {


        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 3-ох ")]
        [Range(3, 100, ErrorMessage = "Будь ласка, введіть ціле число більше 3-ох в поле 'Кількість вузлів' ")]
        public int number_of_nodes { get; set; }


        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці")]
        [Range(1, 1000, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці")]
        public int min_weight { get; set; }


        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці")]
        [Range(1, 1000, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці")]
        //[Compare(max_weight>min_weight)]
        public int max_weight { get; set; }
        public static string message { get; set; }

        [Range(1, 100, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці")]
        public int num_iter { get; set; } = 1;


        public int[,] mas_weight;

        public Generator(Generator gen)
        {
            this.number_of_nodes = gen.number_of_nodes;
            this.min_weight = gen.min_weight;
            this.max_weight = gen.max_weight;
            this.mas_weight = new int[number_of_nodes,number_of_nodes];
        }
        public Generator()
        {
        }

        public int[,] StartGenerator(int number_of_nodes, int min_weightint, int max_weight)
        {
            Random zero_or_one = new Random();
            Random rand = new Random();
            mas_weight = new int[number_of_nodes, number_of_nodes];
            for (int i = 0; i < number_of_nodes; i++)
            {
                for (int j = i + 1; j < number_of_nodes; j++)
                {
                    int r = zero_or_one.Next(0, 2);
                    if (r == 0)
                    {
                        mas_weight[i, j] = 0;
                        mas_weight[j, i] = 0;
                    }
                    else
                    {
                        mas_weight[i, j] = rand.Next(min_weight-1, max_weight+1);
                        mas_weight[j, i] = mas_weight[i, j];
                    }
                }
            }

            if (!HayConectividadsimple(mas_weight))
            {
                StartGenerator(number_of_nodes, min_weight, max_weight);
            }


            for (int i = 0; i < number_of_nodes; i++)
            {
                int count = 0;
                bool flag = false;
                for (int j = 0; j < number_of_nodes; j++)
                {
                    if (mas_weight[i, j] != 0)
                    {
                        count++;
                        if (count > 1)
                            flag = true;
                    }

                }
                if (!flag)
                    StartGenerator(number_of_nodes, min_weight, max_weight);
            }

            

            return mas_weight;
        }

        public bool HayConectividadsimple(int[,] graf)
        {
            Queue<int> queue = new Queue<int>();
            int n = graf.GetLength(0);
            
            int[] nodes = new int[n];

            for (int i = 0; i < n; i++)
            {
                nodes[i] = 0;
            }
            queue.Enqueue(0);
            while (!(queue.Count == 0))
            {
                int node = queue.Dequeue();
                nodes[node] = 2;
                for (int j = 0; j < n; j++)
                {
                    if (graf[node, j] >0 && nodes[j] == 0)
                    {
                        queue.Enqueue(j);
                        nodes[j] = 1;
                    }
                }
            }

            for(int i=0;i<n;i++)
            {
                if (nodes[i] == 0)
                    return false;
            }
            return true;
        }
    }
}
