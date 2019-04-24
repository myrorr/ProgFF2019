using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace Prog2FF_2019
{
    class Rooms<T> : IEnumerable   //adott terem amibe a programokat teszed el (a külső láncolt lista egy szeme)
    {
        Programok fej;
        class Programok // belső láncolt lista egy szeme, a programok amiket kezelünk
        {
            Random rnd = new Random();
            int ár;
            public T adat;
            public Programok kovezetkezo;
        }

        class LancoltListaBejaro : IEnumerator<T>
        {
            Programok aktualis = null;
            Programok fej;

            public LancoltListaBejaro(Programok fej)
            {
                this.fej = fej;
            }
            public T Current
            {
                get
                {
                    return aktualis.adat;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (aktualis == null)
                {
                    if (fej != null)
                    {
                        aktualis = fej;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (aktualis.kovezetkezo != null)
                    {
                        aktualis = aktualis.kovezetkezo;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public void Reset()
            {
            }
        }
        public void BeszurElejere(T ertek)
        {
            Programok ujElem = new Programok();
            ujElem.adat = ertek;
            ujElem.kovezetkezo = fej;
            fej = ujElem;
        }

        // indexelés       

        public T this[int index]
        {
            get
            {
                if (index >= 0)
                {
                    Programok aktualis = fej;
                    int db = 0;
                    while (db < index && aktualis != null)
                    {
                        aktualis = aktualis.kovezetkezo;
                        db++;
                    }
                    if (aktualis != null)
                    {
                        return aktualis.adat;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else
                    throw new IndexOutOfRangeException();
            }
            set { /* set the specified index to value here */ }
        }

        // bejárhatóvá tétel OK

        // elvégzi minden listaelemre a paraméterként átadott műveletet OK
        public void Bejar(Action<T> muvelet)
        {
            Programok aktualis = fej;
            while (aktualis != null)
            {
                muvelet?.Invoke(aktualis.adat);
                aktualis = aktualis.kovezetkezo;
            }
        }

        public void Torol()
        { }

        public void Keres()
        { }

        public IEnumerator GetEnumerator()
        {
            return new LancoltListaBejaro(fej);
        }
    }
}
