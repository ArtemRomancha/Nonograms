using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Nonograms
{
    public partial class VisualBoard : Form
    {
        int Size_Box = 25; //Размер 1 клеточки, кол-во клеток в ширину, кол-во клеток в высоту
        Graphics Back; // для отрисовки фона
        Graphics Front; // для отрисовки переднего плана
        Numbers Nums; // объект для условий
        Board Nono; //доска
        BinaryFormatter formatter;

        public VisualBoard()
        {
            InitializeComponent(); //Инициализация компонента ВинФорм
            
            formatter = new BinaryFormatter(); //Инициализация форматера для сериализации

            DialogResult load = MessageBox.Show("Хотите ли Вы восстановить предыдущую игру?", "Восстановить", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (load == DialogResult.Yes)
            {
                using (var fStream = File.OpenRead("./BoardInfo.dat"))
                {
                    Nono = (Board)formatter.Deserialize(fStream);
                }
                using (var fStream = File.OpenRead("./NumsInfo.dat"))
                {
                    Nums = (Numbers)formatter.Deserialize(fStream);
                }              
            }
            if (load == DialogResult.No) 
            {
                Nono = new Board(); //Создаем доску 
                Nums = new Numbers(Nono.Nono_Width, Nono.Nono_Height); // Создаем объект для хранения условий, в качестве параметров передаем Ширину нашего изображения и Высоту
                Nums.Take_Numbers(Nono.cells); //Просчитываем условия для каждой строки и столбца
                using (var fStream = new FileStream("./NumsInfo.dat", FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    formatter.Serialize(fStream, Nums);
                }
            }
     


            Initialize(Nono.Nono_Width, Nono.Nono_Height, Nums.max_horiz, Nums.max_vert); // Инициализируем необходимые компоненты для отрисовки элементов
            Draw_Board(Nono.Nono_Width, Nono.Nono_Height, Nums.max_horiz, Nums.max_vert); // Отрисовываем доску
            Draw_Numbers(Nums); //Отрисовываем условия
            ReDraw();//
        }

        void Initialize(int width, int height, int max_hor, int max_vert)
        {
            Nonogram_Board.Width = (width + max_hor) * Size_Box + 1;    //Устанавливаем ширину области для отрисовки (ПикчерБокса) (Ширина изображения + Ширина максимального кол-ва условий в строке) умноженное на размер ячейки 
            Nonogram_Board.Height = (height + max_vert) * Size_Box + 1; // -//- (Высота изображения + Высота максимального кол-ва условий в столбце)
            Clear.Location = new Point(Nonogram_Board.Location.X + Nonogram_Board.Width - Clear.Width, Clear.Location.Y); //Устанавливаем положение кнопки очистить в Правом углу изображения (Координата левого верхнего угла изображения + ширина изображения - Ширина кнопки)
            Control.Location = new Point(Nonogram_Board.Location.X, Control.Location.Y); // Устанавливаем положение кнопки Проверить в левом верхнем углу
            Bitmap bBack = new Bitmap((width + max_hor) * Size_Box + 1, (height + max_vert) * Size_Box + 1);      // создаем битмап для рисования на заднем плане на 16 ячеек размера Size
            Bitmap bFront = new Bitmap((width + max_hor) * Size_Box + 1, (height + max_vert) * Size_Box + 1);
            Back = Graphics.FromImage(bBack);
            Front = Graphics.FromImage(bFront);
            Nonogram_Board.BackgroundImage = bBack;
            Nonogram_Board.Image = bFront;            
            Nonogram_Board.Refresh();
        }
        void Draw_Board(int width, int height, int max_hor, int max_vert)
        {
            Pen blackPen = new Pen(Color.Red, 1); //Создаем кисть которой рисовать будем (Цвет, толщина)
            for (int i = max_hor; i <= width + max_hor; i++)  //Вертикальные линии доски
            {
                if ((i - max_hor) % 5 == 0)
                    Front.DrawLine(new Pen(Color.Red, 3), i * Size_Box, 0, i * Size_Box, (height + max_vert) * Size_Box);
                else
                    Front.DrawLine(blackPen, i * Size_Box, 0, i * Size_Box, (height + max_vert) * Size_Box);
            }
            for (int f = max_vert; f <= height + max_vert; f++) // Горизонтальные линии доски
            {
                if ((f - max_vert) % 5 == 0)
                    Front.DrawLine(new Pen(Color.Red, 3), 0, f * Size_Box, (width + max_hor) * Size_Box, f * Size_Box);
                else
                    Front.DrawLine(blackPen, 0, f * Size_Box, (width + max_hor) * Size_Box, f * Size_Box);

            }
            Nonogram_Board.Refresh(); // Обновляем рисунок в Пикчербоксе
        }
        void Draw_Numbers(Numbers num)
        {
            Font drawFont = new Font("Arial", 16); //Создаем шрифт которым будем писать условия
            SolidBrush drawBrush = new SolidBrush(Color.Black); //Создаем кисть 

            for (int i = 0; i < num.horizontal.Count(); i++) //Отрисовываем условия строк
            {
                for (int f = 0; f < num.horizontal[i].Count; f++)
                {
                    Front.DrawString(num.horizontal[i][f].ToString(), drawFont, drawBrush, (num.max_horiz - num.horizontal[i].Count + f) * Size_Box, (num.max_vert + i) * Size_Box + 2);
                }
            }
            for (int i = 0; i < num.vertical.Count(); i++) //Отрисовываем условия столбцов
            {
                for (int f = 0; f < num.vertical[i].Count; f++)
                {
                    Front.DrawString(num.vertical[i][f].ToString(), drawFont, drawBrush, (num.max_horiz + i) * Size_Box + 2, (num.max_vert - num.vertical[i].Count + f) * Size_Box);
                }
            }
            Nonogram_Board.Refresh(); //Обновляем изображение после отрисовки
        }
        public void ReDraw() //Перерисовываем пользовательский массив (Зарисовываем отмеченные клетки)
        {
            Back.Clear(Color.Empty); //Очищаем пользовательский слой
            for (int i = 0; i < Nono.Input.GetLength(0); i++)
            {
                for (int f = 0; f < Nono.Input.GetLength(1); f++)
                {
                    if (Nono.Input[i, f] == 1) //если равно 1 то зарисоовываем сплошным цветом
                        Back.FillRectangle(new SolidBrush(Color.Black), (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box, Size_Box, Size_Box);
                    else
                        if (Nono.Input[i, f] == 2) // если 2, то зачеркиваем клетку
                        {
                            Pen Pen_ = new Pen(Color.Black, 2);
                            Back.DrawLine(Pen_, (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box, (f + Nums.max_horiz) * Size_Box + Size_Box, (i + Nums.max_vert) * Size_Box + Size_Box);
                            Back.DrawLine(Pen_, (f + Nums.max_horiz) * Size_Box + Size_Box, (i + Nums.max_vert) * Size_Box, (f + Nums.max_horiz) * Size_Box, (i + Nums.max_vert) * Size_Box + Size_Box);
                        }
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e) //Если была нажата клавиша мышки на пикчербоксе
        {
            int X = Cursor.Position.X - this.DesktopLocation.X - Nonogram_Board.Location.X - 8;  // Вычисляем позицию куда кликнули мышкой
            int Y = Cursor.Position.Y - this.DesktopLocation.Y - Nonogram_Board.Location.Y - 30;
            int X1 = X / Size_Box, Y1 = Y / Size_Box;   //Определяем в какой квадратик мы попали курсором

            if (e.Button == MouseButtons.Left && (X1 - Nums.max_horiz) >= 0 && (Y1 - Nums.max_vert) >= 0) //Если мы кликнули Левой клавишой мышки и попали именно в область изображения то 
            {
                Nono.Draw((X1 - Nums.max_horiz), (Y1 - Nums.max_vert), 1);  //Добавляем в МАССИВ пользовательского ввода в позицию куда кликнули 1 (Зарисованный квадратик)                 
                ReDraw(); // перерисовываем Слой полльзователя
            }
            if (e.Button == MouseButtons.Right && (X1 - Nums.max_horiz) >= 0 && (Y1 - Nums.max_vert) >= 0) // Если мы кликнули Правой клавишой мышки и попали именно в область изображения то 
            {
                Nono.Draw((X1 - Nums.max_horiz), (Y1 - Nums.max_vert), 2); //Добавляем в МАССИВ пользовательского ввода в позицию куда кликнули 2 (Пустая ячейка)                
                ReDraw();
            }

            using (var fStream = new FileStream("./BoardInfo.dat", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                formatter.Serialize(fStream, Nono);
            }

            Nonogram_Board.Refresh();
        }

        private void Control_Click(object sender, EventArgs e) //Если нажата клавиша Проверить
        {
            if (Nono.Control())  //Если все верно то высветим все верно
            {
                MessageBox.Show("All RIGHT!");                
            }
            else
                MessageBox.Show("Mistake"); //Иначе "Ошибка"
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Nono.Clear(); // Очищаем пользовательский массив
            Back.Clear(Color.Empty); // Очищаем пользовательский слой 
            Nonogram_Board.Refresh(); //обновляем изображение
            using (var fStream = new FileStream("./BoardInfo.dat", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                formatter.Serialize(fStream, Nono);
            }
        }

    }
}
