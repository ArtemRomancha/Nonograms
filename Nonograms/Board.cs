using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Nonograms
{
    [Serializable]
    class Board
    {
        int _nono_width, _nono_height; // ширина и высота

        public int[,] cells; // массив изображения 
        int[,] _input;        // массив пользовательского ввода
        
        public int Nono_Width //Свойство которое возвращает ширину
        {
            get { return _nono_width; }
        }
        public int Nono_Height //Свойство которое возвращает высоту
        {
            get { return _nono_height; }
        }
        public Board()
        {
            ReadAndParse(); //Считываем из файла и переводим в массив
            _input = new int[_nono_height, _nono_width]; //Создаем пользовательский массив
        }
        public void ReadAndParse()
        {
            StreamReader reader = new StreamReader(@"Image.txt"); //Создаем стримридер в корневой папке
            string s = reader.ReadLine(); //Считываем строку
            string[] size = s.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries); //Разбиваем строку на 2 элемента разделенных нижним подчеркиванием
            _nono_height = Convert.ToInt16(size[0]); //Первое число - высота
            _nono_width = Convert.ToInt16(size[1]); // Второе число - ширина
            
            cells = new int[_nono_height, _nono_width]; //Инициализируем массив 

            for (int f = 0; f < cells.GetLength(0); f++) //Заполняем массив
            {
                s = reader.ReadLine();  // считываем строку 
                for (int i = 0; i < cells.GetLength(1); i++)
                {
                    cells[f, i] = Convert.ToInt16(s[i]) - (int)'0'; //Парсим ее в нормальный вид (0, 1)
                }
            }
            reader.Close(); //закрываем чтение из файла
        }
        public void Draw(int x, int y, int stat) //Передаем нажатие клавиши в пользовательский массив
        {
            if (_input[y, x] == stat) //Если кнопка в этой клетке была уже нажата, мы отменяем состояние закрашивания
            {
                _input[y, x] = 0;
            }
            else
            {
                _input[y, x] = stat; //Иначе клетка закрашена
            }
        }
        public void ReDraw(Graphics Back, int Size_Box, Numbers Nums) //Перерисовываем пользовательский массив (Зарисовываем отмеченные клетки)
        {
            Back.Clear(Color.Empty); //Очищаем пользовательский слой
            for(int i=0; i<_input.GetLength(0); i++)
            {
                for (int f = 0; f < _input.GetLength(1); f++)
                {
                    if (_input[i, f] == 1) //если равно 1 то зарисоовываем сплошным цветом
                        Back.FillRectangle(new SolidBrush(Color.Black), (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box, Size_Box, Size_Box);
                    else
                        if (_input[i, f] == 2) // если 2, то зачеркиваем клетку
                        {
                            Pen Pen_ = new Pen(Color.Black, 2);
                            Back.DrawLine(Pen_, (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box, (f + Nums.max_horiz) * Size_Box + Size_Box, (i + Nums.max_vert) * Size_Box + Size_Box);
                            Back.DrawLine(Pen_, (f + Nums.max_horiz) * Size_Box + Size_Box, (i + Nums.max_vert) * Size_Box, (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box + Size_Box);   
                        }
                }
            }
        }
        public bool Control() //Проверяем верно ли решено
        {
            for (int i = 0; i < _input.GetLength(0); i++)
            {
                for (int f = 0; f < _input.GetLength(1); f++)
                {
                    if (_input[i, f] != cells[i, f] && cells[i, f] == 1) //Если ячейки пользовательского массива и массива изображения не совпадают, но там должно быть зарисовано, то ошибка
                        return false;
                    if( cells[i, f] == 0 && _input[i, f] == 1)  //Если ничего нет, а мы зарисовали то ОШИБКА
                        return false;
                }
            }
            return true;
        }
        public void Clear() //Опустошаем пользовательский массив
        {
            for (int i = 0; i < _input.GetLength(0); i++)
            {
                for (int f = 0; f < _input.GetLength(1); f++)
                {
                    _input[i, f] = 0;
                }
            }
        }        
    }
}
