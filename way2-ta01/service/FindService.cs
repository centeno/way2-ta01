using System;
using System.Configuration;
using System.Net;
using way2_ta01.model;

namespace way2_ta01.service
{
    public class FindService : IDisposable
    {
        private string url, word;
        private long lastIndex, index, tentatives, baseIndex;
        private WebClient wc;

        public FindService(String url)
        {
            if (String.IsNullOrEmpty(url))
                throw new Exception("Url não pode ser nula");

            this.url = url;
            wc = new WebClient();
        }

        public FindService()
            : this(ConfigurationManager.AppSettings["url"])
        { }

        public FindResult Search(string word)
        {
            if (String.IsNullOrEmpty(word))
                throw new Exception("Palavra não pode ser nula");

            this.word = word;
            
            index = baseIndex = tentatives = 0;

            long? i = Find(0);

            return new FindResult()
            {
                Index = i,
                Tentatives = tentatives,
                Success = i.HasValue,
                Word = word
            };

        }

        private long? Find(long idx)
        {
            lastIndex = index;
            index = idx;

            long? compare = Compare(baseIndex + index);

            if (!compare.HasValue)
            {
                baseIndex += lastIndex;
                index = 0;
                return Find(index);
            }
            else
            {
                if (compare < 0)
                    return SearchBinary(baseIndex + lastIndex, baseIndex + index);
                else if (compare > 0)
                    return Find((index == 0) ? index + 1 : (index * 2));
                else
                    return baseIndex + index;
            }

        }

        protected long? Compare(long idx)
        {
            tentatives++;
            try
            {
                string testWord = wc.DownloadString(string.Concat(url, idx));
                return string.Compare(word, testWord, true);
            }
            catch (WebException)
            {
                return null;
            }
        }

        private long? SearchBinary(long low, long high)
        {
            if (low > high)
                return null;

            long mid = (low + high) / 2;
            long? compare = Compare(mid);

            if (compare > 0)
                return SearchBinary(mid + 1, high);
            else if (compare < 0)
                return SearchBinary(low, mid - 1);
            else
                return mid;
        }

        public void Dispose()
        {
            wc.Dispose();
        }
    }
}
