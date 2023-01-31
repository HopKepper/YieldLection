using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeildLection
{
    public class PerfomanceExampleSimple
    {
        public int[] Array(int start, int count)
        {
            var numbers = new int[count];
            for (var i = 0; i < count; ++i)
                numbers[i] = start + i;

            return numbers;
        }

        public int[] Iterator(int start, int count)
        {
            return IteratorInternal(start, count).ToArray();
        }

        private IEnumerable<int> IteratorInternal(int start, int count)
        {
            for (var i = 0; i < count; ++i)
                yield return start + i;
        }
    }

    public class PerfomanceExampleHard
    {
        public List<Tuple<int, string>> List(int start, int count)
        {
            var odds = new List<Tuple<int, string>>(); //First time
            foreach (var record in OddsArray(ReadFromDb(start, count)))
                if (record.Item1 % 3 == 0)
                    odds.Add(record);

            return odds;
        }

        public List<Tuple<int, string>> Iterator(int start, int count)
        {
            return IteratorInternal(start, count).ToList();
        }

        private IEnumerable<Tuple<int, string>> IteratorInternal(int start, int count)
        {
            foreach (var record in OddsIterator(ReadFromDb(start, count)))
                if (record.Item1 % 3 == 0)
                    yield return record;
        }

        private IEnumerable<Tuple<int, string>> OddsIterator(IEnumerable<Tuple<int, string>> records)
        {
            foreach (var record in records)
                if (record.Item1 % 2 != 0)
                    yield return record;
        }

        private List<Tuple<int, string>> OddsArray(IEnumerable<Tuple<int, string>> records)
        {
            var odds = new List<Tuple<int, string>>(); //Second time
            foreach (var record in records)
                if (record.Item1 % 2 != 0)
                    odds.Add(record);

            return odds;
        }

        private IEnumerable<Tuple<int, string>> ReadFromDb(int start, int count)
        {
            for (var i = start; i < count; ++i)
                yield return new Tuple<int, string>(start + i, RandomString());
        }

        private static string RandomString()
        {
            return Guid.NewGuid().ToString("n");
        }
    }
}