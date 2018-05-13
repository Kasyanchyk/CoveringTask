using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoveringTask.Models;




namespace CoveringTask.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult GeneratorView()
        {
            Generator.message = "";
            return View();
        }

        [HttpPost]
        public ViewResult GeneratorView(Generator generator)
        {
            if (ModelState.IsValid)
            {

                if (generator.min_weight >= generator.max_weight)
                {
                    Generator.message = "'Максимальна вага' має бути більше 'Мінмальної ваги'";

                    return View(generator);
                }
                else
                {
                    Generator.message = "";


                    for (int i = 0; i < generator.num_iter; i++)
                    {
                        Generator temp_generator = new Generator(generator);
                        temp_generator.StartGenerator(generator.number_of_nodes, generator.min_weight, generator.max_weight);
                        Repository.AddResponse(temp_generator);

                    }
                    return View("Index");
                }
            }
            else
            {

                return View();
            }
        }



        public ViewResult GeneratorListView()
        {
            return View(Repository.Responses);
        }

        [HttpGet]
        public ViewResult AlgorithmView()
        {
            Algorithms.message = String.Format("");

            var rep = Repository.Responses;
            if (rep.Count == 0)
            {
                Algorithms.message2 = "Немає доступних задач";
                return View();
            }

            Algorithms.message2 = String.Format("Виберіть номер задачі в діапазоні в 1 до {0}", rep.Count);
            return View();
        }

        [HttpPost]
        public ViewResult AlgorithmView(Algorithms alg)
        {
            if (ModelState.IsValid)
            {
                var rep = Repository.Responses;

                if (rep.Count == 0)
                {
                    Algorithms.message = String.Format("Немає доступних задач. Згенеруйте задачу на початковій сторінці");
                    return View(alg);
                }
                else if (alg.number_task > rep.Count)
                {
                    Algorithms.message = String.Format("Виберіть номер задачі в діапазоні в 1 до {0}", rep.Count);
                    return View(alg);
                }
                else if (alg.start < rep[alg.number_task - 1].number_of_nodes && alg.finish < rep[alg.number_task - 1].number_of_nodes)
                {
                    Algorithms.message = "";
                    alg.number_task--;

                    try
                    {
                        if (alg.check_exact)
                            alg.ExactAlg(alg);
                        else
                            alg.list_exact_alg = null;
                        //throw new NullReferenceException("rez is null");
                    }
                    catch
                    {
                        alg.list_exact_alg = null;
                    }


                    try
                    {
                        if (alg.check_exact_non_weights)
                            alg.ExactAlgNonWeights(alg);
                        else
                            alg.list_exact_alg_non_weights = null;
                        //throw new NullReferenceException("rez is null");
                    }
                    catch
                    {
                        alg.list_exact_alg_non_weights = null;
                    }

                    try
                    {
                        if (alg.check_greedy)
                            alg.GreedyAlg(alg);
                        else
                            alg.list_greedy_alg = null;
                    }
                    catch
                    {
                        alg.list_greedy_alg = null;
                    }

                    try
                    {
                        if (alg.cheeck_henre_laboder_non_weignt)
                            alg.Henry_Laboder_Non_Weiht(alg);
                        else
                            alg.list_henry_laboder_non_weight = null;
                    }
                    catch
                    {
                        alg.list_henry_laboder_non_weight = null;
                    }



                    alg.number_task++;
                    Repository.AddAnswer(alg);

                    return View("AnswersView", alg);
                }

                else
                {
                    Algorithms.message = String.Format("Недоступна вершина. Виберіть вершину в діапазоні від 0 до {0}", rep[alg.number_task - 1].number_of_nodes - 1);
                    return View(alg);
                }
            }
            else
            {
                return View();
            }
        }


        public ViewResult AnswerListView()
        {
            return View(Repository.Answers_list);
        }

        public ViewResult InfoView()
        {
            return View();
        }
    }
}
