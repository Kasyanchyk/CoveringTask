using System;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace CoveringTask.Models
{
    public class Algorithms
    {

        public List<List<int>> list_greedy_alg = new List<List<int>>();
        public List<List<int>> list_exact_alg = new List<List<int>>();
        public List<List<int>> list_exact_alg_non_weights = new List<List<int>>();
        public List<List<int>> list_henry_laboder_non_weight = new List<List<int>>();

        public long time_greedy { get; set; }
        public long time_exact { get; set; }
        public long time_exact_non_weights { get; set; }
        public long time_henry_laboder_non_weight { get; set; }

        public int sum_greedy { get; set; }
        public int sum_exact { get; set; }
        public int number_of_paths { get; set; }
        public int number_of_paths_henry_laboder { get; set; }

        public bool check_greedy { get; set; } = true;
        public bool check_exact { get; set; } = true;
        public bool check_exact_non_weights { get; set; } = true;
        public bool cheeck_henre_laboder_non_weignt { get; set; } = true;

        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці ")]
        [Range(1, int.MaxValue, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці в поле 'Номер задачі' ")]
        public int number_task { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці ")]
        [Range(0, 100, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці в поле 'Початкова вершина' ")]
        public int start { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці ")]
        [Range(0, 100, ErrorMessage = "Будь ласка, введіть ціле число більше 1-ці в поле 'Термінальна вершина' ")]
        public int finish { get; set; }

        public static string message { get; set; }
        public static string message2 { get; set; }

        public void GreedyAlg(Algorithms alg)
        {
            var rep = Repository.Responses;
            list_greedy_alg = new List<List<int>>(); 
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Greedy(rep[number_task].mas_weight, rep[number_task].number_of_nodes, start, finish);
            sum_greedy = SumRouteGreedy(rep[number_task].mas_weight, rep[number_task].number_of_nodes);
            stopWatch.Stop();
            time_greedy = stopWatch.ElapsedMilliseconds;
        }

        private int Min(int[] numbers)
        {
            int min = int.MaxValue, minIndex = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 0) numbers[i] = int.MaxValue;
                if (min > numbers[i])
                {
                    min = numbers[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
        private int[] GetArray(int[,] mas, int index)
        {
            int[] array = new int[mas.GetLength(0)];
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                if (mas[index, i] != 0)
                    array[i] = mas[index, i];
                else
                    array[i] = int.MaxValue;
            }
            return array;
        }


        private void Greedy(int[,] mas, int n, int start, int finish)
        {
            //0 - не була пройдена
            //1- була пройдена
            list_greedy_alg.Add(new List<int>());
            list_greedy_alg[0].Add(start);
            int count = 0;
            int[] array_indexes = new int[n];
            for (int i = 0; i < n; i++)
            {
                array_indexes[i] = 0;
            }
            array_indexes[start] = 2;
            int current_index = start;
            int pre_current_index = start;
            while (true)
            {
                int[] array = GetArray(mas, current_index);
                for (int i = 0; i < array.Length; i++)
                    if (array_indexes[i] == 2)
                        array[i] = int.MaxValue;
                array[pre_current_index] = int.MaxValue;
                array[start] = int.MaxValue;

                while (true)
                {
                    int min = Min(array);
                    if (array_indexes[min] != 1 && array_indexes[min] != 2)
                    {
                        pre_current_index = current_index;
                        current_index = min;
                        //flag2 = false;
                        list_greedy_alg[count].Add(current_index);
                        array_indexes[current_index] = 2;
                        break;
                    }
                    else if (array_indexes[min] == 1 && array[Min(array)] != int.MaxValue)
                    {
                        array[min] = int.MaxValue;
                        min = Min(array);
                    }

                    if (array[Min(array)] == int.MaxValue)
                    {
                        array = GetArray(mas, current_index);
                        array[pre_current_index] = int.MaxValue;
                        array[start] = int.MaxValue;

                        int m = int.MaxValue, minIndex = 0;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] == 0) array[i] = int.MaxValue;
                            if (m > array[i] && array_indexes[i] != 2)
                            {
                                m = array[i];
                                minIndex = i;
                            }
                        }

                        pre_current_index = current_index;
                        current_index = minIndex;
                        list_greedy_alg[count].Add(current_index);
                        array_indexes[current_index] = 1;
                        break;
                    }
                }
                bool flag4 = false;
                if (current_index == finish)
                {

                    count++;
                    list_greedy_alg.Add(new List<int>());
                    list_greedy_alg[count].Add(start);
                    current_index = start;
                    pre_current_index = start;

                    for (int i = 0; i < array_indexes.Length; i++)
                    {
                        if (array_indexes[i] == 2)
                            array_indexes[i] = 1;
                        array_indexes[start] = 2;
                    }

                    bool flag3 = true;
                    for (int i = 0; i < array_indexes.Length; i++)
                    {
                        if (array_indexes[i] > 0) { }
                        else
                        {
                            flag3 = false;
                        }
                    }
                    if (flag3)
                        flag4 = flag3;

                }
                if (flag4 == true)
                {
                    list_greedy_alg.RemoveAt(list_greedy_alg.Count - 1);
                    break;
                }
            }
        }

        public void ExactAlg(Algorithms alg)
        {
            var rep = Repository.Responses;
            ScriptEngine engine = Python.CreateEngine();
            var paths = engine.GetSearchPaths();
            paths.Add(@"Lib");
            engine.SetSearchPaths(paths);
            ScriptScope scope = engine.CreateScope();

            engine.ExecuteFile("ExactAlg.py", scope);

            dynamic function = scope.GetVariable("main_func");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = function(ArrayTransformation(rep[number_task].mas_weight, rep[number_task].number_of_nodes), start, finish);
            stopWatch.Stop();
            time_exact = stopWatch.ElapsedMilliseconds;
            IList<object> list_exact_alg1 = (IList<object>)result;
            int n = list_exact_alg1.Count;

            //var a = (int)m[1];
            for (int i = 0; i < n; i++)
            {
                list_exact_alg.Add(new List<int>());
                var m = (IList<object>)list_exact_alg1[i];
                for (int j = 0; j < m.Count; j++)
                {
                    int a = (int)m[j];
                    list_exact_alg[i].Add(a);
                }
            }

            sum_exact = SumRouteExact(rep[number_task].mas_weight, rep[number_task].number_of_nodes);

        }


        public void ExactAlgNonWeights(Algorithms alg)
        {
            var rep = Repository.Responses;
            int num = rep[number_task].number_of_nodes;
            int[,] mas = new int[num, num];

            for (int i = 0; i <num;i++)
            {
                for(int j=i+1;j<num;j++)
                {
                    if(rep[number_task].mas_weight[i,j]==0)
                    {

                    }
                    else
                    {
                        mas[i, j] = 1;
                        mas[j, i] = 1;
                    }
                }
            }

            ScriptEngine engine = Python.CreateEngine();
            var paths = engine.GetSearchPaths();
            paths.Add(@"Lib");
            engine.SetSearchPaths(paths);
            ScriptScope scope = engine.CreateScope();

            engine.ExecuteFile("ExactAlgNonWeights.py", scope);

            dynamic function = scope.GetVariable("main_func");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = function(ArrayTransformation(mas, num), start, finish);
            stopWatch.Stop();
            time_exact_non_weights = stopWatch.ElapsedMilliseconds;

            IList<object> list_exact_alg1 = (IList<object>)result;
            int n = list_exact_alg1.Count;

            //var a = (int)m[1];
            for (int i = 0; i < n; i++)
            {
                list_exact_alg_non_weights.Add(new List<int>());
                var m = (IList<object>)list_exact_alg1[i];
                for (int j = 0; j < m.Count; j++)
                {
                    int a = (int)m[j];
                    list_exact_alg_non_weights[i].Add(a);
                }
            }

            foreach(List<int> lst in list_exact_alg_non_weights)
            {
                number_of_paths += lst.Count - 1;
            }
        }

        public void Henry_Laboder_Non_Weiht(Algorithms alg)
        {
            var rep = Repository.Responses;

            int num = rep[number_task].number_of_nodes;
            int[,] mas = new int[num, num];

            for (int i = 0; i < num; i++)
            {
                for (int j = i + 1; j < num; j++)
                {
                    if (rep[number_task].mas_weight[i, j] == 0)
                    {

                    }
                    else
                    {
                        mas[i, j] = 1;
                        mas[j, i] = 1;
                    }
                }
            }

            ScriptEngine engine = Python.CreateEngine();
            var paths = engine.GetSearchPaths();
            paths.Add(@"Lib");
            engine.SetSearchPaths(paths);
            ScriptScope scope = engine.CreateScope();

            engine.ExecuteFile("HenryLaboderNonWeight.py", scope);

            dynamic function = scope.GetVariable("main_func");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            dynamic result = function(ArrayTransformation(mas, num), start, finish);
            stopWatch.Stop();
            time_henry_laboder_non_weight= stopWatch.ElapsedMilliseconds;
            IList<object> list = (IList<object>)result;
            int n = list.Count;

            //var a = (int)m[1];
            for (int i = 0; i < n; i++)
            {
                list_henry_laboder_non_weight.Add(new List<int>());
                var m = (IList<object>)list[i];
                for (int j = 0; j < m.Count; j++)
                {
                    int a = (int)m[j];
                    list_henry_laboder_non_weight[i].Add(a);
                }
            }

            foreach (List<int> lst in list_henry_laboder_non_weight)
            {
                number_of_paths_henry_laboder += lst.Count - 1;
            }

        }

        //метод перетворення двухмірного масиву в зубчастий масив
        private int[][] ArrayTransformation(int[,] mas, int n)
        {
            int[][] graph = new int[n][];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    graph[i][j] = mas[i, j];
                }
            }
            return graph;
        }
        private int SumRouteExact(int[,] mas, int n)
        {
            int s = 0;
            foreach (List<int> list in list_exact_alg)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    s += mas[list[i - 1], list[i]];
                }
            }
            return s;
        }

        //лічильники
        /*static private int count = 0;
        static private int iter = 0;*/

        //Метод жадібного проходу до кінцевої точки
        /*private void Func(int[,] mas, int n, int start, int finish)
        {

            list_greedy_alg.Add(new List<int>());

            list_greedy_alg[count].Add(start);
            int[] m = new int[n];
            for (int i = 0; i < n; i++)
            {
                m[i] = mas[start, i];
            }
            m[finish] = 100000;
            int mCountIndexNotNull = IndexNotNulArray(m);

            int[] m_finish = new int[n];
            for (int i = 0; i < n; i++)
            {
                m_finish[i] = mas[i, finish];
            }
            int m_finishCountIndexNotNull = IndexNotNulArray(m_finish);

            while (true)
            {

                int min = Min(m);
                if (m[min] == 100000)
                {
                    list_greedy_alg.RemoveAt(count);
                    count--;
                    break;
                }
                if (min == finish)
                {
                    list_greedy_alg[count].Add(min);
                    break;
                }

                if (!IsInQueue(min))
                {

                    list_greedy_alg[count].Add(min);
                    m = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        m[i] = mas[min, i];
                    }
                }
                else
                {
                    m[min] = 100000;
                }
            }
            count++;
            iter++;
            for (int i = 0; i < n; i++)
            {
                if (!IsInQueue(i) && iter < m_finishCountIndexNotNull && iter < mCountIndexNotNull)
                    Func(mas, n, start, finish);
            }
        }*/


       /* //Метод що викликає алгоритм Дейкстри на не пройдені вершини графу
        private void Func1(int[,] mas, int n, int start, int finish)
        {
            int temp = start;
            for (int i = 0; i < n; i++)
            {
                if (!IsInQueue(i))
                {
                    temp = i;
                    break;
                }
            }

            if (temp != start)
            {
                list_greedy_alg.Add(new List<int>());
                var routeToStart = DijkstraWithoutQueue.DijkstraAlgorithm(mas, temp, start);
                var routeToFinish = DijkstraWithoutQueue.DijkstraAlgorithm(mas, temp, finish);
                for (int i = routeToStart.Count - 1; i >= 0; i--)
                {
                    list_greedy_alg[list_greedy_alg.Count - 1].Add(routeToStart[i]);
                }
                for (int i = 1; i < routeToFinish.Count; i++)
                {
                    list_greedy_alg[list_greedy_alg.Count - 1].Add(routeToFinish[i]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (!IsInQueue(i))
                {
                    Func1(mas, n, start, finish);
                }
            }
        }*/


        //Метод який сумує загальну вартість всіх шляхів
        private int SumRouteGreedy(int[,] mas, int n)
        {
            int s = 0;
            foreach (List<int> list in list_greedy_alg)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    s += mas[list[i - 1], list[i]];
                }
            }
            return s;
        }

        //повертає кількість не нульових елементів масива
        private int IndexNotNulArray(int[] mas)
        {
            int count = 0;
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i] != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /*//повертає індекс мінмального числа
        private int Min(int[] numbers)
        {
            int min = 100000, minIndex = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == 0) numbers[i] = 100000;
                if (min > numbers[i])
                {
                    min = numbers[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }*/

        //перевірка наявності вершини у списку пройдених вершин
        private bool IsInQueue(int m)
        {
            for (int i = 0; i < list_greedy_alg.Count; i++)
                for (int j = 0; j < list_greedy_alg[i].Count; j++)
                {
                    if (list_greedy_alg[i].Contains(m))
                        return true;
                }
            return false;
        }
    }
}
