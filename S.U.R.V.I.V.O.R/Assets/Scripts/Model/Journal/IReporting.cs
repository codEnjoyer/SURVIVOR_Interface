using System;

namespace Model.Journal
{
    public interface IReporting
    {
        public event Action<IReporting> Report;

        public string CreateReport();
    }
}