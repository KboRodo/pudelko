using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Pudelko
{
	public class Box : IFormattable, IEquatable<Box>, IEnumerable<double>
	{
		private double[] dimensions = new double[3];

		//domyslnie zapisywane w metrach
		#region getters/setters
		public double A
		{
			get => dimensions[0];

			set
			{
				dimensions[1] = ((value <= 10) && (value > 0))
					? value
					: throw new System.ArgumentOutOfRangeException("Podana wartosc jest za duża/ujemna");
			}
		}
		public double B
		{
			get => dimensions[1];

			set
			{
				dimensions[1] = ((value <= 10) && (value > 0))
					? value
					: throw new System.ArgumentOutOfRangeException("Podana wartosc jest za duża/ujemna");
			}
		}
		public double C
		{
			get => dimensions[2];

			set
			{
				dimensions[2] = ((value <= 10) && (value > 0))
					? value
					: throw new System.ArgumentOutOfRangeException("Podana wartosc jest za duża/ujemna");
			}
		}
        #endregion getters/setters

		//konstruktory i metody klasy Box
        #region konstruktory/metody
        public Box()//domyslny konstrukto zwaracajacy pudelko 10x10x10
		{
			A = 0.1;
			B = 0.1;
			C = 0.1;
		}
		public Box(double a = 0.1, double b = 0.1, double c = 0.1, string jednostka = "m")//konsruktor umozliwiajacy podawanie wymairow i jendostek
		{
			A = a;
			B = b;
			C = c;
		}

		public string objetosc()
        {
			return String.Format("{0} m3",Math.Round((A * B * C),6));
        }
		
		public Box Parse(string dimensions)
		{ 
			//dodać parametry
			return new Box();
        }
        #endregion konstruktory/metody

        //zwrot wymiarow  w rozncyh jednostkach
        #region iformatable-jednostki
        public string Meter
		{
			get {
				return String.Format("{0}m x {1}m x {2}m", A, B, C);
			}
		}
		public string Centimeter
		{
			get
			{
				return String.Format("{0}cm x {1}cm x {2}cm", A*100, B*100, C*100);
			}
		}
		public string Milimeter
		{
			get
			{
				return String.Format("{0}mm x {1}mm x {2}mm", A*1000, B*1000, C*1000);
			}
		}
		#endregion iformatable-jednostki

		//implementacja iformattable
		#region implementacja-iformattable
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format)) format = "M";
			if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
				case "M":
					return Meter;
				case "MM":
					return Milimeter;
				case "CM":
					return Centimeter;
				default:
					throw new System.ArgumentException("Nie istnieje taki format");
            }
		}
        #endregion implementacja-iformattable

        //implementacja iequatable
        #region implementacja iequatable
		public bool Equals(Box _box)
        {
			if (_box == null)
			{
				return false;
			}
			else if (_box.A == A && _box.B == B && _box.C == C)
			{
				return true;
			}
			else
			{
				return false;
			}
        }

        public override int GetHashCode()
        {
            return Tuple.Create(A,B,C).GetHashCode();
        }

		public static bool operator == (Box _boxA, Box _boxB)
        {
			if(_boxA.Equals(_boxB))
            {
				return true;
            }
            else
            {
				return false;
            }
        }

		public static bool operator != (Box _boxA, Box _boxB)
		{
			if (_boxA.Equals(_boxB)==false)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion implementacja iequatable

		//implementacja operatorow
		#region operatory
		public static Box operator +(Box _box1, Box _box2)
		{
			List<double> wymiaryBox1 = new List<double> { _box1.A, _box1.B, _box1.C };
			List<double> wymiaryBox2 = new List<double> { _box2.A, _box2.B, _box2.C };
			wymiaryBox1.Sort();//sortowanie dlugosci bokow pudelek
			wymiaryBox2.Sort();

			return new Box(wymiaryBox1[0] + wymiaryBox2[0], wymiaryBox1[1] + wymiaryBox2[1], wymiaryBox1[2] + wymiaryBox2[2]);
		}

		public static explicit operator double[] (Box box)
        {
			double[] boxToDouble = new double[] { box.A, box.B, box.C };
            return (boxToDouble);
        }

		/* konwersja z tuple na box
		public static implicit operator Box(ValueTuple<(int a, int b, int c)> parameters)
        {
			return new Box(Convert.ToDouble(parameters.a), Convert.ToDouble(parameters.b), Convert.ToDouble(parameters.c));
        }
		*/

		public double this[int n]
        {
            get
            {
                switch (n)
                {
					case 1:
						return A;
					case 2:
						return B;
					case 3:
						return C;
					default:
						return -1;
                }
            }
        }
		#endregion operatory

		#region implementacja enumerable

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public IEnumerator<double> GetEnumerator()
        {
			foreach(double i in dimensions)
            {
				yield return i;
            }
        }

        #endregion implementacja enumerable
    }
}