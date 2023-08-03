
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightForm.SvgElement
{

    public static class SvgElements
    {
        private static List<SvgElement> Items = new List<SvgElement>()
        {
            new MoveTo(),
            new LineTo(),
            new HorizontalTo(),
            new VerticalTo(),
            new Rectangle(),
            new Ellipse(),
            new Weight(),
        };

        public static SvgElement Find(char c)
        {
            return Items.FirstOrDefault(i => i.Prefix == c.ToString().ToUpper()[0]);
        }

        public static List<SvgElement> Parse(string data)
        {
            List<SvgElement> ret = new List<SvgElement>();
            var prefs = Items.Select(f => f.Prefix).ToList();
            char c='\0';
            List<object> prm = new List<object>();
            string val="";
            int i = 0;

            Action AddValue = () => { prm.Add(int.Parse(val)); val = ""; };
            Action AddResult = () =>
            {
                if (c != '\0')
                {
                    if (prm.Count != prm.Count)
                        throw new Exception($"to many params from index {i} in '{data}'");
                    var p = Find(c)?.Clone();
                    if (p == null)
                        throw new Exception($"undefine figure from index {i} in '{data}'");
                    p.Params = prm.ToList();
                    ret.Add(p);
                    prm.Clear();
                    val = "";
                    c = '\0';
                }
                if(i<data.Length) c = data[i];
            };

            for (; i < data.Length; i++)
            {
                if (data[i] == ' ') AddValue.Invoke();
                else if (data[i] == ',' && val.Length > 0) AddValue.Invoke();
                else if (data[i] == '-') val = "-";
                else if (char.IsDigit(data[i])) val += data[i];
                else if (prefs.Contains(data[i])) AddResult.Invoke();
            }

            if (c != '\0')
            {
                if(val!="") AddValue.Invoke();
                AddResult.Invoke();
            }
            return ret;
        }
    }


    public class MoveTo : SvgElement
    {
        public override char Prefix => 'M';
        public override string Name => "MoveTo";
        public MoveTo() => Params = new List<object> {0,0};
        public MoveTo(int x,int y) => Params = new List<object> {x,y};
        public override SvgElement Clone() => new MoveTo((int)Params[0], (int)Params[1]);
    }

    public class LineTo : SvgElement
    {
        public override char Prefix => 'L';
        public override string Name => "LineTo";
        public LineTo() => Params = new List<object> { 0, 0 };
        public LineTo(int x, int y) => Params = new List<object> { x, y };
        public override SvgElement Clone() => new LineTo((int)Params[0], (int)Params[1]);
    }

    public class HorizontalTo : SvgElement
    {
        public override char Prefix => 'H';
        public override string Name => "HorizontalTo";
        public HorizontalTo() => Params = new List<object> { 0, 0 };
        public HorizontalTo(int x, int y) => Params = new List<object> { x, y };
        public override SvgElement Clone() => new HorizontalTo((int)Params[0], (int)Params[1]);
    }

    public class VerticalTo : SvgElement
    {
        public override char Prefix => 'V';
        public override string Name => "VerticalTo";
        public VerticalTo() => Params = new List<object> { 0, 0 };
        public VerticalTo(int x, int y) => Params = new List<object> { x, y };
        public override SvgElement Clone() => new VerticalTo((int)Params[0], (int)Params[1]);
    }

    public class Rectangle : SvgElement
    {
        public override char Prefix => 'R';
        public override string Name => "Rectangle";
        public Rectangle() => Params = new List<object> { 0,0 };
        public Rectangle(int x, int y) => Params = new List<object> { x, y };
        public override SvgElement Clone() => new Rectangle((int)Params[0], (int)Params[1]);
    }

    public class Ellipse : SvgElement
    {
        public override char Prefix => 'E';
        public override string Name => "Ellipse";
        public Ellipse() => Params = new List<object> { 0, 0 };
        public Ellipse(int x, int y) => Params = new List<object> { x, y };
        public override SvgElement Clone() => new Ellipse((int)Params[0],(int)Params[1]);
    }
    public class Weight : SvgElement
    {
        public override char Prefix => 'W';
        public override string Name => "Weight";
        public Weight() => Params = new List<object> { 1 };
        public Weight(int weight) => Params = new List<object> {weight};
        public override SvgElement Clone() => new Weight((int)Params[0]);
    }

    /// <summary> Описание элемента фигуры </summary>
    public class SvgElement
    {
        /// <summary> Символ элемента </summary>
        public virtual char Prefix { get; private set; }

        /// <summary> Наименование </summary>
        public virtual string Name { get; private set; }

        /// <summary> Кол-во параметров </summary>
        public virtual int CountParameter => Params.Count;

        /// <summary> Параметры </summary>
        public List<object> Params { get; set; } = new List<object>();

        public override string ToString()
        {
            string s = $"{Prefix} ";
            for (int i = 0; i < Params.Count; i++)
            {
                s += Params[i].ToString();
                if(i < Params.Count - 1) s+= ",";
            }
            return s;
        }

        public virtual SvgElement Clone()
        {
            return this;
        }

    }

}
