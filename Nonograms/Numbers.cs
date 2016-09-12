using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nonograms
{
    [Serializable]
    class Numbers
    {
        public List<int>[] horizontal; //список горизонтальных условий
        public List<int>[] vertical;    //список вертикальных условий
        public int max_vert = 0, max_horiz = 0; //максимальное кол-во условий в строке/столбце

        public Numbers(int width, int height)
        {
            horizontal = new List<int>[height]; //Список горизонтальных условий
            vertical = new List<int>[width]; // Список вертикальных условий
        }
        public void Take_Numbers(int [,] cells)
        {
            bool found=false;
            int counter = 0;
            for (int i = 0; i < cells.GetLength(0); i++) //вычисляем условия для каждой строки
            {
                horizontal[i] = new List<int>();
                found = false;
                counter = 0;
                for (int f = 0; f < cells.GetLength(1); f++) //пробегаемся по каждой строке если находим 1, то Фаунд = истина и увеличиваем счетчик, если натыкаемся на  элемент, то счетчик добавляем в список и обнуляем и продолжаем
                {
                    if (!found)
                    {
                        if (cells[i, f] == 1)
                        {
                            counter++;
                            found = true;
                        }
                    }
                    else
                    {
                        if (cells[i, f] == 1)
                        {
                            counter++;
                        }
                        else
                        {
                            found = false;
                            horizontal[i].Add(counter);
                            counter = 0;
                        }
                    }
                }
                if(counter!=0)
                    horizontal[i].Add(counter);
                if (horizontal[i].Count == 0) 
                    horizontal[i].Add(0);
            }            
            
            for (int i = 0; i < cells.GetLength(1); i++)    //условия для каждого столбца
            {
                vertical[i] = new List<int>();
                counter = 0;
                found = false;
                for (int f = 0; f < cells.GetLength(0); f++)
                {
                    if (!found)
                    {
                        if (cells[f, i] == 1)
                        {
                            counter++;
                            found = true;
                        }
                    }
                    else
                    {
                        if (cells[f, i] == 1)
                        {
                            counter++;
                        }
                        else
                        {
                            found = false;
                            vertical[i].Add(counter);
                            counter = 0;
                        }
                    }
                }
                if (counter != 0)
                    vertical[i].Add(counter);
                if (vertical[i].Count == 0) 
                    vertical[i].Add(0);
                
            }
            
            int tmp=0;
            foreach(List<int> list in horizontal) //находим максимальое кол-во условий в строке
            {
                foreach (int element in list)
                    tmp++;
                if (tmp > max_horiz)
                    max_horiz = tmp;
                tmp = 0;
            }
            foreach (List<int> list in vertical) // -//- в столбце
            {
                foreach (int element in list)
                    tmp++;
                if (tmp > max_vert)
                    max_vert = tmp;
                tmp = 0;
            }

        }

    }
}
